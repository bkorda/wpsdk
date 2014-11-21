using System;
using System.Diagnostics;
using Windows.Data.Json;

namespace LoopMeSDK.Network
{
    class LoopMeAdConfiguration
    {
        const int BANNER_REFRESH_INTERVAL_TIMER_MIN = 30;
        const int BANNER_REFRESH_INTERVAL_TIMER_MAX = 300; 
        const int EXPIRE_TIME_INTERVAL_MIN = 600; 

        
        public LoopMeAdType AdType { get; set; }
        public LoopMeAdOrientation Orientation { get; set; }
        public Int32 ExpirationTime { get; set; }
        public Int32 BannerRefreshInterval { set; get; }
        public string AdResponseHTMLString { set; get; }

        public LoopMeAdConfiguration(string responseJson)
        {
            JsonValue json;
            if (JsonValue.TryParse(responseJson, out json))
            {
                AdResponseHTMLString = json.GetObject().GetNamedString("script");
                MapConfiguration(json);
            }
        }

        private void MapConfiguration(JsonValue json)
        {
            JsonObject settings = json.GetObject().GetNamedObject("settings");
            if (settings.GetNamedString("format").Equals("banner"))
                AdType = LoopMeAdType.LoopMeAdTypeBanner;
            else
                AdType = LoopMeAdType.LoopMeAdTypeInterstitial;

            BannerRefreshInterval = (int)settings.GetNamedNumber("ad_refresh_time");

            if (BannerRefreshInterval > BANNER_REFRESH_INTERVAL_TIMER_MAX) 
                BannerRefreshInterval = BANNER_REFRESH_INTERVAL_TIMER_MAX;
            else if (BannerRefreshInterval < BANNER_REFRESH_INTERVAL_TIMER_MIN)
                BannerRefreshInterval = BANNER_REFRESH_INTERVAL_TIMER_MIN;

            ExpirationTime = (int)settings.GetNamedNumber("ad_expiry_time");
            if (ExpirationTime < EXPIRE_TIME_INTERVAL_MIN)
                ExpirationTime = EXPIRE_TIME_INTERVAL_MIN; 

            if (settings.GetNamedString("orientation").Equals("landscape")) 
                Orientation = LoopMeAdOrientation.LoopMeAdOrientationLandscape; 
            else if (settings.GetNamedString("orientation").Equals("portrait")) 
                Orientation = LoopMeAdOrientation.LoopMeAdOrienationPortrait; 
            else
                Orientation = LoopMeAdOrientation.LoopMeAdOrientationUndefined; 
        }
    }

    enum LoopMeAdOrientation
    {
        LoopMeAdOrientationUndefined, 
        LoopMeAdOrienationPortrait, 
        LoopMeAdOrientationLandscape
    }

    enum LoopMeAdType
    { 
        LoopMeAdTypeUndefined, 
        LoopMeAdTypeInterstitial, 
        LoopMeAdTypeBanner
    } 
}
