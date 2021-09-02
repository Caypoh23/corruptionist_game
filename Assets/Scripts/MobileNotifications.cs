using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class MobileNotifications : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AndroidNotificationCenter.CancelAllDisplayedNotifications();


        // create a channel to sent notifications through
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Reminder notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        DateTime now = DateTime.Now;


        // daily reminder to come back
        var notification = new AndroidNotification();
        notification.Title = "Время собирать деньги!";
        notification.Text = "Слышишь шелест банкнот? Возвращайся и собери их всех!";
        notification.LargeIcon = "icon_large";
        notification.SmallIcon = "icon_small";

        //notification.FireTime = new DateTime(now.Year, now.Month, now.Day, 10, 0, 0).AddDays(1);
        //notification.RepeatInterval = new TimeSpan(1, 0, 0, 0);
        notification.FireTime = now;
        notification.RepeatInterval = new TimeSpan(0, 0, 5, 0);

        AndroidNotificationCenter.SendNotification(notification, "channel_id");

        //if(AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
        //{
        //    AndroidNotificationCenter.CancelAllNotifications();
        //    AndroidNotificationCenter.SendNotification(notification, "channel_id");
        //}

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
