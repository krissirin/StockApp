// a Windows Phone 8 app which calls an operation on a RESTful web service and displays the results
// also uses ASP.Net Web API client utilities - NuGet install
// display on a long list selector (flat not grouped)

using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;

using System.Windows;

using StockViewWP_App;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
//using Windows.Web.Http;

namespace StockPricePhoneApp
{
    // main page for app
    public partial class MainPage : PhoneApplicationPage
    {
        // URI for RESTful service (implemented using Web API)
        private const String serviceURI = "http://stockupdatemvc.azurewebsites.net";

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        // display prices button clicked - event handler
        private async void Button_Click_DisplayPrices(object sender, RoutedEventArgs e)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(serviceURI);      // base URL for API Controller i.e. RESTFul service

                    // add an Accept header for JSON
                    client.DefaultRequestHeaders.
                        Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // GET ../api/stock
                    // get all stock listings asynchronously - await the result (i.e. block and return control to caller)
                    HttpResponseMessage response = await client.GetAsync("api/stock");

                    // continue
                    if (response.IsSuccessStatusCode)                                                   // 200.299
                    {
                        // read result and display on UI

                        var listings = await response.Content.ReadAsAsync<IEnumerable<Stock>>();

                        // set the data source for the priceList long list selector
                        priceList.ItemsSource = new ObservableCollection<Stock>(listings);
                    }
                    else
                    {
                        //
                    }
                }
            }
            catch (Exception)
            {
                //;
            }
        }
    }
}