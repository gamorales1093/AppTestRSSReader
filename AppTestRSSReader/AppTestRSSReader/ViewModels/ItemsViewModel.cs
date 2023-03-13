using AppTestRSSReader.Data;
using AppTestRSSReader.Models;
using AppTestRSSReader.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppTestRSSReader.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private NewsData _selectedItem;

        public ObservableCollection<NewsData> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<NewsData> ItemTapped { get; }

        public ItemsViewModel()
        {
            Title = "Noticias Guardadas";
            Items = new ObservableCollection<NewsData>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<NewsData>(OnItemSelected);

        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                RssDataBase database = await RssDataBase.Instance;
                var items =  await database.GetItemsAsync();
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public NewsData SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        async void OnItemSelected(NewsData item)
        {
            if (item == null)
                return;

            await Browser.OpenAsync(item.link);
        }
    }
}