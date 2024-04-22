using System.Transactions;
using UnityEngine;

public class EnemyDeath : MonoBehaviour {
    // Start is called before the first frame update
    private Animator anim;
    private void Start() {
        anim = GetComponent<Animator>();
    }

    public void Death() {
        anim.SetTrigger("Dead");
        transform.GetComponent<BoxCollider2D>().enabled = false;
        transform.GetComponent<Enemy_sideways>().enabled = false;
    }

    public void Deactivate() {
        transform.gameObject.SetActive(false);
    }
}
