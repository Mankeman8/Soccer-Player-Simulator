using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private SaveFileManager saveFileManager;
    [Header("Main Menu Settings")]
    public Sprite[] menuBackgrounds;
    [SerializeField]
    private Image backgroundImage;
    [SerializeField]
    private GameObject playerSelectGO;
    private UserInfo userInfo;
    private bool userLoaded = false;
    private bool mainScreen = true;
    private bool playerSelector = false;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            // If there is no instance, set this instance as the persistent one and don't destroy it
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        //if the current scene is the main menu, change the background
        if (SceneManager.GetActiveScene().name=="MainMenu")
        {
            ChangeBackground();
            mainScreen = true;
            playerSelector = false;
        }

        //check if there's already previous user info and adapt accordingly
        saveFileManager = GameObject.FindObjectOfType<SaveFileManager>();
        userInfo = saveFileManager.LoadPlayerInfo();
        if (userInfo != null)
        {
            userLoaded = true;
        }
        else
        {
            userLoaded = false;
            userInfo = new UserInfo();
        }
    }

    public void SaveUserInfo()
    {
        userInfo.player = new Player();
        userInfo.player.stats = new Stat();
        //Save Icon
        Toggle[] images = GameObject.Find("Player Picture Selector").GetComponentsInChildren<Toggle>();
        for (int i = 0; i < images.Length; i++)
        {
            if (images[i].isOn)
            {
                Image[] tempName = images[i].gameObject.GetComponentsInChildren<Image>();
                for (int j = 0; j < tempName.Length; j++)
                {
                    if (tempName[j].gameObject.name == "Face")
                    {
                        userInfo.icon = tempName[j].GetComponent<Image>();
                    }
                    
                }
            }
        }

        //Save Position
        Toggle[] position = GameObject.Find("Player Position Selector").GetComponentsInChildren<Toggle>();
        for (int i = 0; i < position.Length; i++)
        {
            if (position[i].isOn)
            {
                TextMeshProUGUI[] tempName = position[i].gameObject.GetComponentsInChildren<TextMeshProUGUI>();
                userInfo.player.stats.position = tempName[0].text;
            }
        }
        
        //Save First Name
        TMP_InputField firstName = GameObject.Find("First Name").GetComponent<TMP_InputField>();
        if (firstName.text == "")
        {
            userInfo.player.firstName = "Nasir";
        }
        else
        {
            userInfo.player.firstName = firstName.text;
        }

        //Save Last Name
        TMP_InputField lastName = GameObject.Find("Last Name").GetComponent<TMP_InputField>();
        if (lastName.text == "")
        {
            userInfo.player.lastName = "Omar";
        }
        else
        {
            userInfo.player.lastName = lastName.text;
        }

        //Save Nationality
        Toggle[] nation = GameObject.Find("Nationality").GetComponentsInChildren<Toggle>();
        for (int i = 0; i < nation.Length; i++)
        {
            if (nation[i].isOn)
            {
                userInfo.player.stats.nationality = nation[i].name;
            }
        }
        saveFileManager.SavePlayerInfo(userInfo);
        SceneManager.LoadScene("MainLevel");
        mainScreen = true;
    }

    public void ChangeBackground()
    {
        //change the background using one of the many images we have
        backgroundImage = GameObject.Find("Background Image").GetComponent<Image>();
        backgroundImage.sprite = menuBackgrounds[Random.Range(0, menuBackgrounds.Length)];
    }
    
    public void SpecificBackground(int back)
    {
        //change the background to a specific one we have
        backgroundImage = GameObject.Find("Background Image").GetComponent<Image>();
        backgroundImage.sprite = menuBackgrounds[back];
    }

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            return;
        }
        if(mainScreen)
        {
            return;
        }

        if(playerSelector)
        {
            TMP_InputField firstName = GameObject.Find("First Name").GetComponent<TMP_InputField>();
            TMP_InputField lastName = GameObject.Find("Last Name").GetComponent<TMP_InputField>();
            Button nextButton = GameObject.Find("Next Button").GetComponent<Button>();
            if(CheckInputField(firstName, lastName))
            {
                nextButton.interactable = true;
            }
            else
            {
                nextButton.interactable = false;
            }
        }
    }

    public void ChangingScreens()
    {
        if (userLoaded)
        {
            SceneManager.LoadScene("MainLevel");
        }
        else
        {
            mainScreen = !mainScreen;
            playerSelector = !playerSelector;
            PlayerSelector();
        }
    }

    void PlayerSelector()
    {
        if (playerSelector)
        {
            playerSelectGO.SetActive(true);
            GameObject mainScr = GameObject.Find("Main Screen");
            mainScr.SetActive(false);
        }
    }

    bool CheckInputField(TMP_InputField firstName, TMP_InputField lastName)
    {
        return firstName.text.Length > 0 && lastName.text.Length > 0;
    }

    public void DeleteData()
    {
        saveFileManager.DeletePlayerInfo();
        userLoaded = false;
        userInfo = new UserInfo();
    }
}
