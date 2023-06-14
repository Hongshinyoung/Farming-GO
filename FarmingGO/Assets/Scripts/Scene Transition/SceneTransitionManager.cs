//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class SceneTransitionManager : MonoBehaviour
//{
//    public static SceneTransitionManager Instance;

//    public enum Location { SampleScene, PlayerHome, Town, CameraScene }
//    public Location currentLocation;

//    public void SwitchScene(string PlayerHome)
//    {
//        SceneManager.LoadScene(PlayerHome);
//    }
//}

//Transform playerPoint;

//bool screenFadedOut;

//private void Awake()
//{
//    //If there is more than 1 instance, destroy GameObject
//    if (Instance != null && Instance != this)
//    {
//        Destroy(gameObject);
//    }
//    else
//    {
//        //Set the static instance to this instance
//        Instance = this;
//    }

//    //Make the gameobject persistent across scenes
//    DontDestroyOnLoad(gameObject);
//    SceneManager.sceneLoaded += OnLocationLoad;
//    playerPoint = FindObjectOfType<PlayerController>().transform;

//}

//public void SwitchLocation(Location locationToSwitch)
//{
//    UIManager.Instance.FadeOutScreen();
//    screenFadedOut = false;
//    StartCoroutine(ChangeScene(locationToSwitch));
//}

//IEnumerator ChangeScene(Location locationToSwitch)
//{
//    //Disable the player's CharacterController component
//    CharacterController playerCharacter = playerPoint.GetComponent<CharacterController>();
//    playerCharacter.enabled = false;
//    while (!screenFadedOut)
//    {
//        yield return new WaitForSeconds(0.1f);
//    }

//    //Reset the boolean
//    screenFadedOut = false;
//    UIManager.Instance.ResetFadeDefaults();
//    SceneManager.LoadScene(locationToSwitch.ToString());

//}

//public void OnFadeOutComplete()
//{
//    screenFadedOut = true;

//}

//public void OnLocationLoad(Scene scene, LoadSceneMode mode)
//{
//    //The location the player is coming from when the scene loads
//    Location oldLocation = currentLocation;

//    //Get the new location by converting the string of our current scene into a Location enum value
//    Location newLocation = (Location)Enum.Parse(typeof(Location), scene.name);

//    //If the player is not coming from any new place, stop executing the function
//    if (currentLocation == newLocation) return;

//    //Find the start point
//    Transform startPoint = LocationManager.Instance.GetPlayerStartingPosition(oldLocation);

//    //Disable the player's CharacterController component
//    CharacterController playerCharacter = playerPoint.GetComponent<CharacterController>();
//    playerCharacter.enabled = false;

//    //Change the player's position to the start point
//    playerPoint.position = startPoint.position;
//    playerPoint.rotation = startPoint.rotation;

//    //Re-enable player character controller so he can move
//    playerCharacter.enabled = true;

//    //Save the current location that we just switched to
//    currentLocation = newLocation;
//}