using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Brick[] bricks;
    private Ball ball;
    private Paddle paddle;    
    public int score = 0;
    public int lives = 3;
    public TMP_Text scoreText;
    public TMP_Text levelText;
    public int maxBasamak = 7;
    public GameObject uiItems;
    private int level = 1;
    private int maxLevel = 3;
    private bool paused = false;

    private void Awake() {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(uiItems);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void Start() {
        
        NewGame();
    }
    private void Update() {
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused ? true : false;

            if(paused)
            {
                Time.timeScale = 0;
                uiItems.transform.GetChild(0).gameObject.SetActive(false);
                uiItems.transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                uiItems.transform.GetChild(1).gameObject.SetActive(false);
                uiItems.transform.GetChild(0).gameObject.SetActive(true);
            }
        }

    }
    private void NewGame()
    {
        this.score = 0;
        this.lives = 3;

        LoadLevel();
    }
    private void LoadLevel()
    {
        score = 0;
        scoreText.text = "0000000";
        levelText.text = level.ToString();
        SceneManager.LoadScene("Level" + level.ToString());
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();
        bricks = FindObjectsOfType<Brick>();
    }
    private void ResetLevel()
    {
        ball.ResetBall();
        paddle.ResetPaddle();
    }
    public void Miss()
    {
        lives--;

        if(lives > 0)
            ResetLevel();
        else
            GameOver();
    }
    private void GameOver()
    {
        NewGame();
    }
    private bool AllBricksDestroyed()
    {
        foreach (var item in bricks)
        {
            if(item.gameObject.activeInHierarchy)
                return false;
        }
        return true;
    }
    public void Hit(Brick brick)
    {
        score += brick.pointsPerBrickState;
        var zeroCount = maxBasamak - BasamakHesapla(score);
        string text = "";
        for (int i = 0; i < zeroCount; i++)
        {
            text += "0";
        }

        scoreText.text = text + score.ToString();

        if(AllBricksDestroyed())
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            if(level < maxLevel)
                level++;
            LoadLevel();
        }
    } 
    private int BasamakHesapla(int deger)
    {
        int kacBasamak = 1;
        while(deger > 10)
        {
            kacBasamak++;
            deger = deger / 10;
        }
        return kacBasamak;
    }  
}
