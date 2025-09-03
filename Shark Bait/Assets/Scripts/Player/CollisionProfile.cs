using System;
using UnityEngine;

public enum CollisionType
{
    Player, 
    Catcher,
}

public class CollisionProfile : MonoBehaviour
{
    [SerializeField] private CollisionType CollisionMode;
    [SerializeField] private GameObject ObjectPoolPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (CollisionMode)
        {
            case CollisionType.Player:
                
                // Do score stuff
                SetFishInactive(collision.gameObject);
                
                break;
            
            case CollisionType.Catcher:
                
                SetFishInactive(collision.gameObject);
                
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void SetFishInactive(GameObject CollidedFish)
    {
        // Sets the missed fish inactive
        CollidedFish.SetActive(false);

        // Sets the position back to the object pool point
        if (ObjectPoolPoint)
        {
            CollidedFish.transform.position = ObjectPoolPoint.transform.position;
        }
        else
        {
            // If for whatever reason the object pool point isn't active, sets it manually
            CollidedFish.transform.position = new Vector2(0, 15);
        }
    }
}
