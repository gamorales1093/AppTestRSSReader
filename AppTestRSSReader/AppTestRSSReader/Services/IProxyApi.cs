using AppTestRSSReader.Helpers;
using AppTestRSSReader.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppTestRSSReader.Services
{
    public interface IProxyApi
    {
       public Task<List<NewsData>> GetSyncFeedAsync(EnumTypeRSS typeRSS);
    }
}
