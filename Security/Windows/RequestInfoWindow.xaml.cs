using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
using AppConnect.DatabaseModel;
using static AppConnect.Connection;

namespace Security.Windows
{
    /// <summary>
    /// Interaction logic for RequestInfoWindow.xaml
    /// </summary>
    public partial class RequestInfoWindow : Window
    {
        static VisitTimeSpans currentRequestTimeSpans;
        public RequestInfoWindow(int selectedRequest)
        {
            InitializeComponent();
            currentRequestTimeSpans = cont.VisitTimeSpans.Find(selectedRequest);
            var currentRequestData = GetRequestData(selectedRequest).ToList()[0];
            DataContext = currentRequestData;
            runDateStart.Text = currentRequestData.VisitDateStart.ToString().Substring(0, 10);
            runDateEnd.Text = currentRequestData.VisitDateEnd.ToString().Substring(0, 10);
            tbEmp.Text = $"{currentRequestData.EmployeeSurname} {currentRequestData.EmployeeName} {currentRequestData.EmployeePatronymic}";
            if (currentRequestTimeSpans.CheckPointIncome == null)
                btnOpenTourniquet.IsEnabled = true;
            if (currentRequestTimeSpans.CheckpointOutcome == null)
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (currentRequestTimeSpans.SubdivisionOutcome != null)
            {
                DateTime dtOutcome = Convert.ToDateTime($"{dpDate.SelectedDate.ToString().Substring(0, 10)} {tbxHours.Text}:{tbxMinutes.Text}");
                currentRequestTimeSpans.CheckpointOutcome = dtOutcome;
                cont.SaveChanges();
                MessageBox.Show
                    (
                        "Выход отмечен",
                        "Информация",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );
            }
            else
            {
                MessageBox.Show
                    (
                        "Нельзя отметить выход из проходной, пока не отмечен выход из подразделения",
                        "Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
            }

        }

        private void btnOpenTourniquet_Click(object sender, RoutedEventArgs e)
        {
            SystemSounds.Hand.Play();
            btnOpenTourniquet.IsEnabled = false;
            currentRequestTimeSpans.CheckPointIncome = DateTime.Now;
            cont.SaveChanges();
        }

    }
}
