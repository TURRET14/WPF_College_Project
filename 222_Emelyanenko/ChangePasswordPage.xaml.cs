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
    /// Логика взаимодействия для ChangePasswordPage.xaml
    /// </summary>
    public partial class ChangePasswordPage : Page
    {
        private User currentUser;
        public ChangePasswordPage()
        {
            InitializeComponent();
        }

        public static string GetHash(string password)
        {
            SHA1 hash = SHA1.Create();
            return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            if (Login_Input.Text.Length == 0)
            {
                MessageBox.Show("Введите логин!");
                return;
            }
            else if (OldPassword_Input.Text.Length == 0 || NewPassword_Input.Text.Length == 0)
            {
                MessageBox.Show("Введите старый и новый пароли!");
                return;
            }
            else if (OldPassword_Input.Text.Length == NewPassword_Input.Text.Length)
            {
                MessageBox.Show("Старый и новый пароль должны различаться!");
            }
            else
            {
                string login = Login_Input.Text;
                string oldPassword = GetHash(OldPassword_Input.Text);
                string newPassword = GetHash(NewPassword_Input.Text);
                currentUser = Emelyanenko_DB_PaymentEntities1.getInstance().User.FirstOrDefault(user => user.Login == login && user.Password == oldPassword);
                if (currentUser == null)
                {
                    MessageBox.Show("Логин или пароль неверны!");
                }
                else
                {
                    currentUser.Password = newPassword;
                    Emelyanenko_DB_PaymentEntities1.getInstance().SaveChanges();
                    MessageBox.Show("Пароль успешно изменен!");
                    NavigationService.Navigate(new AuthPage());
                }
            }
        }
    }
}
