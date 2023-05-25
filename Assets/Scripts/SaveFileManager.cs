using System.IO;
using UnityEngine;

public class SaveFileManager : MonoBehaviour
{
    private string SAVE_FILE_PATH;
    private void Awake()
    {
        SAVE_FILE_PATH = Path.Combine(Application.persistentDataPath, "player_info.json");
    }
     
    
    public void SavePlayerInfo(UserInfo userInfo)
    {
        string json = JsonUtility.ToJson(userInfo);
        File.WriteAllText(SAVE_FILE_PATH, json);
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
            Debug.Log("Save file doesn't exist");
            return null;
        }
    }

    public void DeletePlayerInfo()
    {
        if (File.Exists(SAVE_FILE_PATH))
        {
            File.Delete(SAVE_FILE_PATH);
        }
        else
        {
            Debug.Log("Save file doesn't exist");
        }
    }
}
