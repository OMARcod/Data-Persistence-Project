using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class DataManager : MonoBehaviour
{

    public static DataManager Instance;

    public int PlayerScore;
    public string PlayerName;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadScore();
        LoadName();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Serializable]

    class SaveData
    {
        public string PlayerName;
        public int PlayerScore;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.PlayerScore = PlayerScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    } 


    public bool LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            PlayerScore = data.PlayerScore;

            return true;
        }
        else //or make it boolean .. return false if there is no path .. next function will check and will change the text
        {
            PlayerScore = 0;
            return false;
        }
    }

    public void SaveName()
    {
        SaveData data = new SaveData();
        data.PlayerName = PlayerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public bool LoadName()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            PlayerName = data.PlayerName;

            return true;
        }
        else //or make it boolean .. return false if there is no path .. next function will check and will change the text
        {
            return false;
        }
    }
}
