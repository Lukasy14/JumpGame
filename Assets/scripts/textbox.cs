using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textbox : MonoBehaviour
{
    public GameObject TextBox;

    void Start()
    {
        TextBox.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            TextBox.SetActive(true);
        }

    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            TextBox.SetActive(false);
        }
    }
}
