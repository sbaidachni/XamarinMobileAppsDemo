using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin_MobileApps_Demo.Models;

namespace Xamarin_MobileApps_Demo.ViewModels
{
    class MainPageViewModel: INotifyPropertyChanged
    {
        public ObservableCollection<Product> Items { get; private set; }
        MobileServiceClient client;
        int loadedRows = 0;

        public event PropertyChangedEventHandler PropertyChanged;

        public int RecordsCount
        {
            get { return loadedRows; }
            set
            {
                loadedRows = value;
                if (PropertyChanged!=null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("RecordsCount"));
                }
            }
        }

        private static MainPageViewModel _instance = null;

        public static MainPageViewModel Instance
        {
            get
            {
                if (_instance==null)
                {
                    _instance = new MainPageViewModel();
                }
                return _instance;
            }
        }

        protected MainPageViewModel()
        {
            client = new MobileServiceClient("http://mobiletestbackend.azurewebsites.net");
            Items = new ObservableCollection<Product>();
        }

        public async Task LoadDataAsync()
        {
            var table=client.GetTable<vProductList>();
            var rows=await table.Skip(loadedRows).ToListAsync();
            foreach(var row in rows)
            {
                Items.Add(new Product(row.Id,row.Name,row.ProductNumber,row.Color,row.Size));
            }
            RecordsCount += rows.Count;

            ////test
            //for (int i=0;i<50;i++)
            //{
            //    Items.Add(new Product(RecordsCount+i, "Surface"));
            //}
            //RecordsCount += 50;
            ////end test


        }

    }

    class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ProductNumber { get; set; }

        public string Color { get; set; }

        public string Size { get; set; }

        public Product(int id,string name, string pnumber,string color,string size)
        {
            Id = id;
            Name = name;
            ProductNumber = pnumber;
            Color = color;
            Size = size;
        }
    }
}
