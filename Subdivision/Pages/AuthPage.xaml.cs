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
using Subdivision.Pages;
using static AppConnect.Connection;


namespace Subdivision.Pages
{
    /// <summary>
    /// Interaction logic for AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }
        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            bool isCodeValid = CheckCode(tbxCode.Text);
            if (isCodeValid)
                NavigationService.Navigate(new RequestsPage());
            else
                MessageBox.Show
                (
                    "Код введён неверно",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
        }

        private bool CheckCode(string Code)
        {
            try
            {
                int IntCode = Convert.ToInt32(Code);
                if (cont.Employee.Where(emp => emp.EmployeeCode == IntCode).ToList().Count() > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }

        }
    }
}
