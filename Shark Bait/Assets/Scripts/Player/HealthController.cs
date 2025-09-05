using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [Header("Health Settings")]
    private int CurrentHealth;
    [SerializeField] private int MaxHealth;
    
    [Header("Health UI Settings")]
    [SerializeField] private Image[] HealthHearts;
    
    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        
        UpdateUIHealth(CurrentHealth);
        
        CheckIsOutOfLives();
    }

    public void AddHealth(int health)
    {
        CurrentHealth += health;
    }

    private void CheckIsOutOfLives()
    {
        if (CurrentHealth <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateUIHealth(int NowHealth)
    {
        if (HealthHearts.Length <= 0) return;
        
        Image HealthHeart = HealthHearts[CurrentHealth];
        
        switch (NowHealth)
        {
            case 0:
                
                // Sets the first heart black 
                HealthHeart.color = Color.black;
                break;
            
            case 1:

                // Sets the first heart red, sets the second heart black
                Image FirstHeart = HealthHearts[0];
                FirstHeart.color = Color.white;
                
                HealthHeart.color = Color.black;
                break;
            
            case 2:
                
                // Sets the second heart red, sets the third heart black
                Image SecondHeart = HealthHearts[1];
                SecondHeart.color = Color.white;
                
                HealthHeart.color = Color.black;
                break;
            
            case 3:
                
                // Sets the third heart red
                Image ThirdHeart = HealthHearts[2];
                ThirdHeart.color = Color.white;
                break;
        }
    }
}
