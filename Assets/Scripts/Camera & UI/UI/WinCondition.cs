using UnityEngine;

public class WinCondition : MonoBehaviour {
    private UIManager uiManager;
    private Animator anim;

    private void Awake() {
        uiManager = FindObjectOfType<UIManager>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            collision.GetComponentInChildren<Health>().DisableControls();
            anim.SetTrigger("Pick");
        }
    }

    public void Deactivate() {
        transform.gameObject.SetActive(false);
        uiManager.Win();
    }
}