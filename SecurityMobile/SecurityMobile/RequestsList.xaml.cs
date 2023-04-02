using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SecurityMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RequestsList : ContentPage
    {
        List<Request> Requests;
        public RequestsList()
        {
            InitializeComponent();
            var client = new WebClient();
            //TODO: normal api connection
            //var response = client.DownloadString("http://localhost:51091/api/VisitRequests");
            var response = "[{" + "\"VisitRequestID\":4,\"UserName\":\"Радинка\",\"UserSunrame\":\"Степанова\",\"UserPatronymic\":\"Власовна\",\"VisitType\":\"Личное\",\"Subdivision\":\"Производство\",\"EmployeeSurname\":\"Фомичева\",\"VisitDateTime\":\"2023-03-27T12:00:00\"},{\"VisitRequestID\":6,\"UserName\":\"Юрин\",\"UserSunrame\":\"Кононов\",\"UserPatronymic\":\"Романович\",\"VisitType\":\"Личное\",\"Subdivision\":\"Администрация\",\"EmployeeSurname\":\"Носкова\",\"VisitDateTime\":\"2023-04-02T09:00:00\"},{\"VisitRequestID\":7,\"UserName\":\"Альбина\",\"UserSunrame\":\"Елисеева\",\"UserPatronymic\":\"Николаевна\",\"VisitType\":\"Личное\",\"Subdivision\":\"Производство\",\"EmployeeSurname\":\"Фомичева\",\"VisitDateTime\":\"2023-03-31T20:00:00\"}]";
            Requests = JsonConvert.DeserializeObject<List<Request>>(response);
            LVRequests.ItemsSource = Requests.Where(req => req.VisitDateTime.Date == DateTime.Now.Date);
        }
        // TODO: Fix pickers

        private void LVRequests_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new RequestInfoPage(LVRequests.SelectedItem as Request));
        }

        private void pSubdiv_SelectedIndexChanged(object sender, EventArgs e)
        { 
            LVRequests.ItemsSource = Requests.Where(req => req.Subdivision == (string)pSubdiv.SelectedItem).ToList();
        }

        private void pType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LVRequests.ItemsSource = Requests.Where(req => req.VisitType == (string)pType.SelectedItem).ToList();
        }

        private void dpDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            LVRequests.ItemsSource = Requests.Where(req => req.VisitDateTime.Date == dpDate.Date).ToList();
        }
    }
}