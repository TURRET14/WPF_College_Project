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
            users = Emelyanenko_DB_PaymentEntities2.getInstance().User.ToList();
            UsersView.ItemsSource = users;
            Sort();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Emelyanenko_DB_PaymentEntities2.getInstance().SaveChanges();
        }

        private void FIO_Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            Sort();
        }

        private void Admin_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Sort();
        }

        private void Admin_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Sort();
        }

        private void ClearFilters_Button_Click(object sender, RoutedEventArgs e)
        {
            FIO_Input.Text = "";
            Admin_CheckBox.IsChecked = false;
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sort();
        }

        private void Sort()
        {
            if (UsersView != null)
            {
                List<User> usersFiltered = users;
                if (Admin_CheckBox.IsChecked.GetValueOrDefault())
                {
                    usersFiltered = usersFiltered.Where(user => user.Role == "Admin").ToList();
                }
                
                if (FIO_Input.Text.Length > 0)
                {
                    usersFiltered = usersFiltered.Where(user => user.FIO.ToLower().Contains(FIO_Input.Text.ToLower())).ToList();
                }

                if (SortComboBox.SelectedItem as string == "ФИО, По убыванию")
                {
                    usersFiltered.Sort((user1, user2) => user1.FIO.CompareTo(user2.FIO));
                    usersFiltered.Reverse();
                }
                else if (SortComboBox.SelectedItem as string == "ФИО, По возрастанию")
                {
                    usersFiltered.Sort((user1, user2) => user1.FIO.CompareTo(user2.FIO));
                }

                UsersView.ItemsSource = null;
                UsersView.ItemsSource = usersFiltered;
            }
        }
    }
}
