using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager GameManagerInstance;
    public GameObject[] levelPath = new GameObject[2];

    [SerializeField] private TextMeshProUGUI inGameTotalCoinTXT, inGameCurrentLevelTXT, collectedCoinTXT, totalCoinTXT, mainMenuTotalCoinTXT, mainMenuLevelTXT, inGameLevelTXT, finishedLevelTXT;
    int collectedCoin, totalCoin, levelCount, levelNumber;
    public ParticleSystem CollideParticle;
    public int currentLevel;

    private void Start()
    {
        GameManagerInstance = this;

        totalCoin = PlayerPrefs.GetInt("totalCoin");
        levelNumber = PlayerPrefs.GetInt("ReachedLevel") + 1;

        inGameTotalCoinTXT.text = mainMenuTotalCoinTXT.text = totalCoin.ToString();
        mainMenuLevelTXT.text = inGameLevelTXT.text = levelNumber.ToString();
    }

        

    public void LevelFail()
    {
        MenuManager.MenuManagerInstance.menuElement[2].SetActive(true);
        StopEffects();
        
        MenuManager.MenuManagerInstance.GameState = false;    
    }

    public void LevelFinish()
    {
        MenuManager.MenuManagerInstance.menuElement[1].SetActive(true);
        StopEffects();
        collectedCoin = (int)Mathf.Round(PlayerManager.PlayerManagerInstance._player.transform.localScale.y * 10);
        Debug.Log(collectedCoin);
        PlayerPrefs.SetInt("totalCoin", PlayerPrefs.GetInt("totalCoin") + collectedCoin);

        collectedCoinTXT.text = collectedCoin.ToString();
        mainMenuTotalCoinTXT.text = inGameTotalCoinTXT.text = totalCoinTXT.text = PlayerPrefs.GetInt("totalCoin").ToString();

        levelCount++;
        PlayerPrefs.SetInt("ReachedLevel", levelCount);

        MenuManager.MenuManagerInstance.GameState = false;
    }

    public void GetLevel()
    {
        levelNumber = PlayerPrefs.GetInt("ReachedLevel") + 1;
        Debug.Log(levelNumber);
        mainMenuLevelTXT.text = inGameLevelTXT.text = finishedLevelTXT.text = levelNumber.ToString();

        if (PlayerPrefs.GetInt("ReachedLevel") % 2 == 0)
        {
            currentLevel = 0;
            Debug.Log(currentLevel);
        }
        else
        {
            currentLevel = 1;
            Debug.Log(currentLevel);
        }
    }

    public void LevelActiveted()
    {
        levelPath[currentLevel].SetActive(true);

        foreach (Transform item in levelPath[currentLevel].transform)
        {
            item.gameObject.SetActive(true);
        }
    }

    public void StopEffects()
    {
        PlayerManager.PlayerManagerInstance.airEffect.Stop();
        PlayerManager.PlayerManagerInstance.ballTrail.Stop();
    }

    public void StartEffects()
    {
        PlayerManager.PlayerManagerInstance.ballTrail.Play();
        PlayerManager.PlayerManagerInstance.airEffect.Play();
    }

    public void CloseThePaths()
    {
        levelPath[0].SetActive(false);
        levelPath[1].SetActive(false);
    }
}
