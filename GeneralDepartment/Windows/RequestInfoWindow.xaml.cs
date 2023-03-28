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

namespace GeneralDepartment.Windows
{
    /// <summary>
    /// Interaction logic for RequestInfoWindow.xaml
    /// </summary>
    public partial class RequestInfoWindow : Window
    {

        static VisitRequest CurrentRequest;
        public RequestInfoWindow(int ReqID) //TODO: Validate DateTime
        {
            InitializeComponent();
            var RequestData = GetRequestData(ReqID).ToList();
            DataContext = RequestData[0];
            CurrentRequest = cont.VisitRequest.Where(req => req.VisitRequestID == ReqID).FirstOrDefault();
            cbxStatus.ItemsSource = cont.VisitStatus.ToList();
            cbxStatus.DataContext = CurrentRequest;
            runDateStart.Text =  CurrentRequest.VisitDateStart.ToString().Substring(0, 10);
            runDateEnd.Text =  CurrentRequest.VisitDateEnd.ToString().Substring(0, 10);
            tbEmp.Text = $"{RequestData[0].EmployeeSurname} {RequestData[0].EmployeeName} {RequestData[0].EmployeePatronymic}";
            BlackListCheck(RequestData[0].UserBlackList);
            var ReqVisitDateTime = cont.VisitTimeSpans.Where(item => item.VisitID == CurrentRequest.VisitRequestID).FirstOrDefault();
            if(ReqVisitDateTime != null)
            {
                dpDate.SelectedDate = ReqVisitDateTime.VisitDateTime;
                tbxHours.Text = ReqVisitDateTime.VisitDateTime.ToString().Substring(11, 2).Replace(":", "");
                tbxMinutes.Text = ReqVisitDateTime.VisitDateTime.ToString().Substring(ReqVisitDateTime.VisitDateTime.ToString().IndexOf(":")+1, 2);
            }
            if (CurrentRequest.VisitRejectionPurpose != null)
                cbxRejectionPurpose.SelectedIndex = (int)CurrentRequest.VisitRejectionPurpose - 1;
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

        private void BlackListCheck(bool UserBlackList)
        {
            if (UserBlackList)
            {
                MessageBox.Show
                (
                    "Заявитель находится в черном списке, заявка отклонена автоматически", 
                    "Внимание", 
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                cbxStatus.SelectedValue = 2;
                cbxStatus.IsEnabled = false;
                cbxRejectionPurpose.SelectedIndex = 0;
                cbxRejectionPurpose.IsEnabled = false;
                cont.SaveChanges();
            }
            else
            {
                MessageBox.Show
                (
                    "Заявителя нет в черном списке",
                    "Информация",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(dpDate.Text != "" && tbxHours.Text!="" && tbxMinutes.Text!="")
                SaveVisitDateTime();
            if (cbxRejectionPurpose.SelectedIndex == 1)
                UserIncorrectDataCountIncrement();

            cont.SaveChanges();     
            MessageBox.Show
            (
                "Изменения сохранены",
                "Информация",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
            Close();
        }

        private void SaveVisitDateTime()
        {
            if ((int)cbxStatus.SelectedValue == 1)
            {
                var CurrentVisitSpans = cont.VisitTimeSpans.Where(item => item.VisitID == CurrentRequest.VisitRequestID).SingleOrDefault();
                DateTime visdt = Convert.ToDateTime($"{dpDate.SelectedDate.ToString().Substring(0, 10)} {tbxHours.Text}:{tbxMinutes.Text}");
                if (CurrentVisitSpans != null)
                    CurrentVisitSpans.VisitDateTime = visdt;
                else
                {
                    VisitTimeSpans newVisitSpans = new VisitTimeSpans
                    {
                        VisitID = CurrentRequest.VisitRequestID,
                        VisitDateTime = visdt
                    };
                    cont.VisitTimeSpans.Add(newVisitSpans);
                }
            }
        }

        private void cbxStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if((int)cbxStatus.SelectedValue == 2)
            {
                dpDate.IsEnabled = false; dpDate.Text = "";
                tbxHours.IsEnabled = false; tbxHours.Text = "";
                tbxMinutes.IsEnabled = false; tbxMinutes.Text = "";
                cbxRejectionPurpose.IsEnabled = true;
            }
            else if((int)cbxStatus.SelectedValue == 3)
            {
                dpDate.IsEnabled = false; dpDate.Text = "";
                tbxHours.IsEnabled = false; tbxHours.Text = "";
                tbxMinutes.IsEnabled = false; tbxMinutes.Text = "";
                cbxRejectionPurpose.IsEnabled = false;
            }
            else
            {
                dpDate.IsEnabled = true;
                tbxHours.IsEnabled = true;
                tbxMinutes.IsEnabled = true;
                cbxRejectionPurpose.IsEnabled = false;
            }
        }

        private void cbxRejectionPurpose_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbxRejectionPurpose.SelectedIndex == 0) 
            {
                CurrentRequest.VisitRejectionPurpose = 1;
            }
            else
            {
                CurrentRequest.VisitRejectionPurpose = 2;
            }
        }

        private void UserIncorrectDataCountIncrement()
        {
            User CurrentUser = cont.User.Where(user => user.UserID == CurrentRequest.VisitUser).SingleOrDefault();
            CurrentUser.UserIncorrectDataCount++;
        }
    }
}
