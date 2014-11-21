using LoopMeSDK.Network;
using System.Collections.Generic;

namespace LoopMeSDK.Interstitial
{
    class LoopMeInterstitial
    {
        private static List<LoopMeInterstitial> _sharedInterstitials;

        private LoopMeInterstitialManager manager;

        public string AppKey { get; private set; }
        public bool IsReady { get; private set; }
        public bool IsLoading { get; private set; }

        public LoopMeInterstitial(string appKey) 
        {
           this.AppKey = appKey;
        }

        ~LoopMeInterstitial()
        {

        }

        public void LoadAd()
        {

        }
    }
}
