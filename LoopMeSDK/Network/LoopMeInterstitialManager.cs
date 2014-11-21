using LoopMeSDK.Builder;
using System;

namespace LoopMeSDK.Network
{
    class LoopMeInterstitialManager
    {
        public bool IsReady {set; get; } 
        public bool IsLoading { set; get; }
        public bool IsDisplayed { get; set; }
        public string TestServerUri { get; set; }

        #region Event Handlers
        public event EventHandler InterstitialLoaded; 
        #endregion

        #region Events
        protected virtual void OnInterstitialLoaded(EventArgs e)
        {
            EventHandler handler = InterstitialLoaded;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion

        #region Private
        private void LoadWithUri(Uri uri)
        {
            if (this.IsLoading)
            {
                return;
            }

            this.IsLoading = true;

        }
        #endregion

        #region Public
        public void LoadInterstitial(string appkey, bool testMode)
        {
            if (this.IsReady)
            {
                OnInterstitialLoaded(EventArgs.Empty);
            }
            else
            {
                if (String.IsNullOrEmpty(TestServerUri))
                    LoadWithUri(LoopMeServerUriBuilder.BuildUri(appkey, testMode));
                else
                    LoadWithUri(LoopMeServerUriBuilder.BuildUri(appkey, testMode, TestServerUri));
            }
        }
        #endregion
    }
}
