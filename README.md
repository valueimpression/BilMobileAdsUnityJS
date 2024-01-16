* Các bước tích hợp Ads vào Unity
- B1:
    Create GameObject với tên "BilUnityJS" và Kéo file "BilUnityJS.cs" vào GameObject đó.
    Đưa GameObject "BilUnityJS" vào trong Hierarchy.
- B2:
    Tạo folder "Plugins" trong Assets và copy file "BilUnityJS.jslib" vào folder đó.
- B3: 
    Tích hợp code chạy demo trong file GameManager.cs


* Tích hợp Ads trong code C#:
1. Banner: init BannerAd trước khi show banner
    - Init: BannerAd banner = new BannerAd(slotID, adSize, position);
        + slotID: ID unique của banner (Tự định nghĩa)
        + adSize: kích thước banner
            Default size dùng BannerSize (vd: BannerSize.BANNER);
            Custom size dùng AdSize (vd: new AdSize(320, 50));
        + position: vị trí hiển thị banner
            Default AdPosition.Top or AdPosition.Bottom Custom position
    - Show: banner.ShowAd();
    - Destroy: banner.Destroy();

    VD:
        BannerAd banner = new BannerAd("banner_top", BannerSize.BANNER, AdPosition.BOTTOM);
        AdSize size = new AdSize(300, 250);
        BannerAd banner = new BannerAd("banner_top", size, AdPosition.BOTTOM); // Custom size
        BannerAd banner = new BannerAd("banner_top", size, 0, 50); // Custom position
        banner.ShowBanner();

2. Reward: 
    slotID: ID unique của reward (Tự định nghĩa), dùng để preload và show theo slotID

    - Preload Ad trước khi show reward
        BilUnityJS.Instance.PreloadRewardedAd(slotID);
            VD: BilUnityJS.Instance.PreLoadRewarded("coin");
    - Show: Bắt buộc phải preload trước khi show
        BilUnityJS.Instance.ShowRewardedAd(slotID);
            VD: BilUnityJS.Instance.ShowRewarded("coin");

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