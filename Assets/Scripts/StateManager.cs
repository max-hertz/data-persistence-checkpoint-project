using System.IO;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance;

    private string PlayerName;
    private string HighScorePlayerName;
    private int HighScoreScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadSaveData();
    }

    public void SetPlayerName(string name) {
        PlayerName = name;
    }

    public string GetPlayerName() {
        return PlayerName;
    }

    public void SetHighScorePlayerName(string name) {
        HighScorePlayerName = name;
    }

    public string GetHighScorePlayerName() {
        return HighScorePlayerName;
    }

    public void SetHighScoreScore(int score) {
        HighScoreScore = score;
    }

    public int GetHighScoreScore() {
        return HighScoreScore;
    }

    [System.Serializable]
    class SaveData 
    {
        public string PlayerName;
        public string HighScorePlayerName;
        public int HighScoreScore;      
    }

    public void SaveSaveData()
    {
        SaveData data = new SaveData();
        data.PlayerName = PlayerName;
        data.HighScorePlayerName = HighScorePlayerName;
        data.HighScoreScore = HighScoreScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadSaveData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            PlayerName = data.PlayerName;
            HighScorePlayerName = data.HighScorePlayerName;
            HighScoreScore = data.HighScoreScore;
        }
    }


}
