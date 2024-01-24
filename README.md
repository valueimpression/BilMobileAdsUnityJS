* Các bước tích hợp Ads vào Unity
- B1:
    Create GameObject với tên "AntGamesSDK" và Kéo file "BilUnityJS.cs" vào GameObject đó.
    Đưa GameObject "BilUnityJS" vào trong Hierarchy.
- B2:
    Tạo folder "Plugins" trong Assets và copy file "BilUnityJS.jslib" vào folder đó.
- B3: 
    Tích hợp code chạy demo trong file GameManager.cs



* Tích hợp Ads trong code C#:
1. Banner: init BannerAd trước khi show banner
    - Init: BannerAd banner = new BannerAd(slotID, position);
        + slotID: ID unique của banner tạo trên ant.game
        + position: vị trí hiển thị banner
            Default AdPosition.Top or AdPosition.Bottom Custom position
    - Show: banner.ShowAd();
    - Destroy: banner.Destroy();

    VD:
        BannerAd banner = new BannerAd("banner_top", AdPosition.BOTTOM);
        or
        BannerAd banner = new BannerAd("banner_top", 0, 50)
        banner.ShowBanner();
        banner.Destroy();

2. Reward:
    slotID: ID unique của reward tạo trên ant.game, dùng để preload và show theo slotID

    - Preload Ad trước khi show reward
        BilUnityJS.Instance.PreloadRewardedAd(slotID);
            VD: BilUnityJS.Instance.PreLoadRewarded(slotID);
    - Show: Bắt buộc phải preload trước khi show
        BilUnityJS.Instance.ShowRewardedAd(slotID);
            VD: BilUnityJS.Instance.ShowRewarded(slotID);

3. Interstitial:


4. SendEvent: Gửi Custom event
    VD:
        BannerData bannerData = new BannerData();
        bannerData.slotID = "banner1";
        EventData<BannerData> eventData = new EventData<BannerData>
        {
            eventName = "InitBanner",
            data = bannerData
        };
        BilUnityJS.Instance.SendEvent(eventData);
