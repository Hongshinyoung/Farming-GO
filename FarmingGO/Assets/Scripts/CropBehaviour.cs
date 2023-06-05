using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropBehaviour : MonoBehaviour
{
    int landID;
    SeedData seedToGrow;

    [Header("Stage of Life")]
    public GameObject seed;
<<<<<<< Updated upstream
    public GameObject seeding;
    public GameObject harvastable;
=======
    public GameObject wilted;
    public GameObject seedling;
    public GameObject harvestable;
>>>>>>> Stashed changes

    int growth;

    int maxGrowth;

    public enum CropState
    {
        Seed, Seeding, Harvastable
    }

    public CropState cropState;

    public void Plant(int landID, SeedData seedToGrow)
    {
        LandManager.Instance.RegisterCrop(landID, seedToGrow, cropState, growth, health);
        LoadCrop(landID, seedToGrow, CropState.Seed, 0, 0);
    }

    public void LoadCrop(int landID, SeedData seedToGrow, CropState cropState, int growth, int health)
    {
        this.landID = landID;
        //Save the seed information
        this.seedToGrow = seedToGrow;

        //Instantiate the seedling and harvestable GameObjects
        seedling = Instantiate(seedToGrow.seedling, transform);

        //Access the crop item data
        ItemData cropToYield = seedToGrow.cropToYield;

        //Instantiate the harvestable crop
        harvestable = Instantiate(cropToYield.gameModel, transform);

        //Convert Days To Grow into hours
        int hoursToGrow = GameTimestamp.DaysToHours(seedToGrow.daysToGrow);
        //Convert it to minutes
        maxGrowth = GameTimestamp.HoursToMinutes(hoursToGrow);

<<<<<<< Updated upstream
        SwitchState(CropState.Seed);
=======
        //Set the growth and health accordingly
        this.growth = growth;
        this.health = health;

        //Check if it is regrowable
        if (seedToGrow.regrowable)
        {
            //Get the RegrowableHarvestBehaviour from the GameObject
            RegrowableHarvestBehaviour regrowableHarvest = harvestable.GetComponent<RegrowableHarvestBehaviour>();

            //Initialise the harvestable 
            regrowableHarvest.SetParent(this);
        }

        //Set the initial state to Seed
        SwitchState(cropState);

>>>>>>> Stashed changes
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

        //Inform LandManager on the changes
        LandManager.Instance.OnCropStateChange(landID, cropState, growth, health);
    }
<<<<<<< Updated upstream
=======

    public void Wither()
    {
        health--;

        if(health <= 0 && cropState != CropState.Seed)
        {
            SwitchState(CropState.Wilted);
        }

        //Inform LandManager on the changes
        LandManager.Instance.OnCropStateChange(landID, cropState, growth, health);
    }
>>>>>>> Stashed changes
    
    void SwitchState(CropState stateToSwitch)
    {
        seed.SetActive(false);
<<<<<<< Updated upstream
        seeding.SetActive(false);
        harvastable.SetActive(false);
=======
        seedling.SetActive(false);
        harvestable.SetActive(false);
        wilted.SetActive(false);
>>>>>>> Stashed changes

        switch (stateToSwitch)
        {
            case CropState.Seed:
                seed.SetActive(true);
                break;
            case CropState.Seeding:
<<<<<<< Updated upstream
                seeding.SetActive(true);
=======
                seedling.SetActive(true);

                health = maxHealth;

>>>>>>> Stashed changes
                break;
            case CropState.Harvastable:
                harvestable.SetActive(true);

<<<<<<< Updated upstream
                harvastable.transform.parent = null;
=======
                if (!seedToGrow.regrowable)
                {
                    harvestable.transform.parent = null;
                    Destroy(gameObject);
                }
>>>>>>> Stashed changes

                Destroy(gameObject);
                break;
        }

        cropState = stateToSwitch;
    }
<<<<<<< Updated upstream
=======

    public void Regrow()
    {
        int hoursToRegrow = GameTimestamp.DaysToHours(seedToGrow.daysToGrow);
        growth = maxGrowth - GameTimestamp.HoursToMinutes(hoursToRegrow);

        SwitchState(CropState.Seeding);
    }

    public void OnDestroy()
    {
        LandManager.Instance.DeregisterCrop(landID);
    }
>>>>>>> Stashed changes
}
