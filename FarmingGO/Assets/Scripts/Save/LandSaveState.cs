using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LandSaveState
{
    public Land.LandStatus landStatus;
    public GameTimestamp lastWatered;

    public LandSaveState(Land.LandStatus landStatus, GameTimestamp lastWatered)
    {
        this.landStatus = landStatus;
        this.lastWatered = lastWatered;
    }
}
