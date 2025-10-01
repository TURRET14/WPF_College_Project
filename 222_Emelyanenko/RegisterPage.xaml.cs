using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
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

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
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
            else
            {
                if (Password_Input.Text != PasswordRepeat_Input.Text)
                {
                    MessageBox.Show("Пароли не совпадают!");
                    return;
                }

                if (!CheckPassword(Password_Input.Text))
                {
                    return;
                }

                string login = Login_Input.Text;
                string password = GetHash(Password_Input.Text);
                string fio = FIO_Input.Text;
                string role = Role_Input.Text;

                if (Emelyanenko_DB_PaymentEntities2.getInstance().User.FirstOrDefault(user => user.Login == login) != null)
                {
                    MessageBox.Show("Логин уже занят!");
                }
                else
                {
                    Emelyanenko_DB_PaymentEntities2.getInstance().User.Add(new User() { Login = login, Password = password, FIO = fio, Role = role });
                    Emelyanenko_DB_PaymentEntities2.getInstance().SaveChanges();
                    MessageBox.Show("Вы успешно зарегистрировались!");
                    NavigationService.Navigate(new AuthPage());
                }
            }
        }
    }
}
