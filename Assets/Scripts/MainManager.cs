using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text HighScoreText;
    public GameObject GameOverText;
    private string playerName;
    private int highScoreScore;
    private string highScorePlayerName;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        // retrieve player name
        playerName = StateManager.Instance.GetPlayerName();
        AddPoint(0);

        // retrieve high score
        UpdateHighscoreText();

    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }

            UpdateHighScore();
        }
        else if (m_GameOver)
        {
            UpdateHighScore();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StateManager.Instance.SaveSaveData();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        else 
        {
            UpdateHighScore();
        }
    }

    public void Exit() 
    {
        StateManager.Instance.SaveSaveData();
    }


    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points} // {playerName}";
    }

    public void GameOver()
    {
        
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    public void UpdateHighScore() 
    {
        if (m_Points > StateManager.Instance.GetHighScoreScore()) {
            StateManager.Instance.SetHighScoreScore(m_Points);
            StateManager.Instance.SetHighScorePlayerName(playerName);
            UpdateHighscoreText();
        }
    }

    public void UpdateHighscoreText() 
    {
        int hss = StateManager.Instance.GetHighScoreScore();
        string hsn = StateManager.Instance.GetHighScorePlayerName();
        HighScoreText.text = "Highscore: " + hss + " // " + hsn;
    }
}
