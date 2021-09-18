using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
}
