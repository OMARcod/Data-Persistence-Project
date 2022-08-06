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

    public InputField InputPlayerName;
    public Text PlayerScore;

    void Start()
    {
        //DataManager.Instance.LoadNameAndScore();
        LoadHighScore();
    }
    void LoadHighScore()
    {
        string playerName;
        int playerScore;

        playerName = DataManager.Instance.PlayerName;
        playerScore = DataManager.Instance.PlayerScore;
        PlayerScore.text = "Best Score: " + playerName + " : " + playerScore;
    }
    //function for start
    public void StartNew()
    {
        //Save the name form the input filde
        DataManager.Instance.NewName = InputPlayerName.text;
        DataManager.Instance.PlayerName = InputPlayerName.text;
        SceneManager.LoadScene(1);
    }

    //function for Exit
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
