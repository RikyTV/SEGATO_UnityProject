using UnityEngine;

public class UIManager : MonoBehaviour {
    public GameObject GameOverScreen;
    public GameObject WinScreen;
    public GameObject PauseScreen;
    public GameObject Player;

    private void Start() {
        Time.timeScale = 1;
        ActivatePlayerScripts();
        GameOverScreen.SetActive(false);
        WinScreen.SetActive(false);
        PauseScreen.SetActive(false);
    }

    public void Pause() {
        DeactivatePlayerScripts();
        PauseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GameOver() {
        DeactivatePlayerScripts();
        GameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Win() {
        DeactivatePlayerScripts();
        WinScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void DeactivatePlayerScripts() {
        Player.GetComponent<PlayerAttack>().enabled = false;
        Player.GetComponent<PlayerController>().enabled = false;
        Player.GetComponentInChildren<Firepoint>().enabled = false;
    }

    public void ActivatePlayerScripts() {
        Player.GetComponent<PlayerAttack>().enabled = true;
        Player.GetComponent<PlayerController>().enabled = true;
        Player.GetComponentInChildren<Firepoint>().enabled = true;
    }
}