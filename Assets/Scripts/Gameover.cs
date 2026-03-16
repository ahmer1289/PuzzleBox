using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameOverPanel.SetActive(true); 
            Time.timeScale = 0f; 
        }
    }

    public void RestartGame()
    {
        PuzzleButton.solvedCount = 0;
        Time.timeScale = 1f; 
        PlayerPrefs.DeleteAll(); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}
