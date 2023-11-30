using System.Collections;
using System.Collections.Generic;
using System.Media;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class EndingManager : MonoBehaviour
{
    public AudioSource audioSource;
    public VideoPlayer videoPlayer;
    public QuestComplete questComplete;
    public RawImage rawImage;

    public void MuteSoundPlayVideo()
    {
        audioSource.Stop();
        rawImage.gameObject.SetActive(true);
        videoPlayer.Play();
    }

}
