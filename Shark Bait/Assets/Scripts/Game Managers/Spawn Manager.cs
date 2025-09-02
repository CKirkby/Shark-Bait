using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Splines;

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
    
    private List<GameObject> SpawnedFishes = new();
    
    void Start()
    {
        StartCoroutine(SpawnEntity());
    }

    private IEnumerator SpawnEntity()
    {
        while (true)
        {
            if (Lanes.Length == 0) yield break;
            
            SplineContainer LaneToUse = Lanes[Random.Range(0, Lanes.Length)];

            Vector3 LaneStartPoint = LaneToUse.EvaluatePosition(0.0f);
            Vector2 SpawnPoint = new Vector2(LaneStartPoint.x, LaneStartPoint.y);
        
            yield return new WaitForSeconds(SpawnInterval);
        
            GameObject SpawnedFish = Instantiate(SmallFishSpawnClass, SpawnPoint, Quaternion.identity);
        
            SplineAnimate SplineAnim =  SpawnedFish.GetComponent<SplineAnimate>();

            SplineAnim.Container = LaneToUse;
            SplineAnim.Play();   
        }
    }
}
