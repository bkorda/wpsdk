using System;
using System.Text;

namespace LoopMeSDK.Builder
{
    class LoopMeServerUriBuilder
    {
        const string LOOP_ME_API_URL = "http://loopme.me/api/loopme/ads"; 
        const string INTERFACE_ORIENTATION_PORTRAIT = "p"; 
        const string INTERFACE_ORIENTATION_LANDSCAPE = "l";

        public static Uri BuildUri(string appkey, bool testmode, string baseUriString = LOOP_ME_API_URL)
        {
            var advertisingId = Windows.System.UserProfile.AdvertisingManager.AdvertisingId;
            string uriParams = String.Format("?ak={0}&uid=&vt={1}", appkey, advertisingId);

            //    Common params
            StringBuilder uriParamsBuilder = new StringBuilder(uriParams);
            uriParamsBuilder.Append(LoopMeServerUriBuilder.RequestParameterForLanguage());
            uriParamsBuilder.Append(LoopMeServerUriBuilder.RequestParameterForApplicationVersion());
            uriParamsBuilder.Append(LoopMeServerUriBuilder.RequestParameterForOrientation());
            uriParamsBuilder.Append(LoopMeServerUriBuilder.RequestParameterForTimeZone());
            uriParamsBuilder.Append(LoopMeServerUriBuilder.RequestParameterForISOCountryCode());
            uriParamsBuilder.Append(LoopMeServerUriBuilder.RequestParameterForConnectionType());
            uriParamsBuilder.Append(LoopMeServerUriBuilder.RequestParameterForDNT());
            uriParamsBuilder.AppendFormat("&sv={0}", LoopMeConstants.LOOPME_SDK_VERSION);
            uriParamsBuilder.Append("&mr=0");
            if (testmode)
                uriParamsBuilder.Append("&tm=1");

            //    Receiving javascript callbacks
            uriParamsBuilder.Append("&fail=loopme");
            string escapedParams = Uri.EscapeDataString(uriParamsBuilder.ToString());

            Uri baseUri = new Uri(baseUriString);
            Uri finalUri = new Uri(baseUri, escapedParams);
      
            return finalUri; 
        }

        private static string RequestParameterForDNT()
        {
            throw new NotImplementedException();
        }

        private static string RequestParameterForConnectionType()
        {
            throw new NotImplementedException();
        }

        private static string RequestParameterForISOCountryCode()
        {
            throw new NotImplementedException();
        }

        private static string RequestParameterForTimeZone()
        {
            throw new NotImplementedException();
        }

        private static string RequestParameterForOrientation()
        {
            throw new NotImplementedException();
        }

        private static string RequestParameterForApplicationVersion()
        {
            throw new NotImplementedException();
        }

        private static string RequestParameterForLanguage()
        {
            throw new NotImplementedException();
        }
    }
}
