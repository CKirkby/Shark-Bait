using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameModeController : MonoBehaviour
{
    private int LoggedHighScore = 0;

    [Header("UI Settings")]
    [SerializeField] private GameObject MainHUD;
    [SerializeField] private GameObject GameOverUI;
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TextMeshProUGUI HighScoreText;
    
    [Header("Score Settings")]
    [SerializeField] private ScoreManager ScoreManager;
    
    [Header("Spawn Settings")]
    [SerializeField] private SpawnManager SpawnManager;
    
    private void Awake()
    {
        // Gets the existing high score
        LoggedHighScore =  PlayerPrefs.GetInt("HighScore", 0);
        
        MainHUD.SetActive(true);
        GameOverUI.SetActive(false);
    }

    public void StartGameOver()
    {
        if (!GameOverUI) return;
        if (!ScoreManager) return;

        if (SpawnManager)
        {
            SpawnManager.EndGameSystems();
        }
        
        // Gets the current score of the session from the score manager
        int LoggedSessionScore = ScoreManager.GetCurrentScore();
        HandleHighScore(LoggedSessionScore);
        
        // Turns off the main HUD and then activates the game over HUD
        MainHUD.SetActive(false);
        GameOverUI.SetActive(true);
    }
    
    private void HandleHighScore(int CurrentScore)
    {
        if (CurrentScore > LoggedHighScore)
        {
            PlayerPrefs.SetInt("HighScore", CurrentScore);
        }
        
        // After seeing if the highscore needed to be updated, sets the text on the UI for the game over screen.
        UpdateGameOverUI(CurrentScore, PlayerPrefs.GetInt("HighScore"));
    }

    private void UpdateGameOverUI(int CurrentScore, int HighScore)
    {
        if (!ScoreText || !HighScoreText) return;
        
        ScoreText.SetText(CurrentScore.ToString());
        HighScoreText.SetText(HighScore.ToString());
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
