using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockUI : MonoBehaviour
{
    [SerializeField] private float secondsPerIngameDay;
    [SerializeField] private Transform clockHourHandTransform;
    [SerializeField] private Transform clockMuniteHandTransform;
    [SerializeField] private float dayStartDegrees = -90;
    [SerializeField] private float dayEndDegrees = 180;
    private float _day;
    private bool _isWorkingDayGoing;

    private void Awake()
    {
        
    }
    private void Start()
    {
        StartClock();
    }
    private void Update()
    {

        if (_isWorkingDayGoing)
        { 
            ClockTiking();
        }

    }
    public void StopClock()
    {
        _isWorkingDayGoing = false;
 
        clockHourHandTransform.eulerAngles = new Vector3(0, 0, dayEndDegrees);
        clockMuniteHandTransform.eulerAngles = new Vector3(0, 0, 0);
    }
    public void StartClock()
    {
        _isWorkingDayGoing = true;

    }
    public void ClockTiking()
    {
        _day += Time.deltaTime / secondsPerIngameDay;
        float dayNormalized = Mathf.Round((_day % 1f) * 100f) / 100f;
  
            //hour hand
            float rotationDegreesPerDay = dayStartDegrees - dayEndDegrees;
            clockHourHandTransform.eulerAngles = new Vector3(0, 0, (dayNormalized * rotationDegreesPerDay) - dayStartDegrees);

            //minute hand
            float hoursInDay = 12f;
            clockMuniteHandTransform.eulerAngles = new Vector3(0, 0, (dayNormalized * rotationDegreesPerDay * hoursInDay));
        
    }
    public float GetSecondsPerIngameWorkingDay()
    {
        return secondsPerIngameDay;
    }
}
