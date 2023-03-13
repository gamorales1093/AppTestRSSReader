using AppTestRSSReader.Helpers;
using AppTestRSSReader.Models;
using AppTestRSSReader.Services.Common;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppTestRSSReader.Services
{
    public partial class ProxyApi : IProxyApi
    {
        #region names
        protected readonly ISettings _settingsSrv;
        #endregion names

        #region constructor
        public ProxyApi()
        {
            _settingsSrv = DependencyService.Get<ISettings>();
        }
        #endregion constructor

        #region interfaz implementation
        public async Task<List<NewsData>> GetSyncFeedAsync(EnumTypeRSS typeRSS)
        {
            try
            {
                if (this.IsConnected())
                {
                    Debug.WriteLine("Starting network load feeds");
                    Debug.WriteLine("at : " + new DateTime().ToString());

                    Uri uri = new Uri(_settingsSrv.GetEntryPointRSS(typeRSS));
                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.GetAsync(uri);
                    String response_string = await response.Content.ReadAsStringAsync();
                    NewsItemParser parser = new NewsItemParser();
                    // List<FeedItem> list = await Task.Run(() => parser.ParseFeed(response_string));
                    List<NewsData> list = await Task.Run(() => parser.ParseNews(response_string));
                    return list;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion 

        #region methods
        public bool IsConnected()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                return true;
            }
            return false;
        }
        #endregion methods
    }
}
