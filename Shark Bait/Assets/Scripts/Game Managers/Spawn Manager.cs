using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Classes")]
    public GameObject SmallFishSpawnClass;
    public GameObject BigFishSpawnClass;
    public GameObject ToxicFishSpawnClass;

    [Header("Spline Lanes")] 
    public SplineContainer[] Lanes;

    [Header("Spawn Settings")] 
    public float SpawnInterval;
    public float ChanceToSpawnSmallFish = 0.5f;
    public float ChanceToSpawnToxicFish = 0.8f;
    
    [Header("Object Pool Settings")]
    public GameObject ObjectPoolSpawnPoint;
    public int SmallFishPoolSize = 15;
    public int BigFishPoolSize = 5;
    public int ToxicFishPoolSize = 10;
    
    private readonly List<KeyValuePair<GameObject, FishType>> SpawnedFishes = new();
    private readonly Dictionary<GameObject, SplineAnimate> FishAnimations = new();

    private void Start()
    {
        SpawnObjectPools();
        StartCoroutine(ActivateEntity());
    }

    private IEnumerator ActivateEntity()
    {
        while (true)
        {
            if (Lanes.Length == 0) yield break;
            
            // Gets a random spline game object
            SplineContainer LaneToUse = Lanes[Random.Range(0, Lanes.Length)];
        
            yield return new WaitForSeconds(SpawnInterval);
        
            // Spawns the fish at the start of the chosen spline
            // NEED TO DO CHANCE SYSTEM HERE
            GameObject GotFish = ChooseFishToActivate();
            if (!GotFish) yield break;
            GotFish.SetActive(true);
        
            // Gets the anim component for the spline from the fish
            SplineAnimate SplineAnim = GetSplineAnimation(GotFish);
            if (!SplineAnim) yield break;

            // Sets the anims spline to be the chosen lane and plays it.
            SplineAnim.Container = LaneToUse;
            SplineAnim.Restart(true);
            SplineAnim.Play();   
        }
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
        
        // 50% Chance
        if (RandomValue < ChanceToSpawnSmallFish)
            return GetPooledFish(FishType.Small);
        
        // 30% Chance
        if (RandomValue < ChanceToSpawnToxicFish)
            return GetPooledFish(FishType.Toxic);
        
        // remaining 20%
            return GetPooledFish(FishType.Big);

    }
}
