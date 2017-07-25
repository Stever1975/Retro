using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class Player : MonoBehaviour
{
    public UIManager ui;
    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool jump = false;
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    public Transform groundCheck;
    public bool hasKey;
    private bool moveLeft, moveRight;
    private bool grounded = true;
    private Animator anim;
    private Rigidbody2D rb2d;
    public VirtualJoystick moveJoystick;
    public bool isReleased = false;
    public GameObject orb;
    private int orb_direction = 1;
    public GameObject Projectile;
    // Use this for initialization
    void Awake()
    {
        Time.timeScale = 1f;
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        ui = GameObject.FindWithTag("UI").GetComponent<UIManager>();
        GameObject.Find("Jump").GetComponent<Button>().onClick.AddListener(() => Jump());
    }


    void Jump()
    {
        if (grounded)
        {
            anim.SetTrigger("Jump");
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }



    public void SetMoveLeft(bool moveLeft)
    {
        this.moveLeft = moveLeft;
        this.moveRight = !moveLeft;
    }

    public void StopMoving()
    {
        this.moveLeft = false;
        this.moveRight = false;
    }

    public void joystick()
    {
        float h = 0;
        if (moveRight)
        {
            h = 1;
        }
        else if (moveLeft)
        {
            h = -1;
        }
        else
        {
            h = 0;
        }

   

        anim.SetFloat("Speed", Mathf.Abs(h));

        if (h * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

        if (h > 0 && !facingRight)
        {
            Flip();

        }

        else if (h < 0 && facingRight) Flip();
    }

    public void keyboard()
    {
        float h = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(h));

        if (h * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

        if (h > 0 && !facingRight)
        {
            Flip();

        }

        else if (h < 0 && facingRight) Flip();
    }


    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }

        if (Input.GetButtonDown("Fire1") && isReleased == false)
        {
            if (facingRight)
            {
                orb_direction = 1;
                float y_new = this.transform.position.y + 1;
                float x_new = this.transform.position.x + 3;
                orb = (GameObject)Instantiate(Projectile, new Vector3(x_new, y_new), Quaternion.identity);
                isReleased = true;
            }
            else
            {

                orb_direction = -1;
                float y_new = this.transform.position.y + 1;
                float x_new = this.transform.position.x - 3;
                orb = (GameObject)Instantiate(Projectile, new Vector3(x_new, y_new), Quaternion.identity);
                isReleased = true;
            }
            isReleased = true;
        }

    }

   

    public void BouncePlayerWithBouncy(float force)
    {
      //  if (grounded)
        {
            anim.SetTrigger("Jump");
            rb2d.AddForce(new Vector2(0f, force));
            jump = false;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(h));

        if (moveLeft == false && moveRight == false)
        {
            keyboard();
        }
        if (moveLeft == true || moveRight == true)
        {
            joystick();
        }
        if (isReleased)
        {
            if (Math.Abs(orb.transform.position.x - this.transform.position.x) > 7)
                orb_direction = -1 * orb_direction;



            orb.transform.Translate(new Vector3(orb_direction, 0) * Time.deltaTime * 5);


        }
        if (moveJoystick.InputDirection != Vector3.zero)
        {
            h = moveJoystick.InputDirection.x;
        }

        if (h * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

        if (jump)
        {
            anim.SetTrigger("Jump");
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Key")
        { 
            
            hasKey = true;
            other.gameObject.SetActive(false);
            ui.IncrementScore(2);
           

        }
        else if (other.tag == "Diamond")
        {
            other.gameObject.SetActive(false);
            ui.IncrementScore(2);
        }

        else if (other.tag == "orb")
        {
            isReleased = false;
            other.gameObject.SetActive(false);
            ui.IncrementScore(2);
          

        }

        else if (other.tag == "Penguin")
        {
            ui.DecrementLife(20);


        }

        else if (other.tag == "Drill")
        {
            ui.DecrementLife(50);


        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
                 if (collision.gameObject.tag == "Penguin")
        {
            ui.DecrementLife(20);


        }

        else if (collision.gameObject.tag == "Drill")
        {
            ui.DecrementLife(50);


        }

        else if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = collision.transform;


        }

    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}