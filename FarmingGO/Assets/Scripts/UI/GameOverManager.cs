using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject overPanel;
    public void GameOver()
    {
        overPanel.SetActive(true);
    }

    public void Restart()
    {
        PlayerStats.ResetMoney();
        SceneManager.LoadScene(0);
    }
}
