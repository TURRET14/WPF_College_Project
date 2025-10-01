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
                string login = Login_Input.Text;
                string password = GetHash(Password_Input.Text);
                string fio = FIO_Input.Text;
                string role = Role_Input.Text;

                if (Emelyanenko_DB_PaymentEntities1.getInstance().User.FirstOrDefault(user => user.Login == login) != null)
                {
                    MessageBox.Show("Логин уже занят!");
                }
                else
                {
                    Emelyanenko_DB_PaymentEntities1.getInstance().User.Add(new User() { Login = login, Password = password, FIO = fio, Role = role });
                    Emelyanenko_DB_PaymentEntities1.getInstance().SaveChanges();
                    MessageBox.Show("Вы успешно зарегистрировались!");
                    NavigationService.Navigate(new AuthPage());
                }
            }
        }
    }
}
