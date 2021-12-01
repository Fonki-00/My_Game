using UnityEngine;


public class Aiv2 : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform player;
    public float speed;
    public float jump;
    private bool movingRight = true;

    public Transform wallCheck;
    public float checkRadius = 0.5f;
    public LayerMask whatIsWall;
    private bool isTouchingWall;

    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask whatIsGround;

    public Transform platformCheckUp;
    public float rayDistance;

    public float panicDistance;

    public float cooldownStart=2;
    public float cooldownEnd =0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {

        Patrol();
        PlayerDetect();
        
    }
    private void Run()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, checkRadius, whatIsWall);

        if (isTouchingWall == true)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else if (movingRight == false)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
        if (Time.time > cooldownEnd)
        {
            if (movingRight == true && transform.position.x < player.position.x && isTouchingWall == false)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;

                cooldownEnd = Time.time + cooldownStart;

            }
            else if (movingRight == false && transform.position.x > player.position.x && isTouchingWall == false)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;

                cooldownEnd = Time.time + cooldownStart;
            }
        }
        
    }
    private void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (isGrounded == true)
        {
            rb.velocity = Vector2.up * jump;
        }
    }
    private void PlatformDetect()
    {
        RaycastHit2D isPlatformUp = Physics2D.Raycast(platformCheckUp.position, Vector2.up, rayDistance, whatIsGround);
        if (isPlatformUp == true)
        {
            Jump();
        }

    }
    private void Patrol()
    {
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, checkRadius, whatIsWall);
        transform.Translate(Vector2.right * speed / 4 * Time.deltaTime);

        if (isTouchingWall == true)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, -0, 0);
                movingRight = true;
            }
        }
    }
    private void PlayerDetect()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance < panicDistance)
        {
            Run();
            PlatformDetect();
            PlayerEscape(distance);
        }
    }
    private void PlayerEscape(float d)
    {
        if (d < panicDistance / 3)
        {
            if (player.position.y <= transform.position.y)
            {
                Jump();
            }
            if (player.position.y >= transform.position.y && isGrounded == false)
            {
                Slam();
            }
            
        }
    }
    private void Slam()
    {
        rb.velocity = Vector2.down * jump;
    }
}
