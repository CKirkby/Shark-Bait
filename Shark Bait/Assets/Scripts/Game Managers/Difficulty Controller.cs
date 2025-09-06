using System;
using System.Collections;
using NUnit.Framework;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    [SerializeField] private SpawnManager SpawnManager;

    [Header("Difficulty Settings")]
    [Tooltip("The max score to reach before the difficulty multipliers are capped")]
    [SerializeField] private float MaxScoreInterval = 100f;
    [SerializeField] private Vector2 SpeedMultiplierRange = new(1f, 5f);
    [SerializeField] private Vector2 SpawnIntervalMultiplierRange = new(1f, 2f);
    
    [SerializeField] private float[] LaneActivationGates;

    private bool TierOneLaneActived = false;
    private bool TierTwoLaneActived = false;
    private bool MaxLaneTiersReached = false;
    
    public void UpdateDifficulty(int CurrentScore)
    {
        if (!SpawnManager) return;
        
        // Mimics a float curve, smoothly lerps score multiplier up to a max score. 
        float Alpha = Mathf.Clamp01(CurrentScore / MaxScoreInterval);
        
        // Calculates a new speed multiplier clamped between the range
        float NewSpeedModifier = Mathf.Lerp(SpeedMultiplierRange.x, SpeedMultiplierRange.y, Mathf.SmoothStep(0f, 1f, Alpha));
        
        // Calculates a new spawn time modifier clamped
        float NewSpawnIntervalModifier = Mathf.Lerp(SpawnIntervalMultiplierRange.x, SpawnIntervalMultiplierRange.y, 
            Mathf.SmoothStep(0f, 1f, Alpha));

        if (!MaxLaneTiersReached)
        {
            CheckToUpdateLanes(Alpha);
        }
        
        // Passes the new modifiers onto the spawn manager so that the fish can be tweaked
        SpawnManager.SetSpeedMultiplier(NewSpeedModifier);
        SpawnManager.SetSpawnIntervalMultiplier(NewSpawnIntervalModifier);
    }

    private void CheckToUpdateLanes(float CurrentProgress)
    {
        if (LaneActivationGates.Length == 0) return;
        
        // Checks the current progress alpha against the tier float gate
        if (CurrentProgress >= LaneActivationGates[0] && !TierOneLaneActived)
        {
            // sets two lanes to be active
            SpawnManager.SetActiveLanes(2);
            TierOneLaneActived = true;
        }
        else if (CurrentProgress >= LaneActivationGates[1] && TierTwoLaneActived)
        {
            // Sets three lanes to be active and maxes out here
            SpawnManager.SetActiveLanes(3);
            TierTwoLaneActived = true;
            
            MaxLaneTiersReached = true;
        }
    }
}
