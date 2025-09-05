using System;
using UnityEngine;

public enum CollisionType
{
    Player, 
    Catcher,
}

public class CollisionProfile : MonoBehaviour
{
    [Header("Collision Settings")]
    [SerializeField] private CollisionType CollisionMode;

    [Header("Game Manager Settings")]
    [SerializeField] private GameObject GameManager;
    private ScoreManager ScoreManager;
    private GameObject ObjectPoolPoint;
    
    [Header("Health Settings")]
    [SerializeField] private HealthController HealthController;
    

    private void Awake()
    {
        ScoreManager = GameManager.GetComponent<ScoreManager>();

        ObjectPoolPoint = GameManager.transform.Find("ObjectPoolSpawnPoint").gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (CollisionMode)
        {
            case CollisionType.Player:
                
                // Do score stuff
                SetFishInactive(collision.gameObject);

                if (ScoreManager)
                {
                    FishConfiguration IncomingFishConfig = collision.GetComponent<FishConfiguration>();

                    switch (IncomingFishConfig.GetFishType())
                    {
                        case FishType.Small:
                            
                            ScoreManager.AddScore(IncomingFishConfig.GetFishValue());
                            
                            break;
                        case FishType.Big:
                            
                            ScoreManager.AddScore(IncomingFishConfig.GetFishValue());
                            
                            break;
                        
                        case FishType.Toxic:
                            
                            ScoreManager.AddScore(IncomingFishConfig.GetFishValue());

                            if (HealthController)
                            {
                                HealthController.TakeDamage(1);
                            }
                            
                            break;
                        
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                
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
