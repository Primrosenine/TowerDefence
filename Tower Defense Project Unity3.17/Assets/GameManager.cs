using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject endUI;
    public Text endMessage;

    public static GameManager instance;
    private EnemySpawner spawner;

    private void Awake()
    {
        instance = this;
        spawner = GetComponent<EnemySpawner>();
    }

    public void Win()
    {
        endUI.SetActive(true);
        endMessage.text = "Win";
    }

    public void Fail()
    {
        spawner.StopSpowner();
        endUI.SetActive(true);
        endMessage.text = "Game Over";
    }

    public void OnClickRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene(0);
    }
}
