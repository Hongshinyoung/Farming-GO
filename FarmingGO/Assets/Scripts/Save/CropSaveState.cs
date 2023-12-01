using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CropSaveState
{
    //The index of the land the crop is planted on 
    public int landID;

    public string seedToGrow;
    public CropBehaviour.CropState cropState;
    public int growth;
    public int health;

    public CropSaveState(int landID, string seedToGrow, CropBehaviour.CropState cropState, int growth, int health)
    {
        this.landID = landID;
        this.seedToGrow = seedToGrow;
        this.cropState = cropState;
        this.growth = growth;
        this.health = health;
    }

    public void Grow()
    {
        growth++;

        SeedData seedInfo = (SeedData) InventoryManager.Instance.itemIndex.GetItemFromString(seedToGrow);
        float maxGrowth = GameTimestamp.HoursToMinutes(GameTimestamp.DaysToHours(seedInfo.daysToGrow));
        int maxHealth = GameTimestamp.HoursToMinutes(48);

        if (health < maxHealth)
        {
            health++;
        }

        if (growth >= maxGrowth / 2 && cropState == CropBehaviour.CropState.Seed)
        {
            cropState = CropBehaviour.CropState.Seedling;
        }

        if (growth >= maxGrowth && cropState == CropBehaviour.CropState.Seedling)
        {
            cropState = CropBehaviour.CropState.Harvestable;
        }
    }

    public void Wither()
    {
        health--;

        if (health <= 0 && cropState != CropBehaviour.CropState.Seed)
        {
            cropState = CropBehaviour.CropState.Wilted;
        }
    }
}