using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Die : MonoBehaviour
{

    protected Animator Anim;
    protected AudioSource deathAudio;

    protected virtual void Start()
    {
        Anim = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
    }
    public void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(transform.gameObject);
    }

    public void Jumpon()
    {
        transform.gameObject.GetComponent<Animator>().SetTrigger("Die");
        deathAudio.Play();
    }
}
