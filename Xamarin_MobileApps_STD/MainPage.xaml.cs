using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin_MobileApps_Demo.ViewModels;

namespace Xamarin_MobileApps_Demo
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModel model;
        public MainPage()
        {
            InitializeComponent();
            model = MainPageViewModel.Instance;
            this.BindingContext = model;
        }

        protected async override void OnAppearing()
        {
            await model.LoadDataAsync();
            base.OnAppearing();
        }

        protected async void ListView_ItemsAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var itemTypeObject = e.Item as Product;
            if (model.Items.Last() == itemTypeObject)
            {
                await model.LoadDataAsync();
            }
        }
    }
}
