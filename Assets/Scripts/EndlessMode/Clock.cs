using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private float secondsPerIngameDay;

    [SerializeField] private EndlessModeController endlessModeController;
    [SerializeField] private GameObject clockHourHand;

    [SerializeField] private Transform clockHourHandTransform;
    [SerializeField] private Transform clockMinuteHandTransform;
    [SerializeField] private Transform clockLateGameTransform;
    [SerializeField] private float dayStartDegrees = -90;
    [SerializeField] private float dayEndDegrees = 180;
    private float _day;
    private bool _isWorkingDayGoing;


    private void Awake()
    {
        secondsPerIngameDay = endlessModeController.GetWorkingDayTime();
    }

    private void Start()
    {
        //StartClock();
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
        Debug.Log("Stop" + secondsPerIngameDay);
        _isWorkingDayGoing = false;
        clockHourHandTransform.eulerAngles = new Vector3(0, 0, dayEndDegrees);
        clockMinuteHandTransform.eulerAngles = new Vector3(0, 0, 0);

        //clockHourHand.SetActive(false); // to avoid дергание стрелки
    }

    public void StartClock()
    {
        Debug.Log("Start" + secondsPerIngameDay);
        //clockHourHand.SetActive(true);
        _isWorkingDayGoing = true;
    }

    public void ClockTiking()
    {
        _day += Time.deltaTime / secondsPerIngameDay;
        float dayNormalized = Mathf.Round((_day % 1f) * 100f) / 100f;

        //hour hand
        float rotationDegreesPerDay = dayStartDegrees - dayEndDegrees;
        clockHourHandTransform.eulerAngles =
            new Vector3(0, 0, (dayNormalized * rotationDegreesPerDay) - dayStartDegrees);

        //minute hand
        float hoursInDay = 12f;
        clockMinuteHandTransform.eulerAngles =
            new Vector3(0, 0, (dayNormalized * rotationDegreesPerDay * hoursInDay));
    }

    /*
    public float GetSecondsPerIngameWorkingDay()
    {
        return secondsPerIngameDay;
    }

    private IEnumerator StartClockInNewLevel()
    {
        yield return new WaitForSeconds(3f);
        _isWorkingDayGoing = true;
    }
    */
    //TODP: 1 секунду перед началом уровнә (перед началом отсчета. тут и в левел контроллеер) (на затухание панели)
}