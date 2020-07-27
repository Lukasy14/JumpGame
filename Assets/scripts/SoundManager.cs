using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource audioSource;
    [SerializeField]
    private AudioClip BGM, jumpAudio, hurtAudio, cherryAudio, gameoverAudio;

    private void Awake() 
    {
        instance = this;
    }
    public void HurtAudio()
    {
        audioSource.clip = hurtAudio;
        audioSource.Play();
    }
    public void JumpAudio()
    {
        audioSource.clip = jumpAudio;
        audioSource.Play();
    }
    public void CherryAudio()
    {
        audioSource.clip = cherryAudio;
        audioSource.Play();
    }
    public void GameoverAudio()
    {
        audioSource.clip = gameoverAudio;
        audioSource.Play();
    }
}
