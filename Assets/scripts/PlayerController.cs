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
    // public AudioSource JumpAudio;
    // public AudioSource HurtAudio;
    // public AudioSource EatAudio;
    // public AudioSource DeadAudio;
    public Transform cellingChack;
    public Transform groundCheck;

    public bool isHurt;

    public bool isGround;
    public bool isJump;

    bool jumpPressed;
    int jumpCount;
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();

    }

    void Update()
    {
        if(Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }
        Crouch();
        CherryNumber.text = Cherry.ToString();
    }
    void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
        if(!isHurt)
        {
            GroundMovement();
            Jump();
        }
        SwitchAnim();
    }


    void GroundMovement()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        m_rigidbody.velocity = new Vector2(horizontalMove * speed, m_rigidbody.velocity.y);
        if (horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }
    }

    void Jump()
    {
        if(isGround)
        {
            jumpCount = 2;
            isJump = false;
        }
        if(jumpPressed && isGround)
        {
            isJump = true;
            m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, jumpforce);
            jumpCount--;
            jumpPressed = false;
            SoundManager.instance.JumpAudio();
        }
        else if(jumpPressed && jumpCount > 0 && !isGround)
        {
            m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, jumpforce);
            jumpCount--;
            jumpPressed = false;
            SoundManager.instance.JumpAudio();
        }
    }




    void SwitchAnim()
    {
        m_animator.SetFloat("Running", Mathf.Abs(m_rigidbody.velocity.x));
        if(isGround)
        {
            m_animator.SetBool("Falling", false);
        }
        else if(!isGround && m_rigidbody.velocity.y > 0)
        {
            m_animator.SetBool("Jumping", true);
        }
        else if(m_rigidbody.velocity.y < 0)
        {
            m_animator.SetBool("Jumping", false);
            m_animator.SetBool("Falling", true);
        }
        if (isHurt == true)
        {
            Debug.Log("Hurt1");
            m_animator.SetBool("Hurt", true);
            m_animator.SetFloat("Running", 0f);
            if (Mathf.Abs(m_rigidbody.velocity.x) < 0.1f)
            {
                m_animator.SetBool("Hurt", false);
                isHurt = false;
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player");
        if (other.tag == "Collection")
        {
            SoundManager.instance.CherryAudio();
            other.GetComponent<Animator>().Play("Cherryget");
            
        }
        if(other.tag == "DeadLine")
        {
            GetComponent<AudioSource>().enabled = false;
            SoundManager.instance.GameoverAudio();
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
                SoundManager.instance.HurtAudio();
            }
            else if (transform.position.x > other.gameObject.transform.position.x)
            {
                m_rigidbody.velocity = new Vector2(10, m_rigidbody.velocity.y);
                isHurt = true;
                SoundManager.instance.HurtAudio();
            }

        }
    }


    void Crouch()
    {
        if (!Physics2D.OverlapCircle(cellingChack.position, 0.1f, ground))
        {
            if (Input.GetButton("Crouch1"))
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
