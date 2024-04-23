using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonControls : MonoBehaviour {
    private UIManager uiManager;

    private void Awake() {
        uiManager = FindObjectOfType<UIManager>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !uiManager.GameOverScreen.activeSelf && !uiManager.WinScreen.activeSelf)
            Pause();
    }

    public void Pause() {
            uiManager.Pause();
    }

    public void Resume() {
        Time.timeScale = 1f;
        uiManager.ActivatePlayerScripts();
        uiManager.PauseScreen.SetActive(false);
    }

    public void ResetTheGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitTheGame() {
        Application.Quit();
    }
}