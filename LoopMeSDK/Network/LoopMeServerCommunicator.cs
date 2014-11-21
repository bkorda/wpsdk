using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http;

namespace LoopMeSDK.Network
{
    class LoopMeServerCommunicator
    {
        //Signals to a CancellationToken that it should be canceled.
        private HttpClient _httpClient;
        private CancellationTokenSource _cts;

        #region Properies
        private string _userAgent;
        public string UserAgent
        {
            get
            {
                if (String.IsNullOrEmpty(_userAgent))
                {
                    WebView webView = new WebView();
                    string[] args = { "navigator.userAgent" };
                    _userAgent = webView.InvokeScript("eval", args);
                }
                return _userAgent;    
            }
        }

        public bool IsLoading { get; private set; }
        #endregion

        #region Event Handlers
        public event EventHandler<FailWithErrorEventArgs> FailWithError;
        public event EventHandler<RecievedAdConfigurationEventArgs> RecievedAdConfiguration;
        #endregion

        #region Events
        protected virtual void OnFailWithError(FailWithErrorEventArgs e)
        {
            EventHandler<FailWithErrorEventArgs> handler = FailWithError;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnRecievedAdConfiguration(RecievedAdConfigurationEventArgs e)
        {
            EventHandler<RecievedAdConfigurationEventArgs> handler = RecievedAdConfiguration;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion

        #region Private
        private string ErrorMsg(HttpStatusCode statusCode)
        {
           string errorMessage; 
         // Server returns 404 status code for incorrect appKey 
         if (statusCode == HttpStatusCode.NotFound) { 
             errorMessage = "Missing or invalid appkey"; 
         } else if (statusCode == HttpStatusCode.NoContent) { 
             errorMessage = "No ads found"; 
         } else { 
             errorMessage = String.Format("API returned status code {0}.", statusCode); 
         } 
         
         return errorMessage;
        }
        #endregion

        #region Public
        public LoopMeServerCommunicator ()
        {
            _httpClient = new HttpClient();
            _cts = new CancellationTokenSource();
        }

        public async Task LoadUri(Uri uri) 
        {
            Cancel();
            try
            {
                IsLoading = true;
                HttpResponseMessage response = await _httpClient.GetAsync(uri).AsTask(_cts.Token);
                if (response.StatusCode != HttpStatusCode.Ok)
                {
                    Cancel();
                    string errorMsg = ErrorMsg(response.StatusCode);
                    FailWithErrorEventArgs errEventArgs = new FailWithErrorEventArgs() {ErrorMsg = errorMsg};
                    OnFailWithError(errEventArgs);
                    return;
                }

                var result = await response.Content.ReadAsStringAsync();
                LoopMeAdConfiguration conf = new LoopMeAdConfiguration(result);
                IsLoading = false;

                RecievedAdConfigurationEventArgs confEventArgs = new RecievedAdConfigurationEventArgs { configuration = conf };
                OnRecievedAdConfiguration(confEventArgs);
            }
            catch (TaskCanceledException)
            {
                //canceled
            }
        }

        public void Cancel()
        {
            _cts.Cancel();
            _cts.Dispose();

            // Re-create the CancellationTokenSource.
            _cts = new CancellationTokenSource();
            IsLoading = false;
        }

        public void Dispose()
        {
            if (_httpClient != null)
            {
                _httpClient.Dispose();
                _httpClient = null;
            }

            if (_cts != null)
            {
                _cts.Dispose();
                _cts = null;
            }
            IsLoading = false;
        }
        #endregion
    }
        

    class FailWithErrorEventArgs : EventArgs
    {
        public string ErrorMsg;
    }

    class RecievedAdConfigurationEventArgs : EventArgs
    {
        public LoopMeAdConfiguration configuration;
    }
}
