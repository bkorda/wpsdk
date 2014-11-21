using LoopMeSDK.Network;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoopMeSDK.Interstitial
{
    public class LoopMeInterstitial
    {
        private LoopMeInterstitialManager _manager;

        public string AppKey { get; private set; }
        public bool IsReady { get; private set; }
        public bool IsLoading { get; private set; }

        public LoopMeInterstitial(string appKey) 
        {
           this.AppKey = appKey;
           _manager = new LoopMeInterstitialManager();
        }

        ~LoopMeInterstitial()
        {

        }

        public async Task LoadAd()
        {
            await _manager.LoadInterstitialAsync(AppKey, true);
        }
    }
}
