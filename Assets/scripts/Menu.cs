using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioMixer audioMixer;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void UIEnable()
    {
        GameObject.Find("Canvas/MainMenu/UI").SetActive(true);
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void offPause()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void resteGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat("MainVolume", value);
    }
}
