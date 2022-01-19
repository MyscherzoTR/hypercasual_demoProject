using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager MenuManagerInstance;
    public GameObject[] menuElement = new GameObject[3];
    public bool GameState;
    void Start()
    {
        GameState = false;
        MenuManagerInstance = this;
    }

   public void StartTheGame()
    {
        GameState = true;
        menuElement[0].SetActive(false);
        menuElement[3].SetActive(true);
        GameManager.GameManagerInstance.StartEffects();
    }

    public void RetryGame()
    {
        MainMenu();
    }

    public void MainMenu()
    {
        PlayerManager.PlayerManagerInstance.path.position = new Vector3(0, 0, 73.9f);
        PlayerManager.PlayerManagerInstance.ResetPlayer();

        menuElement[0].SetActive(true);
        menuElement[1].SetActive(false);
        menuElement[2].SetActive(false);
        menuElement[3].SetActive(false);
        
        GameManager.GameManagerInstance.GetLevel();
        GameManager.GameManagerInstance.CloseThePaths();

        GameManager.GameManagerInstance.LevelActiveted();
    }
}
