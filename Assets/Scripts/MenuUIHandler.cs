using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class MenuUIHandler : MonoBehaviour
{
    //instaneate a new Name and Score script

    // Start is called before the first frame update

    public InputField PlayerName;
    public Text PlayerScore;


    
    void Start()
    {
    }
    void LoadHighScore()
    {
        string playerName;
        int playerScore;

        if (DataManager.Instance.LoadName() && DataManager.Instance.LoadScore())
        {
            playerName = DataManager.Instance.PlayerName;
            playerScore = DataManager.Instance.PlayerScore;
            PlayerScore.text = "Best Score: " + playerName + " : " + playerScore;
        }
        else
        {
            PlayerScore.text = "Best Score: " + " : 0";
        }

    }

    //function for start
    public void StartNew()
    {
        //Save the name form the input filde
        DataManager.Instance.PlayerName = PlayerName.text;
        SceneManager.LoadScene(1);
    }


    //function for Exit
    public void Exit()
    {
        DataManager.Instance.SaveScore();
        DataManager.Instance.SaveName();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    //function to save Name
    //function to load Name

    //function to load Score
    // Update is called once per frame
    void Update()
    {
        
    }
}
