using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropBehaviour : MonoBehaviour
{
    SeedData seedToGrow;

    [Header("Stage of Life")]
    public GameObject seed;
    public GameObject seeding;
    public GameObject harvastable;

    int growth;

    int maxGrowth;

    public enum CropState
    {
        Seed, Seeding, Harvastable
    }

    public CropState cropState;

    public void Plant(SeedData seedToGrow)
    {
        this.seedToGrow = seedToGrow;

        seeding = Instantiate(seedToGrow.seeding, transform);

        ItemData cropToYield = seedToGrow.cropToYield;

        harvastable = Instantiate(cropToYield.gameModel, transform);

        int hoursToGrow = GameTimestamp.DaysToHours(seedToGrow.daysToGrow);

        maxGrowth = GameTimestamp.HoursToMinutes(hoursToGrow);

        if (seedToGrow.regrowable)
        {
            RegrowableHarvestBehaviour regrowableHarvest = harvastable.GetComponent<RegrowableHarvestBehaviour>();

            regrowableHarvest.SetParent(this);
        }

        SwitchState(CropState.Seed);
    }

    public void Grow()
    {
        growth++;

        if(growth >= maxGrowth / 2 && cropState == CropState.Seed)
        {
            SwitchState(CropState.Seeding);
        }

        if(growth >= maxGrowth && cropState == CropState.Seeding)
        {
            SwitchState(CropState.Harvastable);
        }
    }
    
    void SwitchState(CropState stateToSwitch)
    {
        seed.SetActive(false);
        seeding.SetActive(false);
        harvastable.SetActive(false);

        switch (stateToSwitch)
        {
            case CropState.Seed:
                seed.SetActive(true);
                break;
            case CropState.Seeding:
                seeding.SetActive(true);
                break;
            case CropState.Harvastable:
                harvastable.SetActive(true);

                if(!seedToGrow.regrowable)
                {
                    harvastable.transform.parent = null;

                    Destroy(gameObject);
                }
                break;
        }

        cropState = stateToSwitch;
    }

    public void Regrow()
    {
        int hoursToRegrow = GameTimestamp.DaysToHours(seedToGrow.daysToGrow);

        growth = maxGrowth - GameTimestamp.HoursToMinutes(hoursToRegrow);

        SwitchState(CropState.Seeding);
    }
}
