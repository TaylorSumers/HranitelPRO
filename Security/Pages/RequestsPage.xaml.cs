using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Security.Windows;
using static AppConnect.Connection;

namespace Security.Pages
{
    /// <summary>
    /// Interaction logic for RequestsPage.xaml
    /// </summary>
    
    public partial class RequestsPage : Page
    {
        static IEnumerable<dynamic> Requests;
        public RequestsPage()
        {
            InitializeComponent();
            RefreshData();
            cbxType.ItemsSource = cont.VisitType.ToList();
            cbxSubdiv.ItemsSource = cont.Subdivision.ToList();
        }

        private void RefreshData() // Refreshing data in datagrid
        {
            Requests = from VisitRequests in cont.VisitRequest
                       join Users in cont.User on VisitRequests.VisitUser equals Users.UserID
                       join Subdivisions in cont.Subdivision on VisitRequests.VisitSubdivision equals Subdivisions.SubdivisionID
                       join Employees in cont.Employee on VisitRequests.VisitEmployee equals Employees.EmployeeID
                       join VisitTypes in cont.VisitType on VisitRequests.VisitType equals VisitTypes.VisitTypeID
                       join VisitStatuses in cont.VisitStatus on VisitRequests.VisitStatus equals VisitStatuses.VisitStatusID
                       where VisitStatuses.VisitStatusID == 1
                       join VisitPurposes in cont.VisitPurpose on VisitRequests.VisitPurpose equals VisitPurposes.VisitPurposeID
                       join VisitDateTimes in cont.VisitTimeSpans on VisitRequests.VisitRequestID equals VisitDateTimes.VisitID
                       select new
                       {
                           VisitRequests.VisitRequestID,
                           Users.UserName,
                           Users.UserSurname,
                           Users.UserPatronymic,
                           Users.UserPassportNumber,
                           Subdivisions.SubdivisionID,
                           Subdivisions.SubdivisionName,
                           Employees.EmployeeSurname,
                           Employees.EmployeeCode,
                           VisitTypes.VisitTypeID,
                           VisitTypes.VisitTypeName,
                           VisitPurposes.VisitPurposeName,

                           VisitDateTimes.VisitDateTime,
                           VisitDateTimes.CheckPointIncome,
                           VisitDateTimes.CheckpointOutcome,
                           VisitDateTimes.SubdivisionIncome,
                           VisitDateTimes.SubdivisionOutcome
                       };
            dtgRequests.ItemsSource = Requests.ToList();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            tbxSearch.Text = "";
            cbxType.SelectedValue = null;
            cbxSubdiv.SelectedValue = null;
            dpDate.Text = "";
            RefreshData();
        }

        private void tbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Requests = Requests.Where(k => ($"{k.UserSurname} {k.UserName} {k.UserPatronymic} {k.UserPassportNumber}").Contains(tbxSearch.Text));
            dtgRequests.ItemsSource = Requests.ToList();
            if (tbxSearch.Text == "")
            {
                btnReset_Click(sender, e);
            }
        }

        private void cbxType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxType.SelectedValue != null)
            {
                Requests = Requests.Where(k => (int)cbxType.SelectedValue == k.VisitTypeID);
                dtgRequests.ItemsSource = Requests.ToList();
            }  
        }

        private void cbxSubdiv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxSubdiv.SelectedValue != null)
            {
                Requests = Requests.Where(k => (int)cbxSubdiv.SelectedValue == k.SubdivisionID);
                dtgRequests.ItemsSource = Requests.ToList();
            }
        }

        private void dpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dpDate.SelectedDate != null)
            {
                Requests = Requests.Where(k => k.VisitDateTime.Date ==  dpDate.SelectedDate);
                dtgRequests.ItemsSource = Requests.ToList();
            }
        }

        private void dtgRequests_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int SelectedRequest = (int)dtgRequests.SelectedValue;
            var InfoWin = new RequestInfoWindow(SelectedRequest);
            InfoWin.ShowDialog();
            RefreshData();
        }
    }
}
