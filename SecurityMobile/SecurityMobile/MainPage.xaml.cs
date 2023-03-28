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
            var client = new WebClient();
            //TODO: normal api connection
            //var response = client.DownloadString("http://localhost:51091/api/VisitRequests");
            var response = "[{" + "\"VisitRequestID\":4,\"UserName\":\"Радинка\",\"UserSunrame\":\"Степанова\",\"UserPatronymic\":\"Власовна\",\"VisitType\":\"Личное\",\"Subdivision\":\"Производство\",\"EmployeeSurname\":\"Фомичева\",\"VisitDateTime\":\"2023-03-27T12:00:00\"},{\"VisitRequestID\":6,\"UserName\":\"Юрин\",\"UserSunrame\":\"Кононов\",\"UserPatronymic\":\"Романович\",\"VisitType\":\"Личное\",\"Subdivision\":\"Администрация\",\"EmployeeSurname\":\"Носкова\",\"VisitDateTime\":\"2023-03-30T09:00:00\"},{\"VisitRequestID\":7,\"UserName\":\"Альбина\",\"UserSunrame\":\"Елисеева\",\"UserPatronymic\":\"Николаевна\",\"VisitType\":\"Личное\",\"Subdivision\":\"Производство\",\"EmployeeSurname\":\"Фомичева\",\"VisitDateTime\":\"2023-03-31T20:00:00\"}]";
            LVRequests.ItemsSource = JsonConvert.DeserializeObject<List<Request>>(response);
        }

        private void LVRequests_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new RequestInfoPage(LVRequests.SelectedItem as Request));
        }
    }
}
