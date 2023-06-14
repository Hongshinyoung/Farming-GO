using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LocationEntryPoint : MonoBehaviour
{
    public string PlayerHome;
    private void OnTriggerEnter(Collider other)
    {
        //Check if the collider belongs to the player
        if (other.tag == "Player")
        {
            //SwitchScene(name);
            Debug.Log("´ê");
            SwitchScene(PlayerHome);
        }
    }


    public void SwitchScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    //[SerializeField]
    //SceneTransitionManager.Location locationToSwitch;

    //private void OnTriggerEnter(Collider other)
    //{
    //    //Check if the collider belongs to the player
    //    if (other.tag == "Player")
    //    {
    //        //Switch scenes to the location of the entry point
    //        SceneTransitionManager.Instance.SwitchLocation(locationToSwitch);
    //    }
    //}
}
