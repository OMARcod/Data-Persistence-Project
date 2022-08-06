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

        LoadNameAndScore();
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

    public void SaveNameAndScore()
    {
        SaveData data = new SaveData();
        data.PlayerScore = PlayerScore;
        data.PlayerName = PlayerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadNameAndScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            PlayerScore = data.PlayerScore;
            PlayerName = data.PlayerName;
        }
        else
        {
            Debug.Log("PathNothFound");
        }
    }
}


