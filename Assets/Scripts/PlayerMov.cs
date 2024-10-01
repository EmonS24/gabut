using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce = 30f;
    private Rigidbody2D rb;
    private Animator anim;
    private bool grounded;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update() 
    {
        float horiInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horiInput * speed, rb.velocity.y);

    // Moving right: scale is positive (0.75, 0.75, 0.75)
    if (horiInput > 0.01f)
        transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);

    // Moving left: flip x scale to -0.75, but keep y and z positive
    else if (horiInput < -0.01f)
        transform.localScale = new Vector3(-0.75f, 0.75f, 0.75f);

        if (Input.GetKey(KeyCode.Space) && grounded)
        Jump();

        anim.SetBool("run", horiInput != 0);
        anim.SetBool("grounded", grounded);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);;
        anim.SetTrigger("jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }
}

