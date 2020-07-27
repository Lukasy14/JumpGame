using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eagle : Enemy_Die
{
    private Rigidbody2D m_rigidbody;
    //private Collider2D coll;
    public Transform upPoint;
    public Transform downPoint;
    private float upY;
    private float downY;
    public float Speed;
    public bool dao = true;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        m_rigidbody = GetComponent<Rigidbody2D>();
        //coll = GetComponent<Collider2D>();
        upY = upPoint.position.y;
        downY = downPoint.position.y;
        transform.DetachChildren();
        Destroy(upPoint.gameObject);
        Destroy(downPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if(dao == true)
        {

            m_rigidbody.velocity = new Vector2(transform.position.x, Speed);
            if(transform.position.y > upY)
            {
                dao = false;
            }
        }
        else
        {   
            m_rigidbody.velocity = new Vector2(transform.position.x, -Speed);
            if(transform.position.y < downY)
            {
                dao = true;
            }
        }
        
    }
}
