using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    public Collider2D coll;
    public Collider2D collon;
    public float speed;
    public float jumpforce;
    private Animator m_animator;
    public LayerMask ground;
    public int Cherry = 0;
    public Text CherryNumber;
    private Animator Enemy_Animator;
    public AudioSource JumpAudio;
    public AudioSource HurtAudio;
    public AudioSource EatAudio;
    public AudioSource DeadAudio;
    public Transform cellingChack;

    public Joystick joystick;

    private bool isHurt;
    //public Transform groundCheck;

    //public bool isGround, isJump;

    //bool jumpPressed;
    //int jumpCount;
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }

    // void Update()
    // {
    //     if(Input.GetButtonDown("Jump") && jumpCount > 0)
    //     {
    //         jumpPressed = true;
    //     }
    // }
    // void Update()
    // {
        
    // }
    void FixedUpdate()
    {
        // isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);

        //Jump();
        if (!isHurt)
        {
            GroundMovement();
        }


        SwitchAnim();
    }

    void Update()
    {
        Jump();
        Crouch();
        CherryNumber.text = Cherry.ToString();
    }

    void GroundMovement()
    {
        // 1float horizontalMove = Input.GetAxisRaw("Horizontal");
        //float horizontalMove = Input.GetAxis("Horizontal");
        float horizontalMove = joystick.Horizontal;
        //float facedircetion = Input.GetAxisRaw("Horizontal");
        float facedircetion = joystick.Horizontal;

        // 1m_rigidbody.velocity = new Vector2(horizontalMove * speed, m_rigidbody.velocity.y);
        if (horizontalMove != 0)
        {
            m_rigidbody.velocity = new Vector2(horizontalMove * speed * Time.fixedDeltaTime, m_rigidbody.velocity.y);
            // 1transform.localScale = new Vector3(horizontalMove, 1, 1);
            m_animator.SetFloat("Running", Mathf.Abs(facedircetion));
        }
        if (facedircetion > 0f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (facedircetion < 0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

    }

    void SwitchAnim()
    {
        if (m_rigidbody.velocity.y < 0.1f && !coll.IsTouchingLayers(ground))
        {
            m_animator.SetBool("Falling", true);
        }
        if (m_animator.GetBool("Jumping"))
        {
            if (m_rigidbody.velocity.y < 0)
            {

                m_animator.SetBool("Jumping", false);
                m_animator.SetBool("Falling", true);
            }
        }
        else if (isHurt == true)
        {

            m_animator.SetBool("Hurt", true);
            m_animator.SetFloat("Running", 0f);
            if (Mathf.Abs(m_rigidbody.velocity.x) < 0.3f)
            {
                m_animator.SetBool("Hurt", false);
                isHurt = false;
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            m_animator.SetBool("Falling", false);
        }


    }

        void Jump()
        {
            if (joystick.Vertical > 0.5f && coll.IsTouchingLayers(ground))
            {
            JumpAudio.Play();
            m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, jumpforce * Time.deltaTime);
            m_animator.SetBool("Jumping", true);

            }
            Crouch();
        }

    // void SwitchAnim()
    // {
    //     m_animator.SetFloat("Running", Mathf.Abs(m_rigidbody.velocity.x));
    //     if(isGround)
    //     {
    //         m_animator.SetBool("Falling", false);
    //     }
    //     else if(!isGround && m_rigidbody.velocity.y < 0)
    //     {
    //         m_animator.SetBool("Jumping", true);
    //     }
    //     else if(m_rigidbody.velocity.y < 0)
    //     {
    //         m_animator.SetBool("Jumping", false);
    //         m_animator.SetBool("Falling", true);
    //     }
    // }

    // void Jump()
    // {
    //     if(isGround)
    //     {
    //         jumpCount = 1;
    //         isJump = false;
    //     }
    //     if(jumpPressed && isGround)
    //     {
    //         isJump = true;
    //         m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, jumpforce);
    //         jumpCount--;
    //         jumpPressed = false;
    //     }
    //     else if(jumpPressed && jumpCount > 0 && !isGround)
    //     {
    //         m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, jumpforce);
    //         jumpCount--;
    //         jumpPressed = false;
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player");
        if (other.tag == "Collection")
        {
            EatAudio.Play();
            other.GetComponent<Animator>().Play("Cherryget");
            
        }
        if(other.tag == "DeadLine")
        {
            GetComponent<AudioSource>().enabled = false;
            DeadAudio.Play();
            Invoke(nameof(Restart), 2f);
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemies")
        {
            if (m_animator.GetBool("Falling"))
            {
                Enemy_Die Die = other.gameObject.GetComponent<Enemy_Die>();
                Die.Jumpon();
                m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, jumpforce * Time.deltaTime);
                m_animator.SetBool("Jumping", true);
            }
            else if (transform.position.x < other.gameObject.transform.position.x)
            {
                m_rigidbody.velocity = new Vector2(-10, m_rigidbody.velocity.y);
                isHurt = true;
                HurtAudio.Play();
            }
            else if (transform.position.x > other.gameObject.transform.position.x)
            {
                m_rigidbody.velocity = new Vector2(10, m_rigidbody.velocity.y);
                isHurt = true;
                HurtAudio.Play();
            }

        }
    }


    void Crouch()
    {
        if (!Physics2D.OverlapCircle(cellingChack.position, 0.1f, ground))
        {
            if (joystick.Vertical < -0.5f)
            {
                m_animator.SetBool("Crouching", true);
                collon.enabled = false;
            }
            else
            {
                m_animator.SetBool("Crouching", false);
                collon.enabled = true;
            }
        }

    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CherryCount()
    {
        Cherry += 1;
    }

}
