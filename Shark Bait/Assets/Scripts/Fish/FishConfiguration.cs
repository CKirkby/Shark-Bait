using UnityEngine;

public class FishConfiguration : MonoBehaviour
{
    [SerializeField] private FishType FishType;
    [SerializeField] private int FishValue;

    public FishType GetFishType()
    {
        return FishType;
    }

    public int GetFishValue()
    {
        return FishValue;
    }
}
