using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class ShowBanner : MonoBehaviour {
    private BannerView bannerView;

    // Use this for initialization
    void Start () {
        RequestBanner();
    }



    private void RequestBanner() {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-2818714360507440/5416366816";
#elif UNITY_IPHONE
		string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
#else
		string adUnitId = "unexpected_platform";
#endif

        AdSize adSize = new AdSize(320, 40);
        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder()
            .Build();
        // Load the banner with the request.
        bannerView.LoadAd(request);


    }

    public void Destroy() {
#if UNITY_ANDROID
        if(bannerView != null)
        bannerView.Destroy();
#endif
    }
    void OnDestroy() {
#if UNITY_ANDROID
        if (bannerView != null)
            bannerView.Destroy();
#endif
    }
}
