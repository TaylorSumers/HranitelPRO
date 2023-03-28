using AppConnect.DatabaseModel;
using GeneralDepartment.Windows;
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
using static AppConnect.Connection;

namespace GeneralDepartment.Pages
{
    /// <summary>
    /// Interaction logic for RequestsPage.xaml
    /// </summary>
    public partial class RequestsPage : Page
    {
        static IEnumerable<dynamic> Requests;
        public RequestsPage() //TODO: Fix cbxs
        {
            InitializeComponent();
            RefreshData();
            cbxType.ItemsSource = cont.VisitType.ToList();
            cbxStatus.ItemsSource = cont.VisitStatus.ToList();
            cbxSubdiv.ItemsSource = cont.Subdivision.ToList();
        }

        private void RefreshData()
        {
            Requests = from VisitRequests in cont.VisitRequest
                       join Users in cont.User on VisitRequests.VisitUser equals Users.UserID
                       join Subdivisions in cont.Subdivision on VisitRequests.VisitSubdivision equals Subdivisions.SubdivisionID
                       join Employees in cont.Employee on VisitRequests.VisitEmployee equals Employees.EmployeeID
                       join VisitTypes in cont.VisitType on VisitRequests.VisitType equals VisitTypes.VisitTypeID
                       join VisitStatuses in cont.VisitStatus on VisitRequests.VisitStatus equals VisitStatuses.VisitStatusID
                       join VisitPurposes in cont.VisitPurpose on VisitRequests.VisitPurpose equals VisitPurposes.VisitPurposeID
                       select new 
                       { 
                           VisitRequests.VisitRequestID,
                           Users.UserName,
                           Users.UserSurname,
                           Users.UserPatronymic, 
                           Subdivisions.SubdivisionName,
                           Employees.EmployeeSurname,
                           Employees.EmployeeCode,
                           VisitRequests.VisitDateStart,
                           VisitRequests.VisitDateEnd,
                           VisitTypes.VisitTypeName,
                           VisitPurposes.VisitPurposeName,
                           VisitStatuses.VisitStatusName 
                       };
            dtgRequests.ItemsSource = Requests.ToList();
        }

        private void cbxType_SelectionChanged(object sender, SelectionChangedEventArgs e) //TODO: Fix sorting
        {
            string SelectedType = (string)cbxType.SelectedValue;
            dtgRequests.ItemsSource = Requests.Where(req => req.VisitTypeName == SelectedType).ToList();
        }

        private void cbxSubdiv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string SelectedSubdiv = (string)cbxSubdiv.SelectedValue;
            dtgRequests.ItemsSource = Requests.Where(req => req.SubdivisionName == SelectedSubdiv).ToList();
        }

        private void cbxStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string SelectedStatus = (string)cbxStatus.SelectedValue;
            dtgRequests.ItemsSource= Requests.Where(req => req.VisitStatusName == SelectedStatus).ToList();
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

