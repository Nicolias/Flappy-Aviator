using UnityEngine;
using OneSignalSDK;
using System.Collections.Generic;
using System;

public class OneSignalServise : MonoBehaviour
{
    [SerializeField] private string _appId;

    private void Start()
    {
        OneSignal.Default.Initialize(_appId);

        PromptForPush();
    }

    public async void PromptForPush()
    {
        var result = await OneSignal.Default.PromptForPushNotificationsWithUserResponse();

        if (result == NotificationPermission.Denied)
            Application.Quit();

        //SendPushToSelf();
    }

    //public async void SendPushToSelf()
    //{
    //    // Use the id of the push subscription in order to send a push without needing an API key
    //    var pushId = OneSignal.Default.PushSubscriptionState.userId;

    //    // Check out our API docs at https://documentation.onesignal.com/reference/create-notification
    //    // for a full list of possibilities for notification options.
    //    var pushOptions = new Dictionary<string, object>
    //    {
    //        ["contents"] = new Dictionary<string, string>
    //        {
    //            ["en"] = "Test Message"
    //        },

    //        // Send notification to this user
    //        ["include_player_ids"] = new List<string> { pushId },

    //        // Example of scheduling a notification in the future
    //        ["send_after"] = DateTime.Now.ToUniversalTime().AddSeconds(30).ToString("U")
    //    };

    //    await OneSignal.Default.PostNotification(pushOptions);
    //}
}
