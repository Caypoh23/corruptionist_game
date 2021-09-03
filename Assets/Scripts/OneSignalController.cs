using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneSignalController : MonoBehaviour
{
    [SerializeField] private string appId;
    void Start () {
      // Uncomment this method to enable OneSignal Debugging log output 
      // OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.VERBOSE, OneSignal.LOG_LEVEL.NONE);
      
      // Replace 'YOUR_ONESIGNAL_APP_ID' with your OneSignal App ID.
      OneSignal.StartInit(appId)
        .HandleNotificationOpened(OneSignalHandleNotificationOpened)
        .Settings(new Dictionary<string, bool>() {
          { OneSignal.kOSSettingsAutoPrompt, false },
          { OneSignal.kOSSettingsInAppLaunchURL, false } })
        .EndInit();
      
      OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;
      
      // iOS - Shows the iOS native notification permission prompt.
      //   - Instead we recomemnd using an In-App Message to prompt for notification 
      //     permission to explain how notifications are helpful to your users.
      OneSignal.PromptForPushNotificationsWithUserResponse(OneSignalPromptForPushNotificationsReponse);
    }
    
    // Gets called when the player opens a OneSignal notification.
    private static void OneSignalHandleNotificationOpened(OSNotificationOpenedResult result) {
      // Place your app specific notification opened logic here.
    }
    
    // iOS - Fires when the user anwser the notification permission prompt.
    private void OneSignalPromptForPushNotificationsReponse(bool accepted) {
      // Optional callback if you need to know when the user accepts or declines notification permissions.
    }
}
