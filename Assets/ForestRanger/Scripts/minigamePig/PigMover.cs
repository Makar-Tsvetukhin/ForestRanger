using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PigMover : MonoBehaviour
{
    public float speed = 2f;
    public Collider2D mazeCollider;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveDirection;
    private bool isMoving;
    private Vector2 swipeStart;

    private Vector2 targetNodePosition;
    private bool centering;

    public bool IsMoving
    {
        get { return isMoving; }
        set { isMoving = value; }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.touchCount > 0 && !centering)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                swipeStart = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector2 swipe = touch.position - swipeStart;
                if (swipe.magnitude > 50f)
                {
                    if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
                        moveDirection = swipe.x > 0 ? Vector2.right : Vector2.left;
                    else
                        moveDirection = swipe.y > 0 ? Vector2.up : Vector2.down;

                    isMoving = true;
                    UpdateAnimation(moveDirection);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Node"))
        {
            targetNodePosition = collision.transform.position;
            centering = true;
            isMoving = false;
            moveDirection = Vector2.zero;
            animator.Play("Idle"); 
        }
    }

    void FixedUpdate()
    {
        if (centering)
        {
            rb.position = Vector2.MoveTowards(rb.position, targetNodePosition, speed * Time.fixedDeltaTime);

            if (Vector2.Distance(rb.position, targetNodePosition) < 0.01f)
            {
                rb.position = targetNodePosition;
                centering = false;
            }
            return;
        }

        if (!isMoving)
            return;

        Vector2 nextPos = rb.position + moveDirection * speed * Time.fixedDeltaTime;

        if (!mazeCollider.OverlapPoint(nextPos))
            rb.MovePosition(nextPos);
        else
        {
            isMoving = false;
            animator.Play("Idle");
        }
    }

    private void UpdateAnimation(Vector2 direction)
    {
        if (direction == Vector2.up)
            animator.Play("MoveUp");
        else if (direction == Vector2.down)
            animator.Play("MoveDown");
        else if (direction == Vector2.left)
            animator.Play("MoveLeft");
        else if (direction == Vector2.right)
            animator.Play("MoveRight");
    }
}
