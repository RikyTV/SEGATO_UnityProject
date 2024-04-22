using UnityEngine;

public class Enemy_sideways : MonoBehaviour {
    public float movementDistance;
    public float speed;

    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;
    private float scaleX;
    private bool reverse = true;

    private void Awake() {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
        scaleX = transform.localScale.x;
        if (movementDistance >= 0)
            reverse = false;
    }

    private void Update() {
        if (!reverse) {
            if (movingLeft) {
                if (transform.position.x > leftEdge) {
                    transform.localScale = new Vector3(-scaleX, transform.localScale.y, transform.localScale.z);
                    transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
                } else
                    movingLeft = false;
            } else if (transform.position.x < rightEdge) {
                transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            } else
                movingLeft = true;
        } else {
            if (movingLeft) {
                if (transform.position.x < leftEdge) {
                    transform.localScale = new Vector3(-scaleX, transform.localScale.y, transform.localScale.z);
                    transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
                } else
                    movingLeft = false;
            } else if (transform.position.x > rightEdge) {
                transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            } else
                movingLeft = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player")
            collision.GetComponentInChildren<Health>().Death();
    }
}
