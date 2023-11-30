using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShippingState : MonoBehaviour, ITimeTracker
{
    public ShippingState Instance { get; private set; }
    public void ClockUpdate(GameTimestamp timestamp)
    {
        UpdateShippingState(timestamp);
    }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void UpdateShippingState(GameTimestamp timestamp)
    {
        if (timestamp.hour == ShippingBin.hourToShip && timestamp.minute == 0)
        {
            ShippingBin.ShipItems();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        TimeManager.Instance.RegisterTracker(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
