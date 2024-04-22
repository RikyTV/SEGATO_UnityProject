using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed;
    public float jump;
    public float doubleJump;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public float coyoteTime;
    
    private float coyoteCounter;
    private float wallJumpCooldown;
    private bool canDoubleJump;
    private Animator anim;
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    private Vector3 mousePosition;
    Vector2 direction;

    private void Start() {
        Physics2D.IgnoreLayerCollision(3, 8);
        body = transform.GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        boxCollider = transform.GetComponent<BoxCollider2D>();
    }

    private void Update() {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        //Flip Player
        if (horizontalInput > 0)
            transform.GetChild(0).localScale = new Vector3(1, 1, 1);
        else if (horizontalInput < 0) 
            transform.GetChild(0).localScale = new Vector3(-1, 1, 1);
        

        //Set animators parameters
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", IsGrounded());
        if (!IsGrounded())
            anim.SetTrigger("Jump");

        if (IsGrounded())
            coyoteCounter = coyoteTime;
        else 
            coyoteCounter -= Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        //Wall jump & double jump logic
        if (wallJumpCooldown > 0.2f) {
            if (!IsGrounded()) {
                if (!canDoubleJump)
                    body.velocity = new Vector2(body.velocity.x, body.velocity.y);
                else
                    body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
                if (Input.GetMouseButtonDown(1) && canDoubleJump)
                    DoubleJump();
            } else
                body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
            if (OnWall() && !IsGrounded())
                body.velocity = new Vector2(horizontalInput, -1.5f);
        } else
            wallJumpCooldown += Time.deltaTime;
    }

    private void Jump() {
        if (coyoteCounter <= 0 && !OnWall()) return;

        coyoteCounter = 0;

        if ((OnWall() && !IsGrounded()) || (OnWall() && IsGrounded())) {
            if (IsGrounded()) {
                wallJumpCooldown = 0;
                body.velocity = new Vector2(Mathf.Sign(transform.GetChild(0).localScale.x), jump);
                canDoubleJump = true;
            } else {
                wallJumpCooldown = 0;
                body.velocity = new Vector2(-Mathf.Sign(transform.GetChild(0).localScale.x) * (speed + 1.25f), (jump - 3));
                canDoubleJump = true;
                if (-Mathf.Sign(transform.GetChild(0).localScale.x) < 0)
                    transform.GetChild(0).localScale = new Vector3(-1, 1, 1);
                else
                    transform.GetChild(0).localScale = new Vector3(1, 1, 1);
            }
        } else {
            body.velocity = new Vector2(body.velocity.x, jump);
            canDoubleJump = true;
        }
    }

    private Vector2 GetMouseDirectionInverted() {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = -(mousePosition - transform.GetChild(1).position);
        return direction;
    }

    private void DoubleJump() {
        direction = GetMouseDirectionInverted();
        direction = direction.normalized;
        direction *= doubleJump;
        body.velocity = direction;
        if (-Mathf.Sign(transform.GetChild(0).localScale.x) < direction.x)
            transform.GetChild(0).localScale = new Vector3(-1, 1, 1);
        else
            transform.GetChild(0).localScale = new Vector3(1, 1, 1);

        canDoubleJump = false;
    }


    private bool IsGrounded() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool OnWall() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.GetChild(0).localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool CanAttack() {
        direction = GetMouseDirectionInverted();
        direction = direction.normalized;
        direction.x *= 10;
        if (Mathf.Sign(transform.GetChild(0).localScale.x) <= direction.x)
            transform.GetChild(0).localScale = new Vector3(1, 1, 1);
        else
            transform.GetChild(0).localScale = new Vector3(-1, 1, 1);
        return (IsGrounded() && body.velocity.x == 0);
    }
}