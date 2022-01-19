using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Collector : MonoBehaviour
{
    public static Collector CollectorInstance;
    public GameObject[] levelPath = new GameObject[2];

    [SerializeField] private TextMeshProUGUI inGameTotalCoinTXT, inGameCurrentLevelTXT, collectedCoinTXT, totalCoinTXT, mainMenuTotalCoinTXT, mainMenuLevelTXT, inGameLevelTXT, finishedLevelTXT;
    GameObject player;
    float playerHeight, height, growthRate = 0.25f, shrinkRate; 
    int collectedCoin, totalCoin, levelCount, levelNumber;
    private Vector3 ScaleChange;
    public ParticleSystem CollideParticle;
    public int currentLevel;

    private void Start()
    {
        CollectorInstance = this;

        player = GameObject.FindGameObjectWithTag("Player");
        Obstacle.obsShrinkRate += ObstacleShrinkRate;
        totalCoin = PlayerPrefs.GetInt("totalCoin");
        levelNumber = PlayerPrefs.GetInt("ReachedLevel") + 1;

        inGameTotalCoinTXT.text = mainMenuTotalCoinTXT.text = totalCoin.ToString();
        mainMenuLevelTXT.text = inGameLevelTXT.text = levelNumber.ToString();
    }

    void ObstacleShrinkRate(float shrinkRate)
    {
        this.shrinkRate = shrinkRate;
    }
    private void OnTriggerEnter(Collider other)
    {
        playerHeight = player.transform.localScale.y;

        var NewParticle = Instantiate(CollideParticle, transform.position, Quaternion.identity);
        //var particleColor = NewParticle.GetComponent<Renderer>().material;

        if (other.CompareTag("collectable"))
        {
            NewParticle.GetComponent<Renderer>().material = other.GetComponent<Renderer>().material;

            player.transform.position = new Vector3(transform.position.x, transform.position.y + (playerHeight * growthRate) / 2, transform.position.z);
            height = (playerHeight * growthRate) + playerHeight;
            ScaleChange = new Vector3(1.0f, height, 1.0f);
            player.transform.localScale = ScaleChange;


            Destroy(other.gameObject);
        }

        if (other.CompareTag("obstacle"))
        {
            if (shrinkRate == 0.25f)
            {
                NewParticle.GetComponent<Renderer>().material.color = new Color(255, 10, 0);

            }
            else
            {
                NewParticle.GetComponent<Renderer>().material.color = new Color(220, 0, 0);
            }

            player.transform.position = new Vector3(transform.position.x, transform.position.y - (playerHeight * shrinkRate) / 2, transform.position.z);
            height = playerHeight - (playerHeight * shrinkRate);
            ScaleChange = new Vector3(1.0f, height, 1.0f);
            player.transform.localScale = ScaleChange;


            Destroy(other.gameObject);
        }

        if (other.CompareTag("finishLine"))
        {
            LevelFinish();
        }

        if (height < 0.10f)
        {
            LevelFail(); 
        }
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
        collectedCoin = (int)Mathf.Round(player.transform.localScale.y * 10);
        PlayerPrefs.SetInt("totalCoin", PlayerPrefs.GetInt("totalCoin") + collectedCoin);

        collectedCoinTXT.text = collectedCoin.ToString();
        mainMenuTotalCoinTXT.text =inGameTotalCoinTXT.text = totalCoinTXT.text = (collectedCoin + totalCoin).ToString();

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
            //levelPath[0].SetActive(false);
            //levelPath[1].SetActive(true);
            Debug.Log(currentLevel);
        }
        else
        {
            currentLevel = 1;
            //levelPath[0].SetActive(true);
            //levelPath[1].SetActive(false);
            Debug.Log(currentLevel);
        }
        MenuManager.MenuManagerInstance.menuElement[1].SetActive(false);
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
        //PlayerManager.PlayerManagerInstance.path.GetChild(0).gameObject.SetActive(false);
        //PlayerManager.PlayerManagerInstance.path.GetChild(1).gameObject.SetActive(false);
    }
}
