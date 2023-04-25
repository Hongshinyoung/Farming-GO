using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropBehaviour : MonoBehaviour
{
    //씨앗이 어떤 작물로 자랄지에 대한 정보
    SeedData seedToGrow;

    
    [Header("Stages of Life")]
    public GameObject seed;
    public GameObject seeding;
    public GameObject harvestable;

    //작물 성장 단계
    public enum CropState
    {
        Seed, Seeding, Harvestable
    }

    public CropState cropState;

    public void Plant(SeedData seedToGrow)
    {
        this.seedToGrow = seedToGrow;

        seeding = Instantiate(seedToGrow.seeding, transform);

        Item cropToYield = seedToGrow.cropToYield;

        harvestable = Instantiate(cropToYield.gamemodel, transform);

        SwitchState(CropState.Seed);
    }

    void SwitchState(CropState stateToSwitch)
    {
        seed.SetActive(false);
        seeding.SetActive(false);
        harvestable.SetActive(false);

        switch (stateToSwitch)
        {
            case CropState.Seed:
                seed.SetActive(true);
                break;
            case CropState.Seeding:
                seeding.SetActive(true);
                break;
            case CropState.Harvestable:
                harvestable.SetActive(true);
                break;
        }
        cropState = stateToSwitch;
    }
    public void Grow()
    {

    }

}
