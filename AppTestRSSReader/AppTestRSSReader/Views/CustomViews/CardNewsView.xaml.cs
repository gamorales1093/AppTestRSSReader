
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTestRSSReader.Views.CustomViews
{
    public partial class CardNewsView : ViewCell 
    {
        public static readonly BindableProperty ParentBindingContextProperty =
   BindableProperty.Create(nameof(ParentBindingContext), typeof(object), typeof(CardNewsView), null);
        public CardNewsView()
        {
            InitializeComponent();
        }
        public object ParentBindingContext
        {
            get { return this.GetValue(ParentBindingContextProperty); }
            set { this.SetValue(ParentBindingContextProperty, value); }
        }
    }
}