using UnityEngine;

public class UIManager : MonoBehaviour {
    public GameObject GameOverScreen;
    public GameObject WinScreen;
    public GameObject PauseScreen;

    private void Start() {
        Time.timeScale = 1;
        GameOverScreen.SetActive(false);
        WinScreen.SetActive(false);
        PauseScreen.SetActive(false);
    }

    public void Pause() {
        PauseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GameOver() {
        GameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Win() {
        WinScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}