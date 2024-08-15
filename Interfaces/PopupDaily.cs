using System;
using UnityEngine;

public class PopupDaily : BasePopup
{
    private long mLastUnitTime;

    private int mLastSecondInDay;

    public DailyItemView[] dailyViewArr;

    /*
    public void Start()
    {       
        
        if (mLastUnitTime == 0)
        {
            long timeStamp = (long)(DateTime.UtcNow.Subtract(new DateTime(2019, 1, 1))).TotalSeconds;
            mLastUnitTime = timeStamp;

            int currentSecondInDay = (int)(DateTime.Now - DateTime.Today).TotalSeconds;
            mLastSecondInDay = currentSecondInDay;

        }
        
    }
    */
    private const int SECONDS_PER_DAY = 24 * 60 * 60;

    public void CheckNewDay()
    {
        long timeStamp = (long)(DateTime.UtcNow.Subtract(new DateTime(2019, 1, 1))).TotalSeconds;
        int currentSecondInDay = (int)(DateTime.Now - DateTime.Today).TotalSeconds;

        if (currentSecondInDay == (mLastUnitTime + SECONDS_PER_DAY))
        {
            mLastUnitTime = timeStamp;
            mLastSecondInDay = currentSecondInDay;

            //mClaimed.setValue(false);
            //mClaimGoldCoundInDay.setValue(0);
        }
    }
    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            long dailyKey = (long)(DateTime.Today.Subtract(new DateTime(2019, 1, 1))).TotalSeconds;

            if (PlayerPrefs.GetInt("Daily" + dailyKey.ToString()) == 0)
            {
                PlayerPrefs.SetInt("Daily" + dailyKey.ToString(), 1);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            long dailyKey = (long)(DateTime.Today.Subtract(new DateTime(2019, 1, 1))).TotalSeconds;

            if (PlayerPrefs.GetInt("Daily" + dailyKey.ToString()) == 0)
            {
                Debug.Log("Not get award today");
            }
            else
            {
                Debug.Log("Got award today");
            }
        }
    }
    */
    public override void Start()
    {
       
    }

    public override void Update()
    {
        
    }

    public override void InitView()
    {
        long dailyKey = (long)(DateTime.Today.Subtract(new DateTime(2019, 1, 1))).TotalSeconds;

        for (int i = 0; i < dailyViewArr.Length; i++)
        {
            dailyViewArr[i].itemIndex = i;
            dailyViewArr[i].InitItem();
            if (PlayerPrefs.GetInt("Daily" + dailyKey.ToString() + i.ToString()) == 0)
            {
                dailyViewArr[i].EnableItem();
            }
            else
            {
                dailyViewArr[i].DisableItem();
            }
        }
    }
}
