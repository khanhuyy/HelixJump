using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool levelWin;

    public GameObject gameOverPanel;
    public GameObject levelWinPanel;

    public static int CurrentLevelIndex;
    public static int noOfPassingRings;

    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;
    public Slider ProgressBar;

    public void Awake () {
        CurrentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);
    }

    private void Start() {
        Time.timeScale = 1;
        noOfPassingRings = 0;
        gameOver = false;
        levelWin = false;
    }

    private void Update() {
        if(gameOver) {
            gameOverPanel.SetActive(true);
            if(Input.GetMouseButtonDown(0)) {
                SceneManager.LoadScene(0);
            }
        }

        currentLevelText.text = CurrentLevelIndex.ToString();
        nextLevelText.text = (CurrentLevelIndex + 1).ToString();

        // update our slider
        int progress = noOfPassingRings * 100 / FindObjectOfType<HelixManager>().noOfRings;
        ProgressBar.value = progress;

        if(levelWin) {
            levelWinPanel.SetActive(true);
            if(Input.GetMouseButtonDown(0)) {
                SceneManager.LoadScene(0);
            }
        }
    }
}
