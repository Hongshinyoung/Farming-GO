//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Vuforia;

//public class ObjectRecognition : MonoBehaviour, ITrackableEventHandler
//{
//    private TrackableBehaviour trackableBehaviour;

//    void Start()
//    {
//        trackableBehaviour = GetComponent<TrackableBehaviour>();

//        if (trackableBehaviour)
//        {
//            trackableBehaviour.RegisterTrackableEventHandler(this);
//        }
//    }

//    public void OnTrackableStateChanged(
//        TrackableBehaviour.Status previousStatus,
//        TrackableBehaviour.Status newStatus)
//    {
//        if (newStatus == TrackableBehaviour.Status.DETECTED ||
//            newStatus == TrackableBehaviour.Status.TRACKED ||
//            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
//        {
//            // 사물이 인식되었을 때의 처리 작업을 여기에 추가합니다.
//            Debug.Log("Object detected or tracked!");
//        }
//        else
//        {
//            // 사물이 인식되지 않았을 때의 처리 작업을 여기에 추가합니다.
//            Debug.Log("Object lost!");
//        }
//    }
//}