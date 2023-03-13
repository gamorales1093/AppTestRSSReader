using AppTestRSSReader.Helpers;
using AppTestRSSReader.Models;
using AppTestRSSReader.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppTestRSSReader.Data
{
    public class RssDataBase
    {


        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<RssDataBase> Instance = new AsyncLazy<RssDataBase>(async () =>
        {
            var instance = new RssDataBase();
            CreateTableResult result = await Database.CreateTableAsync<NewsData>();
            return instance;
        });

        public RssDataBase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<List<NewsData>> GetItemsAsync()
        {
            return Database.Table<NewsData>().ToListAsync();
        }


        public Task<NewsData> GetItemAsync(int id)
        {
            return Database.Table<NewsData>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(NewsData item)
        {

            var dataExist = (Database.Table<NewsData>().Where(i => i.title == item.title));
            if (dataExist.CountAsync().Result > 0)
            {
                return Task.FromResult(-1);
            }
            else
                return Database.InsertAsync(item);
            
        }

        public Task<int> DeleteItemAsync(NewsData item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
