using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

enum PlaySoundState {
    IDLE,
    PLAYING
}

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool levelWin;

    public GameObject gameOverPanel;
    public GameObject levelWinPanel;

    public static int CurrentLevelIndex;
    public static int totalPassedRings;

    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;
    public Slider ProgressBar;
    public AudioClip gameOverSound;
    public AudioClip winLevelSound;
    private AudioSource audioSource;
    private PlaySoundState soundState = PlaySoundState.IDLE;

    public void Awake () {
        CurrentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        Time.timeScale = 1;
        totalPassedRings = 0;
        gameOver = false;
        levelWin = false;
        soundState = PlaySoundState.IDLE;
    }

    private void Update() {
        if(gameOver) {
            if (soundState == PlaySoundState.IDLE) {
                audioSource.PlayOneShot(gameOverSound, 0.5f);
                soundState = PlaySoundState.PLAYING;
            }

            gameOverPanel.SetActive(true);
            if(Input.GetMouseButtonDown(0)) {
                SceneManager.LoadScene(0);
            }
        }

        currentLevelText.text = CurrentLevelIndex.ToString();
        nextLevelText.text = (CurrentLevelIndex + 1).ToString();

        // update our slider
        float progress = totalPassedRings *1f / FindObjectOfType<HelixManager>().GetTotalRings();
        ProgressBar.value = progress;

        if(levelWin) {
            if (soundState == PlaySoundState.IDLE) {
                audioSource.PlayOneShot(winLevelSound, 0.5f);
                soundState = PlaySoundState.PLAYING;
            }
            PlayerPrefs.SetInt("CurrentLevelIndex", 5);
            levelWinPanel.SetActive(true);
            if(Input.GetMouseButtonDown(0)) {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
