using AppConnect.DatabaseModel;
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
using System.Windows.Shapes;
using static AppConnect.Connection;

namespace Subdivision.Windows
{
    /// <summary>
    /// Interaction logic for RequestInfoWindow.xaml
    /// </summary>
    public partial class RequestInfoWindow : Window
    {
        static VisitTimeSpans currentRequestTimeSpans;
        static VisitRequest currentVisitRequest;
        public RequestInfoWindow(int selectedRequest)
        {
            InitializeComponent();
            var currentRequestData = GetRequestData(selectedRequest).ToList()[0];
            DataContext = currentRequestData;
            currentVisitRequest = cont.VisitRequest.Find(selectedRequest);
            currentRequestTimeSpans = cont.VisitTimeSpans.Find(selectedRequest);
            if (currentRequestTimeSpans.SubdivisionIncome == null)
                btnCheckIncome.IsEnabled = true;
            if (currentRequestTimeSpans.SubdivisionOutcome == null)
            {
                btnSave.IsEnabled = true;
                dpDate.IsEnabled = true;
                tbxHours.IsEnabled = true;
                tbxMinutes.IsEnabled = true;
            }
            else
            {
                dpDate.SelectedDate = currentRequestTimeSpans.CheckpointOutcome;
                tbxHours.Text = currentRequestTimeSpans.CheckpointOutcome.ToString().Substring(11, 2).Replace(":", "");
                tbxMinutes.Text = currentRequestTimeSpans.CheckpointOutcome.ToString().Substring(currentRequestTimeSpans.CheckpointOutcome.ToString().IndexOf(":") + 1, 2);
            }
        }

        private void btnCheckIncome_Click(object sender, RoutedEventArgs e)
        {
            if (currentRequestTimeSpans.CheckPointIncome != null)
            {
                currentRequestTimeSpans.SubdivisionIncome = DateTime.Now;
                cont.SaveChanges();
                MessageBox.Show("Приход в подразделение отмечен");
                btnCheckIncome.IsEnabled = false;
            }
            else
                MessageBox.Show("Нельзя отметить приход в подразделение, пока охранник не разрешил вход на территорию");
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DateTime dtOutcome = Convert.ToDateTime($"{dpDate.SelectedDate.ToString().Substring(0, 10)} {tbxHours.Text}:{tbxMinutes.Text}");
            currentRequestTimeSpans.SubdivisionOutcome = dtOutcome;
            cont.SaveChanges();
            MessageBox.Show("Уход из подразделения отмечен");
        }

        private void btnToBlackList_Click(object sender, RoutedEventArgs e)
        {
            User userToBlackList = currentVisitRequest.User;
            userToBlackList.UserBlackList = true;
            cont.SaveChanges();
            MessageBox.Show("Пользователь добавлен в ЧС");
        }

        private IEnumerable<dynamic> GetRequestData(int ReqID)
        {
            var InfoList = from VisitRequests in cont.VisitRequest
                           join Users in cont.User on VisitRequests.VisitUser equals Users.UserID
                           join Subdivisions in cont.Subdivision on VisitRequests.VisitSubdivision equals Subdivisions.SubdivisionID
                           join Employees in cont.Employee on VisitRequests.VisitEmployee equals Employees.EmployeeID
                           join VisitTypes in cont.VisitType on VisitRequests.VisitType equals VisitTypes.VisitTypeID
                           join VisitStatuses in cont.VisitStatus on VisitRequests.VisitStatus equals VisitStatuses.VisitStatusID
                           join VisitPurposes in cont.VisitPurpose on VisitRequests.VisitPurpose equals VisitPurposes.VisitPurposeID
                           where VisitRequests.VisitRequestID == ReqID
                           select new
                           {
                               VisitRequests.VisitRequestID,
                               Users.UserName,
                               Users.UserSurname,
                               Users.UserPatronymic,
                               Users.UserEmail,
                               Users.UserPhone,
                               Users.UserOrganization,
                               Users.UserDateofBirth,
                               Users.UserPassportSeries,
                               Users.UserPassportNumber,
                               Users.UserBlackList,
                               Users.UserPhoto,
                               Subdivisions.SubdivisionName,
                               Employees.EmployeeSurname,
                               Employees.EmployeeName,
                               Employees.EmployeePatronymic,
                               Employees.EmployeeCode,
                               VisitRequests.VisitDateStart,
                               VisitRequests.VisitDateEnd,
                               VisitRequests.VisitNote,
                               VisitTypes.VisitTypeName,
                               VisitPurposes.VisitPurposeName,
                               VisitStatuses.VisitStatusName
                           };
            return InfoList;
        }
    }
}
