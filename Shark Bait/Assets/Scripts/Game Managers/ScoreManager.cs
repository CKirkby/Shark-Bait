using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ScoreUI;
    
    private int CurrentScore = 0;
    
    public void AddScore(int Score)
    {
       CurrentScore = Mathf.Clamp(CurrentScore += Score, 0, 999999999);
        
        ScoreUI.text = CurrentScore.ToString();
    }
}
