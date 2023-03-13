using AppTestRSSReader.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTestRSSReader.Services.Common
{
    public interface ISettings
    {
         string GetEntryPointRSS(EnumTypeRSS typeRSS);
         public string GetSyncfusionLicence();
    }
}
