using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour, ITimeTracker
{
    public static GameStateManager Instance { get; private set; }
    private void Awake()
    {
        //If there is more than one instance, destroy the extra
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            //Set the static instance to this instance
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        TimeManager.Instance.RegisterTracker(this);
    }

    public void ClockUpdate(GameTimestamp timestamp)
    {
        //if (SceneTransitionManager.Instance.currentLocation != SceneTransitionManager.Location.SampleScene)
        //{
        //    List<LandSaveState> landData = LandManager.farmData.Item1;
        //    List<CropSaveState> cropData = LandManager.farmData.Item2;

        //    if (cropData.Count == 0) return;

        //    for (int i = 0; i < cropData.Count; i++)
        //    {
        //        CropSaveState crop = cropData[i];
        //        LandSaveState land = landData[crop.landID];

        //        if (crop.cropState == CropBehaviour.CropState.Wilted) continue;
        //        land.ClockUpdate(timestamp);

        //        if (land.landStatus == Land.LandStatus.Watered)
        //        {
        //            crop.Grow();
        //        }
        //        else if (crop.cropState != CropBehaviour.CropState.Seed)
        //        {
        //            crop.Wither();
        //        }

        //        cropData[i] = crop;
        //        landData[crop.landID] = land;
        //    }

        //    LandManager.farmData.Item2.ForEach((CropSaveState crop) =>
        //    {
        //        Debug.Log(crop.seedToGrow + "\n Health: " + crop.health + "\n Growth: " + crop.growth + "\n State: " + crop.cropState.ToString());
        //    });
        //}
    }
}
