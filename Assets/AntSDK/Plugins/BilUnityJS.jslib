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
				SendMessage("BilUnityJS", "InterstitialAdCallback", JSON.stringify(response));
			}).catch(function (err) {
				SendMessage("BilUnityJS", "InterstitialAdCallback", JSON.stringify(err));
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

	ShowBannerAd: function (slotID, adSize, position) {
		try {
			if (
				typeof window.BilUnityJS == "undefined" ||
				typeof window.BilUnityJS.ShowBanner == "undefined"
			) return;

			const id = UTF8ToString(slotID);
			const size = UTF8ToString(adSize);
			const pos = UTF8ToString(position);
			window.BilUnityJS.ShowBanner(id, size, pos).then(function (response) {
				SendMessage("BilUnityJS", "BannerAdCallback", JSON.stringify(response));
			}).catch(function (err) {
				SendMessage("BilUnityJS", "BannerAdCallback", JSON.stringify(err));
			});
		} catch (error) {
			console.log("ShowBannerAd: " + error);
		}
	},
	DestroyBannerAd: function (slotID) {
		try {
			if (
				typeof window.BilUnityJS == "undefined" ||
				typeof window.BilUnityJS.DestroyBanner == "undefined"
			) return;

			const id = UTF8ToString(slotID);
			window.BilUnityJS.DestroyBanner(id).then(function (response) {
				SendMessage("BilUnityJS", "BannerAdCallback", JSON.stringify(response));
			}).catch(function (err) {
				SendMessage("BilUnityJS", "BannerAdCallback", JSON.stringify(err));
			});
		} catch (error) {
			console.log("DestroyBannerAd: " + error);
		}
	},

	OnSendEvent: function (eventData) {
		try {
			if (
				typeof window.BilUnityJS == "undefined" ||
				typeof window.BilUnityJS.OnSendEvent == "undefined"
			) return;

			const eventN = UTF8ToString(eventData);
			window.BilUnityJS.OnSendEvent(eventN).then(function (response) {
				SendMessage("BilUnityJS", "OnSendEventCallback", JSON.stringify(response));
			}).catch(function (err) {
				SendMessage("BilUnityJS", "OnSendEventCallback", JSON.stringify(err));
			});
		} catch (error) {
			console.log("OnSendEvent: " + error);
		}
	}
});