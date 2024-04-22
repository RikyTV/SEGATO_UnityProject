using UnityEngine;

public class Health : MonoBehaviour {
    private Animator anim;
    private UIManager uiManager;

    private void Start() {
        anim = GetComponent<Animator>();
    }

    private void Awake() {
        uiManager = FindObjectOfType<UIManager>();
    }

    public void DisableControls() {
        anim.SetBool("Grounded", true);
        transform.GetComponentInParent<PlayerController>().enabled = false;
        transform.GetComponentInParent<PlayerAttack>().enabled = false;
    }

    public void Death() {
        anim.SetBool("Grounded", true);
        DisableControls();
        anim.SetTrigger("Die");
    }

    public void Deactivate() {
        transform.parent.gameObject.SetActive(false);
        uiManager.GameOver();
    }
}
