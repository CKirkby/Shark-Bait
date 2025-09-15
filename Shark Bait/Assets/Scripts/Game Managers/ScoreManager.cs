using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ScoreUI;
    [SerializeField] private DifficultyController DifficultyController;
    
    private int CurrentScore = 0;
    
    public void AddScore(int Score)
    {
       CurrentScore = Mathf.Clamp(CurrentScore += Score, 0, 999999999);

       // Sends the current score to the difficulty Controller so it can calculate speeds
       if (DifficultyController)
       {
           DifficultyController.UpdateDifficulty(CurrentScore);
       }
       
       ScoreUI.text = CurrentScore.ToString();
    }

    public int GetCurrentScore()
    {
        return CurrentScore;
    }
}
