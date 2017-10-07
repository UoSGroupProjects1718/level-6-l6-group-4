using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class TimeManager : SingletonClass<TimeManager> {


    [SerializeField]
    private float DaysPerHour;

    private float timeDelta;

    static GameTime IngameTime;

    [SerializeField]
    private Text dateText;

    public override void Awake()
    {
        base.Awake();
        IngameTime = new GameTime();
        //the amount of ingame minutes per second to inccrease the minute counter by
        //the amount of in game days to pass per real time hour multiplied by the amount of hours in the day, divided by the amount of minutes in an hour
        timeDelta = (( DaysPerHour * 24) / 60);
        Debug.Log(timeDelta);
    }
    public void Start()
    {
        IngameTime.Date = new Vector3(0, 1, 1492);
       StartCoroutine("PassTime");
    }
    
    public static GameTime Time
    {
        get
        {
            return IngameTime;
        }
    }

    private IEnumerator PassTime()
    {
        while(true)
        {
        //pass one second
        yield return new WaitForSeconds(1f);
        IngameTime.minutes += timeDelta;

            //if the minutes counter has passed 60
            if (IngameTime.minutes >= 60)
            {
                //find the delta
                float timeOverMinute = IngameTime.minutes - 60;
                IngameTime.hours += 1;
                //revert the minutes counter to whatever amount exceeded 60
                IngameTime.minutes = timeOverMinute;
                //if the amount of hours gets to one day
                if (IngameTime.hours >= 24)
                {
                    IngameTime.Date.x += 1;
                    IngameTime.hours = 0;
                    //using a lunar month of 28 days for simplicity's sake
                    if (IngameTime.Date.x >= 28)
                    {

                        IngameTime.Date.y += 1;
                        IngameTime.Date.x = 0;
                        //if we get to 1 year
                        if (IngameTime.Date.y >= 12)
                        {
                            IngameTime.Date.z += 1;
                            IngameTime.Date.y = 1;
                        }
                    }
                }
            }
        }
    }

    private void Update()
    {
        string timeOfDay = (Time.hours > 12) ? " pm" : " am";
        dateText.text = Time.hours + " : " + Time.minutes + timeOfDay + "\n " + Time.Date.x + ", " + Time.Date.y + ", " + Time.Date.z;
    }

}

public struct GameTime
{
    public Vector3 Date;
    public float minutes;
    public int hours;
}
