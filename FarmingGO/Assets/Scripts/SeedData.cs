using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Seed")]
public class SeedData : ItemData
{
    public int daysToGrow;

    public ItemData cropToYield;

<<<<<<< Updated upstream
    public GameObject seeding;
=======
    public GameObject seedling;

    [Header("Regrowable")]

    public bool regrowable;

    public int dayToRegrow;
>>>>>>> Stashed changes
}
