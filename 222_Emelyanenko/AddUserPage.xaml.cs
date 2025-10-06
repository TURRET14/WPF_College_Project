using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для AddUserPage.xaml
    /// </summary>
    public partial class AddUserPage : Page
    {
        User currentUser;
        bool isNewUser = false;
        public AddUserPage(User selectedUser)
        {
            InitializeComponent();
            if (selectedUser != null)
            {
                selectedUser.Password = "";
                currentUser = selectedUser;
            }
            else
            {
                currentUser = new User();
                isNewUser = true;
            }
            DataContext = currentUser;
        }

        public static string GetHash(string password)
        {
            SHA1 hash = SHA1.Create();
            return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (Login_Input.Text.Length == 0)
            {
                MessageBox.Show("Введите логин!");
                return;
            }
            else if (Password_Input.Text.Length == 0)
            {
                MessageBox.Show("Введите пароль!");
                return;
            }
            else if (FIO_Input.Text.Length == 0)
            {
                MessageBox.Show("Введите ФИО!");
                return;
            }
            else if (Role_Input.SelectedItem == null)
            {
                MessageBox.Show("Выберите роль!");
                return;
            }
            currentUser.Password = GetHash(Password_Input.Text);
            if (isNewUser)
            {
                Emelyanenko_DB_PaymentEntities2.getInstance().User.Add(currentUser);
                MessageBox.Show("Пользователь добавлен!");
            }
            else
            {
                MessageBox.Show("Изменения сохранены!");
            }
            Emelyanenko_DB_PaymentEntities2.getInstance().SaveChanges();
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}
