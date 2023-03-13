using AppTestRSSReader.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTestRSSReader.Services.Common
{
    public partial class Settings : ISettings
    {
        #region names
        private readonly string _syncfusionLicence = "OTIzNjc1QDMyMzAyZTM0MmUzMGZLMllvSlp2aGpoOENjZnJqUDRET2xWZmsvZCs5WDBSYXVWWGFjdi9Kak09";
        #endregion names

        #region interfaz implementation
        public string GetEntryPointRSS(EnumTypeRSS typeRSS)
        {
            string defaultUri = "http://www.cnet.com/rss/news/";
            switch (typeRSS)
            {
                case EnumTypeRSS.Tecnologia: return "http://megaflux.macg.co/";
                case EnumTypeRSS.Deportes: return "http://sports.espn.go.com/espn/rss/news";
                case EnumTypeRSS.Politica: return "http://www.politico.com/rss/magazine.xml";
                case EnumTypeRSS.Negocios: return "http://www.business-standard.com/rss/latest.rss";
                case EnumTypeRSS.Salud: return "http://www.health.com/health/healthy-happy/feed";
                case EnumTypeRSS.Entretenimiento: return "http://news.yahoo.com/rss/entertainment";
            }

            return defaultUri;
        }
        public string GetSyncfusionLicence()
        {
            return _syncfusionLicence;
        }
        #endregion interfaz implementation
    }
}
