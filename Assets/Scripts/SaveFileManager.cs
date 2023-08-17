using System.IO;
using UnityEngine;

public class SaveFileManager : MonoBehaviour
{
    private string SAVE_FILE_PATH;
    private string OPTIONS_FILE_PATH;
    private void Awake()
    {
        SAVE_FILE_PATH = Path.Combine(Application.persistentDataPath, "player_info.json");
        OPTIONS_FILE_PATH = Path.Combine(Application.persistentDataPath, "options_info.json");
    }
     
    
    public void SavePlayerInfo(UserInfo userInfo)
    {
        string json = JsonUtility.ToJson(userInfo);
        File.WriteAllText(SAVE_FILE_PATH, json);
    }

    public void SaveOptionInfo(string[] optionsNumbers)
    {
        string json = JsonUtility.ToJson(optionsNumbers);
        File.WriteAllText(OPTIONS_FILE_PATH, json);
    }
    //TODO: load volumes for music/sfx
    public string[] LoadOptionInfo()
    {
        string[] optionNumbers = new string[2];

        if (File.Exists(OPTIONS_FILE_PATH))
        {
            string json = File.ReadAllText(OPTIONS_FILE_PATH);
            optionNumbers = JsonUtility.FromJson<string[]>(json);
            Debug.Log("JSON: " + json.ToString());
            Debug.Log("OPTIONNUMBERS: " + optionNumbers.ToString());
            return optionNumbers;
        }
        else
        {
            Debug.Log("Options Save file doesn't exist");
            return null;
        }
    }

    public UserInfo LoadPlayerInfo()
    {
        if (File.Exists(SAVE_FILE_PATH)){
            string json = File.ReadAllText(SAVE_FILE_PATH);
            UserInfo userInfo = JsonUtility.FromJson<UserInfo>(json);
            return userInfo;
        }
        else
        {
            Debug.Log("Player Save file doesn't exist");
            return null;
        }
    }

    public void DeletePlayerInfo()
    {
        if (File.Exists(SAVE_FILE_PATH))
        {
            File.Delete(SAVE_FILE_PATH);
            File.Delete(OPTIONS_FILE_PATH);
            Debug.Log("Deleted both save file and options file");
        }
        else
        {
            Debug.Log("Save and options file doesn't exist");
        }
    }
}
