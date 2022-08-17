using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class EnterNameScr : MonoBehaviour
{
    public static EnterNameScr PlayerName;

    TMP_InputField playerName;
    public string playerNameStr;
    public string bestPlayerMenu;
    public int bestScoreMenu;

    public TextMeshProUGUI BestScoreText;

    // Start is called before the first frame update
    private void Awake()
    {
        // start of new code
        if (PlayerName != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        PlayerName = this;
        DontDestroyOnLoad(gameObject);
    }

        void Start()
    {
        playerName = GameObject.Find("InputField (TMP)").GetComponent<TMP_InputField>();
        LoadBestPlayer();
        BestScoreText.text = $"Best score: { bestPlayerMenu} - { bestScoreMenu}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InputName()
    {
        playerNameStr = playerName.GetComponent<TMP_InputField>().text;        
    }

    [System.Serializable]
    class SaveData
    {
        public string playerNameStr;
        public string bestPlayer;
        public int bestScore;
    }

    public void SaveName()
    {
        SaveData data = new SaveData();
        data.playerNameStr = playerNameStr;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savename.json", json);
    }
    public void LoadName()
    {
        string path = Application.persistentDataPath + "/savename.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            playerNameStr = data.playerNameStr;
        }        
    }
    public void SaveBestPlayer(string bestPlayer, int bestScore)
    {
        SaveData data = new SaveData();
        data.bestPlayer = bestPlayer;
        data.bestScore = bestScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/bestscore.json", json);
    }
    public void LoadBestPlayer()
    {
        string path = Application.persistentDataPath + "/bestscore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            bestPlayerMenu = data.bestPlayer;
            bestScoreMenu = data.bestScore;
        }
        else
        {
            bestPlayerMenu = "No one";
        }
    }
}
