using UnityEngine;

public class Enemy_vertical : MonoBehaviour {
    public float movementDistance;
    public float speed;

    private bool movingDown;
    private float upperEdge;
    private float lowerEdge;
    private bool reverse = true;

    private void Awake() {
        upperEdge = transform.position.y + movementDistance;
        lowerEdge = transform.position.y - movementDistance;
        if (movementDistance >= 0)
            reverse = false;
    }

    private void Update() {
        if (!reverse) {
            if (movingDown) {
                if (transform.position.y > lowerEdge)
                    transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
                else
                    movingDown = false;
            }
            else if (transform.position.y < upperEdge)
                transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
            else
                movingDown = true;
        }
        else {
            if (movingDown) {
                if (transform.position.y < lowerEdge)
                    transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
                else
                    movingDown = false;
            } else if (transform.position.y > upperEdge)
                transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
            else
                movingDown = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player")
            collision.GetComponentInChildren<Health>().Death();
    }
}