using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton Implementation
    private static GameManager m_instance;

    public static GameManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                Debug.Log("No instance of GameManager in the scene!");
            }
            return m_instance;
        }
    }

    private void Awake()
    {
        if (m_instance != null)
        {
            Debug.LogWarning("Already an instance of GameManager in here- destroying this one.");
            Destroy(this);
        }

        m_instance = this;
    }
    #endregion

    public int score;

    public Text scoreText;

    private void Start()
    {
        score = 0;
        scoreText.text = score.ToString();

        if(SceneManager.GetActiveScene().name == "Level1")
        {
            StartCoroutine(DisplayMessage("Level 1"));
        }
        else
        {
            StartCoroutine(DisplayMessage("Level 2"));
        }
        
    }

    public void KillEnemy()
    {
        score += 500;
        if(score > 999999)
        {
            score = 999999;
        }
        scoreText.text = score.ToString();
    }

    public void CollectCoin()
    {
        score += 100;
        if (score > 999999)
        {
            score = 999999;
        }
        scoreText.text = score.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(SceneManager.GetActiveScene().name == "Level1")
            {
                StartCoroutine(Level1End());
            }
            else
            {
                StartCoroutine(Level2End());
            }
        }
    }

    public GameObject middleInfoPanel;
    public Text middleInfoText;

    IEnumerator DisplayMessage(string msg)
    {
        middleInfoPanel.SetActive(true);
        middleInfoText.text = msg;
        yield return new WaitForSeconds(2.0f);
        middleInfoPanel.SetActive(false);
    }

    IEnumerator Level1End()
    {
        yield return DisplayMessage("You win!");
        LoadLevel2();
    }

    IEnumerator Level2End()
    {
        yield return DisplayMessage("You win!");
        Application.Quit();
    }

    void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }
}
