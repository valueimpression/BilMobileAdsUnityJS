mergeInto(LibraryManager.library, {
	InitSDK: function () {
		// window.alert("InitSDK");
	},

	PreLoadRewardedAd: function (rewardedType) {
		try {
			if (
				typeof window.BilUnityJS == "undefined" ||
				typeof window.BilUnityJS.PreLoadRewardedAd == "undefined"
			) return;

			const type = UTF8ToString(rewardedType);
			window.BilUnityJS.PreLoadRewardedAd(type).then(function (response) {
				SendMessage("BilUnityJS", "RewardedAdCallback", JSON.stringify(response));
			}).catch(function (err) {
				SendMessage("BilUnityJS", "RewardedAdCallback", JSON.stringify(err));
			});
		} catch (error) {
			console.log("PreLoadRewardedAd: " + error);
		}
	},
	ShowRewardedAd: function (rewardedType) {
		try {
			if (
				typeof window.BilUnityJS == "undefined" ||
				typeof window.BilUnityJS.ShowRewardedAd == "undefined"
			) return;

			const type = UTF8ToString(rewardedType);
			window.BilUnityJS.ShowRewardedAd(type).then(function (response) {
				SendMessage("BilUnityJS", "RewardedAdCallback", JSON.stringify(response));
			}).catch(function (err) {
				SendMessage("BilUnityJS", "RewardedAdCallback", JSON.stringify(err));
			});
		} catch (error) {
			console.log("PreLoadRewardedAd: " + error);
		}
	},
	IsRewardedAdReady: function (rewardedType) {
		try {
			if (
				typeof window.BilUnityJS == "undefined" ||
				typeof window.BilUnityJS.IsRewardedAdReady == "undefined"
			) return;

			const type = UTF8ToString(rewardedType);
			window.BilUnityJS.IsRewardedAdReady(type).then(function (response) {
				SendMessage("BilUnityJS", "RewardedAdCallback", JSON.stringify(response));
			}).catch(function (err) {
				SendMessage("BilUnityJS", "RewardedAdCallback", JSON.stringify(err));
			});
		} catch (error) {
			console.log("PreLoadRewardedAd: " + error);
		}
	},

	PreLoadInterstitialAd: function () {
		try {
			if (
				typeof window.BilUnityJS == "undefined" ||
				typeof window.BilUnityJS.PreLoadInterstitialAd == "undefined"
			) return;

			window.BilUnityJS.PreLoadInterstitialAd().then(function (response) {
				SendMessage("BilUnityJS", "PreloadInterstitialCallback", 1);
			}).catch(function (err) {
				SendMessage("BilUnityJS", "PreloadInterstitialCallback", 0);
			});
		} catch (error) {
			console.log("PreLoadInterstitialAd: " + error);
		}
	},
	ShowInterstitialAd: function () {
		try {
			if (
				typeof window.BilUnityJS == "undefined" ||
				typeof window.BilUnityJS.ShowInterstitialAd == "undefined"
			) return;

			window.BilUnityJS.ShowInterstitialAd().then(function (response) {
				SendMessage("BilUnityJS", "InterstitialAdCallback", JSON.stringify(response));
			}).catch(function (err) {
				SendMessage("BilUnityJS", "InterstitialAdCallback", JSON.stringify(err));
			});
		} catch (error) {
			console.log("ShowInterstitialAd: " + error);
		}
	},

	ShowBannerAd: function (elemID, adSize) {
		try {
			if (
				typeof window.BilUnityJS == "undefined" ||
				typeof window.BilUnityJS.ShowAd == "undefined"
			) return;

			const elem = UTF8ToString(elemID);
			const size = UTF8ToString(adSize);
			window.BilUnityJS.ShowBanner(elem, size).then(function (response) {
				SendMessage("BilUnityJS", "BannerAdCallback", JSON.stringify(response));
			}).catch(function (err) {
				SendMessage("BilUnityJS", "BannerAdCallback", JSON.stringify(err));
			});
		} catch (error) {
			console.log("ShowBannerAd: " + error);
		}
	},

	OnSendEvent: function (eventName) {
		if (
			typeof BilUnityJS == "undefined" &&
			typeof BilUnityJS.OnSendEvent == "undefined"
		) return;

		const event = UTF8ToString(eventName);
		BilUnityJS.OnSendEvent(event);
	}
});
