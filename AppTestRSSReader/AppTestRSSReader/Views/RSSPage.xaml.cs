using AppTestRSSReader.Models;
using AppTestRSSReader.ViewModels;
using System;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTestRSSReader.Views
{
    public partial class RSSPage : ContentPage
    {
        RSSViewModel RSSFeedViewModelObject;
        public RSSPage()
        {
            InitializeComponent();
            Debug.WriteLine("Starting viewModel");
            RSSFeedViewModelObject = new RSSViewModel(Navigation);
            Title = "RSS";
            BindingContext = RSSFeedViewModelObject;

            // start the first refresh of the listview
            FeedListView.IsRefreshing = true;
            FeedListView.BeginRefresh();
        }
    }
}