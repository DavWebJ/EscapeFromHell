using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using BlackPearl;
[RequireComponent(typeof(M_Options))]
[RequireComponent(typeof(M_Input))]
[RequireComponent(typeof(M_Resources))]

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [HideInInspector] public M_Options options = null;
    [HideInInspector] public M_Input input = null;
    [HideInInspector] public M_Resources resources = null;
     [HideInInspector] public AudioM audioManager = null;
    public bool IsGameStarted = false;
    private string gameScene = "Game";
    private string menuScene = "Menu";
    public bool enabledmouse = false;
    public bool enabledGamepad = false;
    public bool CheckHUD
    {
        get{ 
            if(Inventory.instance != null  && gameObject.activeSelf)
            {
                return Inventory.instance.isInventoryOpen;
            }else{
                return false;
            }
        }
    }
    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }else
        {
            instance = this;
            DontDestroyOnLoad(this);
            options = GetComponent<M_Options>();
            input = GetComponent<M_Input>();
            resources = GetComponent<M_Resources>();

            audioManager =GetComponent<AudioM>();
            resources.InitItems();
        }
    }
    private void Start() {
        // SetMouseCursor(enabledmouse);
        // Cursor.lockState = CursorLockMode.Locked;
        // SetGamePad(options.enabledPad);

    }


    public void Menu()
    {
        SceneManager.LoadSceneAsync(menuScene);
    }
    public void Game()
    {
        SceneManager.LoadSceneAsync(gameScene);
    }

    public void Options()
    {

    }
    public void QuitGame()
    {

    }

    public void GameOver()
    {


    }

    public void SetMouseCursor(bool visible)
    {
  
        Cursor.visible = visible;
        Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void SetGamePad(bool activate)
    {
        enabledGamepad = activate;

    }

    private void Update() {
        SetMouseCursor(CheckHUD);
    }
  
}
