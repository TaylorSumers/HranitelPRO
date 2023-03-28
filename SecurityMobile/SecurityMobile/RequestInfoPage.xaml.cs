using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SecurityMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RequestInfoPage : ContentPage
    {

        public RequestInfoPage(Request currentRequest)
        {
            InitializeComponent();
            BindingContext = currentRequest;
        }

        private void btnOpenTournicket_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Сообщение", "Турникет открыт", "OK");
            btnOpenTournicket.IsEnabled = false;
        }

        private void btnSave_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Сообщение", "Уход отмечен", "OK");
            btnSave.IsEnabled = false;
        }
    }
}