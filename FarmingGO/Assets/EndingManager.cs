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
    public RawImage rawImage;

    private void Awake()
    {   
        audioSource = GetComponent<AudioSource>();
        videoPlayer = GetComponent<VideoPlayer>();
        rawImage = GetComponent<RawImage>();
    }
    public void MuteSoundPlayVideo()
    {
        audioSource.Stop();
        Debug.Log("오디오끔");
        rawImage.gameObject.SetActive(true);
        Debug.Log("비디오 출력준비");
        videoPlayer.Play();
        Debug.Log("비디오 재생");
    }

}
