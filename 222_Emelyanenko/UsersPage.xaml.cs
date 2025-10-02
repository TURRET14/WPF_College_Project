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
        List<User> users;
        public UsersPage()
        {
            InitializeComponent();
            Emelyanenko_DB_PaymentEntities2.getInstance().User.Load();
            users = Emelyanenko_DB_PaymentEntities2.getInstance().User.Local.ToList();
            users.Sort((user1, user2) => user1.FIO.CompareTo(user2.FIO));
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
                UsersView.ItemsSource = ((List<User>)(UsersView.ItemsSource)).Where(user => user.Role == "Admin");
            }

            if (SortComboBox.Text == "ФИО, По убыванию")
            {
                ((List<User>)UsersView.ItemsSource).Reverse();
            }

        }

        private void Admin_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UsersView.ItemsSource = users.Where(user => user.Role == "Admin");
            if (FIO_Input.Text.Length > 0)
            {
                UsersView.ItemsSource = ((List<User>)(UsersView.ItemsSource)).Where(user => user.FIO.Contains(FIO_Input.Text));
            }

            if (SortComboBox.Text == "ФИО, По убыванию")
            {
                ((List<User>)UsersView.ItemsSource).Reverse();
            }
        }

        private void Admin_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UsersView.ItemsSource = users;
            if (FIO_Input.Text.Length > 0)
            {
                UsersView.ItemsSource = ((List<User>)(UsersView.ItemsSource)).Where(user => user.FIO.Contains(FIO_Input.Text));
            }

            if (SortComboBox.Text == "ФИО, По убыванию")
            {
                ((List<User>)UsersView.ItemsSource).Reverse();
            }
        }

        private void ClearFilters_Button_Click(object sender, RoutedEventArgs e)
        {
            FIO_Input.Text = "";
            Admin_CheckBox.IsChecked = false;
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((List<User>)UsersView.ItemsSource).Reverse();
        }
    }
}
