using UnityEngine;


public class Ai3 : MonoBehaviour
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

    public float cooldownStartAvoid = 2;
    public float cooldownEndAvoid = 0;

    public float cooldownStartJump = 1;
    public float cooldownEndJump = 0;

    public LayerMask whatIsChicken;
    private bool isChicken;
    public Transform chickenCheck;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Safe();
        Dangerous();
    }
    private void Safe()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance >= panicDistance)
        {
            MoveSafe();
            PlatformDetect();
        }

    }
    private void Dangerous()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance < panicDistance)
        {
            Move();
            PlatformDetect();
            PlayerEscape();
        }
    }
    private void PlatformDetect()
    {
        RaycastHit2D isPlatformUp = Physics2D.Raycast(platformCheckUp.position, Vector2.up, rayDistance, whatIsGround);
        if (isPlatformUp == true && player.position.y <= transform.position.y+2)
        {
            Jump();
        }

    }
    private void PlayerEscape()
    {
            if (transform.position.y + 2 > player.position.y)
            {
                Jump();

                if (Time.time > cooldownEndJump)
                {
                    if (player.position.y >= transform.position.y+2 && isGrounded == false)
                    {
                        Dive();
                        cooldownEndJump = Time.time + cooldownStartJump;
                    }
                }
            }
    }
    private void Dive()
    {
        rb.velocity = Vector2.down * jump;
    }
    private void Move()
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
        else if (isTouchingWall == false)
        {
            isChicken = Physics2D.OverlapCircle(chickenCheck.position, checkRadius, whatIsChicken);
            if (isChicken==true)
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
            if (Time.time > cooldownEndAvoid)
            {
                if (transform.position.y + 5 > player.position.y && transform.position.y - 1 < player.position.y)
                {
                    if (movingRight == true && transform.position.x < player.position.x)
                    {
                        transform.eulerAngles = new Vector3(0, -180, 0);
                        movingRight = false;

                        cooldownEndAvoid = Time.time + cooldownStartAvoid;
                    }
                    else if (movingRight == false && transform.position.x > player.position.x)
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        movingRight = true;

                        cooldownEndAvoid = Time.time + cooldownStartAvoid;
                    }
                }
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
    private void MoveSafe()
    {
        transform.Translate(Vector2.right * speed/3 * Time.deltaTime);
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
        else if (isTouchingWall == false)
        {
            if (Time.time > cooldownEndAvoid)
            {
                if (transform.position.y + 5 > player.position.y && transform.position.y - 1 < player.position.y)
                {
                    if (movingRight == true && transform.position.x < player.position.x)
                    {
                        transform.eulerAngles = new Vector3(0, -180, 0);
                        movingRight = false;

                        cooldownEndAvoid = Time.time + cooldownStartAvoid;
                    }
                    else if (movingRight == false && transform.position.x > player.position.x)
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        movingRight = true;

                        cooldownEndAvoid = Time.time + cooldownStartAvoid;
                    }
                }
            }
        }
    }
}
