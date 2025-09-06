using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Classes")]
    public GameObject SmallFishSpawnClass;
    public GameObject BigFishSpawnClass;
    public GameObject ToxicFishSpawnClass;

    [Header("Spline Settings")] 
    public SplineContainer[] Lanes;
    public float BaseSpeed = 5.0f;

    [Header("Spawn Settings")] 
    public float SpawnInterval;
    public float ChanceToSpawnSmallFish = 0.5f;
    public float ChanceToSpawnToxicFish = 0.8f;
    
    [Header("Object Pool Settings")]
    public GameObject ObjectPoolSpawnPoint;
    public int SmallFishPoolSize = 15;
    public int BigFishPoolSize = 5;
    public int ToxicFishPoolSize = 10;
    
    [Header("Multiplier Settings")]
    private float SpawnIntervalMultiplier = 1.0f;
    private float FishSpeedMultiplier = 1.0f;
    private float CachedModifiedSpeed;
    private float CachedUpdatedSpawnInterval;
    private int ActiveLanes = 1;
    
    private readonly List<KeyValuePair<GameObject, FishType>> SpawnedFishes = new();
    private readonly Dictionary<GameObject, SplineAnimate> FishAnimations = new();

    private void Start()
    {
        SpawnObjectPools();
        StartCoroutine(ActivateEntity());

        CachedModifiedSpeed = BaseSpeed;
        CachedUpdatedSpawnInterval = SpawnInterval;
    }

    private IEnumerator ActivateEntity()
    {
        while (true)
        {
            if (Lanes.Length == 0) yield break;
        
            yield return new WaitForSeconds(CachedUpdatedSpawnInterval);
            
            if (ActiveLanes > 1)
            {
                // Uses more then one lanes at once spawning more then one fish at once
                List<SplineContainer> ShuffledLanes = new List<SplineContainer>(Lanes);

                for (var i = 0; i < ShuffledLanes.Count; i++)
                {
                    // Shuffles the list
                    int randomIndex = Random.Range(i, ShuffledLanes.Count);
                    (ShuffledLanes[i], ShuffledLanes[randomIndex]) = (ShuffledLanes[randomIndex], ShuffledLanes[i]);
                }

                int NumberOfLanesToUse = Mathf.Min(ActiveLanes, ShuffledLanes.Count);
                List<SplineContainer> LanesToUse = ShuffledLanes.GetRange(0, NumberOfLanesToUse);

                foreach (var Lane in LanesToUse)
                {
                    StartCoroutine(MultiLaneActivation(Lane));
                }
            }
            else
            {
                
                // Gets a single spline game object
                SplineContainer LaneToUse = Lanes[Random.Range(0, Lanes.Length)];

                GetFishAndUpdateSplines(LaneToUse);
            }
        }
    }

    private void GetFishAndUpdateSplines(SplineContainer Lane)
    {
        // Spawns the fish at the start of the chosen spline
        GameObject GotFish = ChooseFishToActivate();
        
        // Gets the anim component for the spline from the fish
        SplineAnimate SplineAnim = GetSplineAnimation(GotFish);
        
        GotFish.SetActive(true);
        
        // Sets the new speed of the fish when its in use, should be equal across all fish
        SplineAnim.MaxSpeed = CachedModifiedSpeed;

        // Sets the anims spline to be the chosen lane and plays it.
        SplineAnim.Container = Lane;
        SplineAnim.Restart(true);
        SplineAnim.Play();   
    }

    private void SpawnObjectPools()
    {
        for (int i = 0; i < SmallFishPoolSize; i++)
        {
            HandleSpawnFish(SmallFishSpawnClass, FishType.Small);
        }

        for (int i = 0; i < BigFishPoolSize; i++)
        {
            HandleSpawnFish(BigFishSpawnClass, FishType.Big);
        }

        for (int i = 0; i < ToxicFishPoolSize; i++)
        {
            HandleSpawnFish(ToxicFishSpawnClass,  FishType.Toxic);
        }
    }

    void HandleSpawnFish(GameObject SpawnClass, FishType FishTypeInput)
    {
        if (!SpawnClass) return;
        
        GameObject SpawnedFish = Instantiate(SpawnClass, ObjectPoolSpawnPoint.transform.position, Quaternion.identity);
        SpawnedFishes.Add(new KeyValuePair<GameObject, FishType>(SpawnedFish, FishTypeInput));
        FishAnimations.Add(SpawnedFish, SpawnedFish.GetComponent<SplineAnimate>());
        SpawnedFish.SetActive(false);
    }

    GameObject GetPooledFish(FishType FishTypeInput)
    {
        if (SpawnedFishes.Count ==  0) return null;

        // Finds the index for the first fish type
        int index = SpawnedFishes.FindIndex(x => x.Value == FishTypeInput);
        
        if (index != -1)
        {
            // Gets the pair 
            KeyValuePair<GameObject, FishType> FoundFish = SpawnedFishes[index];
            
            // Sends the fish pair to the back of the queue, so it's not in use.
            SpawnedFishes.RemoveAt(index);
            SpawnedFishes.Add(FoundFish);
            
            // Returns the fish game object
            return FoundFish.Key;
        }
        
        return null;
    }

    SplineAnimate GetSplineAnimation(GameObject FishInput)
    {
        if (!FishInput) return null;

        // Finds the specific fish's spline anim script
        return FishAnimations.GetValueOrDefault(FishInput);
    }

    GameObject ChooseFishToActivate()
    {
        // Random Num between 0 and 1 to act as chance value
        float RandomValue = Random.value;
        
        // Small Fish Chance
        if (RandomValue < ChanceToSpawnSmallFish)
            return GetPooledFish(FishType.Small);
        
        // Toxic Fish Chance
        if (RandomValue < ChanceToSpawnToxicFish)
            return GetPooledFish(FishType.Toxic);
        
        // Big Fish Chance
            return GetPooledFish(FishType.Big);

    }

    public void SetSpeedMultiplier(float speedMultiplier)
    {
        FishSpeedMultiplier = speedMultiplier;
        
        // Sets the default speed so that the fish can be updated on use
        CachedModifiedSpeed = BaseSpeed * FishSpeedMultiplier;
    }

    public float GetSpeedMultiplier()
    {
        return FishSpeedMultiplier;
    }

    public void SetSpawnIntervalMultiplier(float spawnIntervalMultiplier)
    {
        SpawnIntervalMultiplier = spawnIntervalMultiplier;
        
        CachedUpdatedSpawnInterval = SpawnInterval / SpawnIntervalMultiplier;
    }

    public void SetActiveLanes(int activeLanes)
    {
        ActiveLanes = activeLanes;
    }

    public float GetSpawnIntervalMultiplier()
    {
        return SpawnIntervalMultiplier;
    }

    private IEnumerator MultiLaneActivation(SplineContainer Lane)
    {
        // Depending on the number of active lanes, it increases or decreases the range value because it might overlap
        float RandomWait = ActiveLanes switch
        {
            2 => Random.Range(0.1f, 0.5f),
            3 => 0.5f,
            _ => 0.25f
        };

        yield return new WaitForSeconds(RandomWait);
        
        GetFishAndUpdateSplines(Lane);
    }
    
}
