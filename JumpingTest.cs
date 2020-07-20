using UnityEngine;

public class JumpingTest : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private BoxCollider2D boxCollider2D;
    [SerializeField] private LayerMask platformsLayerMask;


    private void Awake()
    {
        rb2D = transform.GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            float jumpVelocity = 10f;
            rb2D.velocity = Vector2.up * jumpVelocity;
        }


    }

    private bool IsGrounded()
    {
        // Using the ray cast component, look at the collider and test a distance away (0.1f)
        // if there is an object/surface below. This will return 'true' if so.
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f, platformsLayerMask);
        return raycastHit2d.collider != null;
    }
}
