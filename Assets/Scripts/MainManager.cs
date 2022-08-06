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
    public Text HighScoreAndName;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    private int oldScore;
    private string oldName;
    private string newName;
    private int newScore;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("The Game has started");
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,6,6};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint); //Learn
            }
        }
        newName = DataManager.Instance.PlayerName;
        UpdateNameAndHighScore();

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

                Ball.transform.SetParent(null); //Learn
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
 
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

 

    void UpdateNameAndHighScore()
    {
        

        //DataManager.Instance.LoadNameAndScore();

        oldScore = DataManager.Instance.PlayerScore;
        oldName = DataManager.Instance.PlayerName;


        if (newScore <= 0) //When there is no old Score
        {
            Debug.Log("(newScore <= 0)..." + "NewScore: " + newScore + "|| NewName: " + newName + "|| OldScore: " + oldScore + "|| OldName: " + oldName);
            HighScoreAndName.text = "Best Score: " + oldName + " : " + oldScore;
        }
        else //When there is an old Score
        {
            Debug.Log("(newScore > 0)..." + "NewScore: " + newScore + "|| New Name: " + newName + "|| OldScore: " + oldScore + "|| OldName: " + oldName);
            HighScoreAndName.text = "Best Score: " + newName + " : " + newScore;
        }
    }

    public void GameOver()
    {
        Debug.Log("The Game has Ended");

        newScore = m_Points;

        //save the score
        //save the name
        //Make a function that check if the this is the highst score ..if not dont save
        if (newScore > oldScore )
        {

            DataManager.Instance.PlayerScore = newScore;
            DataManager.Instance.PlayerName = newName;
            DataManager.Instance.SaveNameAndScore();
            Debug.Log("The NewScore: "+ newScore + "> oldScore: " + oldScore + "\n the new Name,Score has been saved");

        }
        else
        {
            Debug.Log("Only Old Score");
        }

        m_GameOver = true;
        GameOverText.SetActive(true);
    }
}
