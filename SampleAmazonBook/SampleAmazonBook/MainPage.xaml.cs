using Newtonsoft.Json;
using SampleAmazonBook.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SampleAmazonBook
{
    class Item
    {
        public string itemName { get; set; }

        public Item(string name)
        {
            itemName = name;
        }
    }
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private string _base;
        private ObservableCollection<Item> _items;

        private HttpClient _client;

        public MainPage()
        {
            InitializeComponent();

            _base   = "https://www.googleapis.com/books/v1/volumes?q=search";
            _client = new HttpClient();
            _items  = new ObservableCollection<Item>();
            updateBookList("harry");
        }

        private async void updateBookList(string titleQuery)
        {
            var responce = await _client.GetStringAsync(_base + "+" + titleQuery);

            GoogleBooksVolume books = JsonConvert.DeserializeObject<GoogleBooksVolume>(responce);

            _items.Clear();
            var list = books.items;
            foreach (var element in list)
            {
                _items.Add(new Item(element.volumeInfo.title));
            }

            bookList.ItemsSource = _items;
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            //DebugOut.Text = "Entry_TextChanged";

            updateBookList(e.NewTextValue);
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            DebugOut.Text = "Entry_" +
                "Completed";

            //updateBookList(e.);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            DebugOut.Text = "Button_Clicked";
        }

        private void OnMore(object sender, EventArgs e)
        {
            DebugOut.Text = "OnMore";
        }

        private void OnDelete(object sender, EventArgs e)
        {
            DebugOut.Text = "OnDelete";
        }
    }
}
