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
        Collector.CollectorInstance.StartEffects();
    }

    public void RetryGame()
    {
        menuElement[1].SetActive(false);
        menuElement[2].SetActive(false);
        menuElement[3].SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Collector.CollectorInstance.StartEffects();
    }

    public void MainMenu()
    {
        menuElement[0].SetActive(true);
        menuElement[1].SetActive(false);
        menuElement[2].SetActive(false);
        menuElement[3].SetActive(false);
        
        Debug.Log("Reac Level1: " + PlayerPrefs.GetInt("ReachedLevel"));
        Collector.CollectorInstance.GetLevel();
        Debug.Log("Reac Level2: " + PlayerPrefs.GetInt("ReachedLevel"));
        Collector.CollectorInstance.CloseThePaths();
        //PlayerManager.PlayerManagerInstance.path.GetChild(Collector.CollectorInstance.currentLevel).gameObject.SetActive(true);
        Collector.CollectorInstance.levelPath[Collector.CollectorInstance.currentLevel].SetActive(true);
    }
}
