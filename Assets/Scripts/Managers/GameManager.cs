using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject losePanel = null;
    [SerializeField] private GameObject winPanel = null;
    [SerializeField] private GameObject pausePanel = null;
    [SerializeField] private GameObject pauseButton = null;
    [SerializeField] private TMPro.TextMeshProUGUI _levelDisplay;

    private int _level = 0;

    public static GameManager main;

    private void Awake()
    {
        main = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (losePanel != null)
            losePanel.SetActive(false);

        if (winPanel != null)
            winPanel.SetActive(false);

        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (pauseButton != null)
            pauseButton.SetActive(true);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void GoToGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void NextLevel()
    {
        /*if (_level == 2)
        {
            Win();
            return;
        }*/

        _level++;
        _levelDisplay.text = "Level:\n" + _level;
        DungeonManager.main.NewDungeon(true);
    }
    
    public void Loss()
    {
        Time.timeScale = 0;
        losePanel.SetActive(true);
        AudioManager.main.PlaySoundEffect(SoundEffects.loss);
    }

    public void Win()
    {
        Time.timeScale = 0;
        winPanel.SetActive(true);
        AudioManager.main.PlaySoundEffect(SoundEffects.win);
    }

    public void Pause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        pausePanel.SetActive(!pausePanel.activeSelf);
        pauseButton.SetActive(!pausePanel.activeSelf);
    }
}
