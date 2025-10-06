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

        public bool CheckPassword(string password)
        {
            if (password.Length < 6)
            {
                MessageBox.Show("Пароль должен состоять из 6 символов или более!");
                return false;
            }
            bool containsNumber = false;
            for (int count = 0; count < password.Length; count++)
            {
                if (int.TryParse(password[count].ToString(), out int convertedNumber))
                {
                    containsNumber = true;
                }
                else
                {
                    if (!((password[count] >= 'A' && password[count] <= 'Z') || (password[count] >= 'a' && password[count] <= 'z')))
                    {
                        MessageBox.Show("Пароль должен состоять только из латинских символов и цифр!");
                        return false;
                    }
                }
            }
            if (containsNumber)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Пароль должен содержать как минимум одну цифру!");
                return false;
            }
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            if (Login_Input.Text.Length == 0)
            {
                MessageBox.Show("Введите логин!");
                return;
            }
            else if (OldPassword_Input.Password.Length == 0 || NewPassword_Input.Password.Length == 0)
            {
                MessageBox.Show("Введите старый и новый пароли!");
                return;
            }
            else if (OldPassword_Input.Password == NewPassword_Input.Password)
            {
                MessageBox.Show("Старый и новый пароль должны различаться!");
            }
            else
            {
                if (!CheckPassword(NewPassword_Input.Password))
                {
                    return;
                }
                string login = Login_Input.Text;
                string oldPassword = GetHash(OldPassword_Input.Password);
                string newPassword = GetHash(NewPassword_Input.Password);
                currentUser = Emelyanenko_DB_PaymentEntities2.getInstance().User.FirstOrDefault(user => user.Login == login && user.Password == oldPassword);
                if (currentUser == null)
                {
                    MessageBox.Show("Логин или пароль неверны!");
                }
                else
                {
                    currentUser.Password = newPassword;
                    Emelyanenko_DB_PaymentEntities2.getInstance().SaveChanges();
                    MessageBox.Show("Пароль успешно изменен!");
                    NavigationService.Navigate(new AuthPage());
                }
            }
        }
    }
}
