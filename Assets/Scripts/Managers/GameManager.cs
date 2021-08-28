using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseButton;

    public static GameManager main;
    // Start is called before the first frame update
    void Start()
    {
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void GoToGame()
    {
        SceneManager.LoadScene(1);
    }
    
    public void Loss()
    {
        losePanel.SetActive(true);
        AudioManager.main.PlaySoundEffect(SoundEffects.loss);
        //play sound indicating loss
        //do all the other things for loss
    }

    public void Win()
    {
        winPanel.SetActive(true);
        AudioManager.main.PlaySoundEffect(SoundEffects.win);
        //play sound indicating loss
        //do all the other things for win
    }

    public void Pause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        pausePanel.SetActive(!pausePanel.activeSelf);
        pauseButton.SetActive(!pausePanel.activeSelf);
    }
}
