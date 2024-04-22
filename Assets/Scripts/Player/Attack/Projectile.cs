using UnityEngine;

public class Projectile : MonoBehaviour {
    public float speed;
    public Animator anim;
    public Transform spriteTransform;

    private bool hit;
    private Vector3 direction;
    private CapsuleCollider2D col;

    private void Start () {
        Physics2D.IgnoreLayerCollision(3, 8);
    }

    private void Awake() {
        col = GetComponent<CapsuleCollider2D>();
    }

    private void Update() {
        if (hit)
            return;

        Vector3 movementVector = speed * Time.deltaTime * direction;
        transform.Translate(movementVector, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        hit = true;
        col.enabled = false;
        anim.SetTrigger("Explode");
        if (collision.tag == "Enemy")
            collision.GetComponent<EnemyDeath>().Death();
    }

    public void SetDirection(Vector3 _direction) {
        direction = _direction;
        spriteTransform.right = direction;
        gameObject.SetActive(true);
        hit = false;
        col.enabled = true;
    }
}
