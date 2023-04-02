using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using Xamarin.Forms;

namespace SecurityMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            // TODO: Check code
            Navigation.PushAsync(new RequestsList());
        }
    }
}
