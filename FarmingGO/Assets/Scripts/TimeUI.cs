using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour, ITimeTracker
{

    public static TimeManager Instance { get; private set; }

    [Header("Status Bar")]

    public Text timeText;
    public Text dateText;


    // Start is called before the first frame update
    private void Start()
    {
        TimeManager.Instance.RegisterTracker(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClockUpdate(GameTimestamp timestamp)
    {
        int hours = timestamp.hour;
        int minutes = timestamp.minute;

        string prefix = "AM";

        if (hours > 12)
        {
            prefix = "PM";
            hours = hours - 12;
            Debug.Log(hours);
        }

        timeText.text = prefix + hours + ":" + minutes.ToString("00");

        int day = timestamp.day;
        string season = timestamp.season.ToString();
        string dayofTheWeek = timestamp.GetDayOfTheWeek().ToString();

        dateText.text = season + " " + day + "(" + dayofTheWeek + ")";
    }
}
