using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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

namespace _222_Emelyanenko
{
    /// <summary>
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        ObservableCollection<User> users;
        public UsersPage()
        {
            InitializeComponent();
            Emelyanenko_DB_PaymentEntities2.getInstance().User.Load();
            users = Emelyanenko_DB_PaymentEntities2.getInstance().User.Local;
            UsersView.ItemsSource = users;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Emelyanenko_DB_PaymentEntities2.getInstance().SaveChanges();
        }

        private void FIO_Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FIO_Input.Text.Length == 0)
            {
                UsersView.ItemsSource = users;
            }
            else
            {
                UsersView.ItemsSource = users.Where(user => user.FIO.Contains(FIO_Input.Text));
            }

            if (Admin_CheckBox.IsChecked.Value)
            {
                UsersView.ItemsSource = ((IEnumerable<User>)(UsersView.ItemsSource)).Where(user => user.Role == "Admin");
            }
        }

        private void Admin_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UsersView.ItemsSource = users.Where(user => user.Role == "Admin");
            if (FIO_Input.Text.Length > 0)
            {
                UsersView.ItemsSource = ((IEnumerable<User>)(UsersView.ItemsSource)).Where(user => user.FIO.Contains(FIO_Input.Text));
            }
        }

        private void Admin_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UsersView.ItemsSource = users;
            if (FIO_Input.Text.Length > 0)
            {
                UsersView.ItemsSource = ((IEnumerable<User>)(UsersView.ItemsSource)).Where(user => user.FIO.Contains(FIO_Input.Text));
            }
        }

        private void ClearFilters_Button_Click(object sender, RoutedEventArgs e)
        {
            FIO_Input.Text = "";
            Admin_CheckBox.IsChecked = false;
        }
    }
}
