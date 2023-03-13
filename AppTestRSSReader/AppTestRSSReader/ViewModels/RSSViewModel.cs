using AppTestRSSReader.Data;
using AppTestRSSReader.Helpers;
using AppTestRSSReader.Models;
using AppTestRSSReader.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppTestRSSReader.ViewModels
{
    public class RSSViewModel : BaseViewModel
    {
        ObservableCollection<NewsData> newsList = null;
        private IProxyApi _proxSrv = DependencyService.Get<IProxyApi>();

        public RSSViewModel()
        {
            Title = "News";
            SetFavouriteCommand = new Command<object>(btnSetFavourite);
        }

        public ICommand OpenWebCommand { get; }
        public ICommand SetFavouriteCommand { get; set; }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;
                    List<NewsData> list = await _proxSrv.GetSyncFeedAsync(EnumTypeRSS.Tecnologia);
                    NewsList = new ObservableCollection<NewsData>(list);
                    IsRefreshing = false;
                });
            }
        }


        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value); }
        }

        public ObservableCollection<NewsData> NewsList
        {
            get => newsList;
            set
            {
                if (newsList != value)
                {
                    SetProperty(ref newsList, value); 
                }
            }
        }

        private NewsData selectedItem = null;
        private INavigation Navigation;
        public NewsData SelectedItem
        {
            get => selectedItem;
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                    OpenWebPage();
                }
            }
        }



        // constructor
        public RSSViewModel(INavigation navigation)
        {
            //this.GetNewsFeedAsync();
            Navigation = navigation;
        }

        #region methods
        public async void OpenWebPage()
        {
            await Browser.OpenAsync(SelectedItem.link);
        }

        private async void btnSetFavourite(object obj)
        {
            var itemToSave = obj as NewsData;

            if(itemToSave!=null)
            {

            RssDataBase database = await RssDataBase.Instance;
            var result= await database.SaveItemAsync(itemToSave);
                if(result !=-1)
                    await Application.Current.MainPage.DisplayAlert("Alerta", "Noticia agregada a favoritos de manera exitosa", "Aceptar");
                else
                    await Application.Current.MainPage.DisplayAlert("Alerta", "La noticia ya se encuentra registrada en favoritos", "Aceptar");

            }    

             
        }
        #endregion methods
    }
}