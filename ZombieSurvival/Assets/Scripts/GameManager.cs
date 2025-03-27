using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float score;

    public static GameManager instance;

    public Image playerHpBar;
    public GameObject GameOverGO;
    public TMP_Text ammoText;
    public TMP_Text waveText;
    public TMP_Text enemyLeftText;
    public TMP_Text scoreText;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject); // 씬 전환 시 유지
        }
        else if (instance != this)
        {
            Destroy(gameObject); // 중복 인스턴스 제거
        }
    }

    private void Start()
    {
        GameOverGO.GetComponentInChildren<Button>().onClick.AddListener(() => { SceneManager.LoadScene("GameScene"); });
        UpdateScore();
    }

    public void UpdatePlayerHPBar(float value)
    {
        playerHpBar.fillAmount = value;
    }
    public void UpdateAmmoUI(string text)
    {
        ammoText.text = text;
    }

    public void UpdateWaveUI(string text)
    {
        waveText.text = text;
    }

    public void UpdateEnemyLeftText(string text)
    {
        enemyLeftText.text = text;  
    }

    public void UpdateScore()
    {
        scoreText.text = $"Score : {score}";
    }
}