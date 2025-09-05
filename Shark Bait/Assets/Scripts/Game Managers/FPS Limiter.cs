using System;
using UnityEngine;

public class FPSLimiter : MonoBehaviour
{
    [SerializeField] private int TargetFPS = 61;

    private void Awake()
    {
        // Force off VSYNC
        QualitySettings.vSyncCount = 0;
        // Force target fps
        Application.targetFrameRate = TargetFPS;
    }
}
