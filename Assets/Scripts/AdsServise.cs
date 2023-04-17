using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using System.Collections.Generic;

public class AdsServise : MonoBehaviour, IAppodealInitializationListener, IRewardedVideoAdListener, IBannerAdListener
{
    [SerializeField] private Button _showAdsInShopButton;
    [SerializeField] private string _appKey;

    private CreditPanel _creditPanel;
    private int _prize = 0;

    private DateTime? _lastBlockAdsDay
    {
        get
        {
            string data = PlayerPrefs.GetString("lastBlockAdsDay", null);

            if (string.IsNullOrEmpty(data) == false)
                return DateTime.Parse(data);

            return null;
        }
        set
        {
            if (value != null)
                PlayerPrefs.SetString("lastBlockAdsDay", value.ToString());
            else
                PlayerPrefs.DeleteKey("lastBlockAdsDay");
        }
    }

    private DateTime? _nextCreditsAccureDay
    {
        get
        {
            string data = PlayerPrefs.GetString("nextCreditsAccureDay", null);

            if (string.IsNullOrEmpty(data) == false)
                return DateTime.Parse(data);

            return DateTime.UtcNow;
        }
        set
        {
            if (value != null)
                PlayerPrefs.SetString("nextCreditsAccureDay", value.ToString());
            else
                PlayerPrefs.DeleteKey("nextCreditsAccureDay");
        }
    }

    public bool IsAdsBlocked { get; private set; }

    [Inject]
    public void Construct(CreditPanel creditPanel)
    {
        _creditPanel = creditPanel;
    }

    private void Start()
    {
        int adTypes = AppodealAdType.Interstitial | AppodealAdType.Banner | AppodealAdType.RewardedVideo;
        Appodeal.Initialize(_appKey, adTypes, this);
        Appodeal.SetBannerCallbacks(this);
        Appodeal.SetTabletBanners(false);
        Appodeal.SetSmartBanners(true);
    }

    private void OnEnable()
    {
        _showAdsInShopButton.onClick.AddListener(() => ShowAdAndAccrue(100));

        StartCoroutine(BlockAdsStateUpdater());
    }

    private void OnDisable()
    {
        _showAdsInShopButton.onClick.RemoveAllListeners();

        StopAllCoroutines();
    }

    public bool ShowInterstationAds()
    {
        if (Appodeal.IsLoaded(AppodealAdType.Interstitial) && IsAdsBlocked == false)
        {
            Appodeal.Show(AppodealShowStyle.Interstitial);
            return true;
        }

        return false;
    }

    private void ShowAdAndAccrue(int prize)
    {
        Appodeal.SetRewardedVideoCallbacks(this);
        _prize = prize;
        print("Prize is: " + _prize);
        if (!Appodeal.IsLoaded(AppodealAdType.RewardedVideo))
        {
            print("Fall");
            return;
        }
        else
        {
            Appodeal.Show(AppodealShowStyle.RewardedVideo);
        }
    }

    private void BlockAdsOnPeriods(int days)
    {
        IsAdsBlocked = true;

        _lastBlockAdsDay = _lastBlockAdsDay.HasValue ? _lastBlockAdsDay.Value.AddDays(days) : DateTime.UtcNow.AddDays(days);
        _nextCreditsAccureDay = DateTime.UtcNow;

        StartCoroutine(BlockAdsStateUpdater());
    }

    private IEnumerator BlockAdsStateUpdater()
    {
        if (_lastBlockAdsDay == null)
            yield break;

        while (_lastBlockAdsDay.HasValue)
        {
            UpdateAdsBlockState();
            UpdateAccurePrizeState();
            yield return new WaitForSeconds(1);
        }
    }

    private void UpdateAdsBlockState()
    {
        IsAdsBlocked = false;

        if (_lastBlockAdsDay.HasValue)
        {
            if (DateTime.UtcNow > _lastBlockAdsDay.Value)
                _lastBlockAdsDay = null;
            else
                IsAdsBlocked = true;
        }
    }

    private void UpdateAccurePrizeState()
    {
        if (DateTime.UtcNow.Day < _nextCreditsAccureDay.Value.Day)
            return;

        _creditPanel.AddCredits(1000);
        _nextCreditsAccureDay = DateTime.UtcNow.AddDays(1);
    }

    public void OnInitializationFinished(List<string> errors)
    {
        print("Appodeal init");
        if (errors != null)
        {
            foreach (var error in errors)
            {
                print("AppoDeal error: " + error);
            }
        }
    }

    #region RewardVideo

    public void OnRewardedVideoLoaded(bool isPrecache)
    {

    }

    public void OnRewardedVideoFailedToLoad()
    {

    }

    public void OnRewardedVideoShowFailed()
    {

    }

    public void OnRewardedVideoShown()
    {

    }

    public void OnRewardedVideoFinished(double amount, string currency)
    {

    }

    public void OnRewardedVideoClosed(bool finished)
    {
        print("Ad is finished: " + finished);
        if (finished)
        {
            _creditPanel.AddCredits(_prize);
            print("Add coins: " + _prize);
        }
    }

    public void OnRewardedVideoExpired()
    {

    }

    public void OnRewardedVideoClicked()
    {

    }

    #endregion

    #region Banner

    public void OnBannerLoaded(int height, bool isPrecache)
    {
        Appodeal.Show(AppodealShowStyle.BannerBottom);
    }

    public void OnBannerFailedToLoad()
    {

    }

    public void OnBannerShown()
    {

    }

    public void OnBannerShowFailed()
    {
        print("banner was failed");
    }

    public void OnBannerClicked()
    {

    }

    public void OnBannerExpired()
    {

    }

    #endregion
}

