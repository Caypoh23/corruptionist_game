using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class ContinueGameAds : MonoBehaviour, IUnityAdsListener
{
#if UNITY_IOS
    private const string GameId = "4198042";
#else
    private const string GameId = "4198043";
#endif

    private const string PlacementId = "ContinueGame";

    private Action _onRewardedAdSuccess;
    
    private void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(GameId);
    }

    public void PlayContinueGameAd(Action onSuccess)
    {
        _onRewardedAdSuccess = onSuccess;
        if (Advertisement.IsReady(PlacementId))
        {
            Advertisement.Show(PlacementId);
        }
        else
        {
            // if the ad is not yet loaded out
            // is not ready
            Debug.Log("ADS ARE NOT READY");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        if (placementId == PlacementId)
        {
            Debug.Log("ADS ARE READY");
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("ERROR: " + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // need to mute all the sound
        Debug.Log("VIDEO STARTED");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == PlacementId && showResult == ShowResult.Finished)
        {
            Debug.Log("VIDEO FINISHED");
            _onRewardedAdSuccess.Invoke();
        }
        else if (showResult == ShowResult.Skipped)
        {
            // do not reward the user for skipping the ad
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.Log("Failed");
        }
    }
}