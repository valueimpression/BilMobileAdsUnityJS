mergeInto(LibraryManager.library, {
	InitSDK: function () {
		// window.alert("InitSDK");
	},

	PreLoadRewardedAd: function (rewardedType) {
		try {
			if (
				typeof window.AntGamesSDK == "undefined" ||
				typeof window.AntGamesSDK.PreLoadRewardedAd == "undefined"
			) return;

			const type = UTF8ToString(rewardedType);
			window.AntGamesSDK.PreLoadRewardedAd(type).then(function (response) {
				SendMessage("AntGamesSDK", "RewardedAdCallback", JSON.stringify(response));
			}).catch(function (err) {
				SendMessage("AntGamesSDK", "RewardedAdCallback", JSON.stringify(err));
			});
		} catch (error) {
			console.log("PreLoadRewardedAd: " + error);
		}
	},
	ShowRewardedAd: function (rewardedType) {
		try {
			if (
				typeof window.AntGamesSDK == "undefined" ||
				typeof window.AntGamesSDK.ShowRewardedAd == "undefined"
			) return;

			const type = UTF8ToString(rewardedType);
			window.AntGamesSDK.ShowRewardedAd(type).then(function (response) {
				SendMessage("AntGamesSDK", "RewardedAdCallback", JSON.stringify(response));
			}).catch(function (err) {
				SendMessage("AntGamesSDK", "RewardedAdCallback", JSON.stringify(err));
			});
		} catch (error) {
			console.log("PreLoadRewardedAd: " + error);
		}
	},
	IsRewardedAdReady: function (rewardedType) {
		try {
			if (
				typeof window.AntGamesSDK == "undefined" ||
				typeof window.AntGamesSDK.IsRewardedAdReady == "undefined"
			) return;

			const type = UTF8ToString(rewardedType);
			window.AntGamesSDK.IsRewardedAdReady(type).then(function (response) {
				SendMessage("AntGamesSDK", "RewardedAdCallback", JSON.stringify(response));
			}).catch(function (err) {
				SendMessage("AntGamesSDK", "RewardedAdCallback", JSON.stringify(err));
			});
		} catch (error) {
			console.log("PreLoadRewardedAd: " + error);
		}
	},

	PreLoadInterstitialAd: function () {
		try {
			if (
				typeof window.AntGamesSDK == "undefined" ||
				typeof window.AntGamesSDK.PreLoadInterstitialAd == "undefined"
			) return;

			window.AntGamesSDK.PreLoadInterstitialAd().then(function (response) {
				SendMessage("AntGamesSDK", "InterstitialAdCallback", JSON.stringify(response));
			}).catch(function (err) {
				SendMessage("AntGamesSDK", "InterstitialAdCallback", JSON.stringify(err));
			});
		} catch (error) {
			console.log("PreLoadInterstitialAd: " + error);
		}
	},
	ShowInterstitialAd: function () {
		try {
			if (
				typeof window.AntGamesSDK == "undefined" ||
				typeof window.AntGamesSDK.ShowInterstitialAd == "undefined"
			) return;

			window.AntGamesSDK.ShowInterstitialAd().then(function (response) {
				SendMessage("AntGamesSDK", "InterstitialAdCallback", JSON.stringify(response));
			}).catch(function (err) {
				SendMessage("AntGamesSDK", "InterstitialAdCallback", JSON.stringify(err));
			});
		} catch (error) {
			console.log("ShowInterstitialAd: " + error);
		}
	},

	ShowBannerAd: function (slotID, adSize, position) {
		try {
			if (
				typeof window.AntGamesSDK == "undefined" ||
				typeof window.AntGamesSDK.ShowBanner == "undefined"
			) return;

			const id = UTF8ToString(slotID);
			const size = UTF8ToString(adSize);
			const pos = UTF8ToString(position);
			window.AntGamesSDK.ShowBanner(id, size, pos).then(function (response) {
				SendMessage("AntGamesSDK", "BannerAdCallback", JSON.stringify(response));
			}).catch(function (err) {
				SendMessage("AntGamesSDK", "BannerAdCallback", JSON.stringify(err));
			});
		} catch (error) {
			console.log("ShowBannerAd: " + error);
		}
	},
	DestroyBannerAd: function (slotID) {
		try {
			if (
				typeof window.AntGamesSDK == "undefined" ||
				typeof window.AntGamesSDK.DestroyBanner == "undefined"
			) return;

			const id = UTF8ToString(slotID);
			window.AntGamesSDK.DestroyBanner(id).then(function (response) {
				SendMessage("AntGamesSDK", "BannerAdCallback", JSON.stringify(response));
			}).catch(function (err) {
				SendMessage("AntGamesSDK", "BannerAdCallback", JSON.stringify(err));
			});
		} catch (error) {
			console.log("DestroyBannerAd: " + error);
		}
	},

	OnSendEvent: function (eventData) {
		try {
			if (
				typeof window.AntGamesSDK == "undefined" ||
				typeof window.AntGamesSDK.OnSendEvent == "undefined"
			) return;

			const eventN = UTF8ToString(eventData);
			window.AntGamesSDK.OnSendEvent(eventN).then(function (response) {
				SendMessage("AntGamesSDK", "OnSendEventCallback", JSON.stringify(response));
			}).catch(function (err) {
				SendMessage("AntGamesSDK", "OnSendEventCallback", JSON.stringify(err));
			});
		} catch (error) {
			console.log("OnSendEvent: " + error);
		}
	}
});
