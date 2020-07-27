using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : Enemy_Die
{
    private Rigidbody2D m_Rigdbody;
    private Animator Anim1;
    private Collider2D coll;
    public Transform leftpoint;
    public Transform rightpoint;
    private bool faceleft = true;
    public float Speed;
    private float leftx;
    private float rightx;
    public float JumpForce;
    public LayerMask Ground;

    protected override void Start()
    {
        base.Start();
        m_Rigdbody = GetComponent<Rigidbody2D>();
        Anim1 = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;

        Destroy(leftpoint.transform.gameObject);
        Destroy(rightpoint.transform.gameObject);
    }


    void Update()
    {
        SwitchAnim();
    }

    void Movement()
    {

        if (faceleft)
        {
            if (coll.IsTouchingLayers(Ground))
            {
                Anim1.SetBool("Jumping", true);
                m_Rigdbody.velocity = new Vector2(-Speed, JumpForce);
                if (transform.position.x < leftx)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    faceleft = false;
                }
            }

        }
        else
        {
            if (coll.IsTouchingLayers(Ground))
            {
                Anim1.SetBool("Jumping", true);
                m_Rigdbody.velocity = new Vector2(Speed, JumpForce);
                if (transform.position.x > rightx)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    faceleft = true;
                }
            }
        }

    }

    void SwitchAnim()
    {
        if(Anim1.GetBool("Jumping"))
        {
            if(m_Rigdbody.velocity.y < 0.1f)
            {
                Anim1.SetBool("Jumping", false);
                Anim1.SetBool("Falling", true);
            }
        }
        if(coll.IsTouchingLayers(Ground) && Anim1.GetBool("Falling") == true)
        {
            Anim1.SetBool("Falling", false);
        }
    }



}
