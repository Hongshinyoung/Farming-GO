using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string SceneName;
    public void SwitchScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void OnAppQuit()
    {
        Application.Quit();
    }

    public void onclick()
    {
        Debug.Log("Å¬¸¯");
    }
}
