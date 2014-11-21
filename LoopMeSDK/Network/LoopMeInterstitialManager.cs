using LoopMeSDK.Builder;
using System;
using Windows.UI.Xaml.Controls;
using LoopMeSDK.View;
using System.Threading.Tasks;

namespace LoopMeSDK.Network
{
    class LoopMeInterstitialManager
    {
        private LoopMeServerCommunicator _communicator;
        
        public bool IsReady {set; get; } 
        public bool IsLoading { set; get; }
        public bool IsDisplayed { get; set; }
        public string TestServerUri { get; set; }

        #region Event Handlers
        public event EventHandler InterstitialLoaded; 
        #endregion

        #region Events
        protected virtual void OnInterstitialLoaded()
        {
            EventHandler handler = InterstitialLoaded;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
        #endregion

        public LoopMeInterstitialManager()
        {
            _communicator = new LoopMeServerCommunicator();
        }
        #region Private
        private async Task LoadWithUriAsync(Uri uri)
        {
            if (this.IsLoading)
            {
                return;
            }

            this.IsLoading = true;
            await _communicator.LoadUri(uri);
        }
        #endregion

        #region Public
        public async Task LoadInterstitialAsync(string appkey, bool testMode)
        {
            if (this.IsReady)
            {
                OnInterstitialLoaded();
            }
            else
            {
                if (String.IsNullOrEmpty(TestServerUri))
                   await LoadWithUriAsync(LoopMeServerUriBuilder.BuildUri(appkey, testMode));
                else
                   await LoadWithUriAsync(LoopMeServerUriBuilder.BuildUri(appkey, testMode, TestServerUri));
            }
        }
         
        public void PresentInterstitialFromFrame(Frame frame)
        {
            frame.Navigate(typeof(LoopMeInterstitialPage));
        }
        #endregion
    }
}
