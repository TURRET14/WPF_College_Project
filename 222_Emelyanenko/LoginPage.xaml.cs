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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private int failedAttempts = 0;
        private User currentUser;
        public LoginPage()
        {
            InitializeComponent();
        }

        public static string GetHash(string password)
        {
            SHA1 hash = SHA1.Create();
            return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
        }

        private void Login_Click(object sender, RoutedEventArgs e)
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
            else
            {
                string login = Login_Input.Text;
                string password = GetHash(Password_Input.Text);
                currentUser = Emelyanenko_DB_PaymentEntities1.getInstance().User.AsNoTracking().FirstOrDefault(user => user.Login == login && user.Password == password);
                if (currentUser == null)
                {
                    MessageBox.Show("Логин или пароль неверны!");
                    failedAttempts++;
                    if (failedAttempts >= 3)
                    {

                    }
                }
                else
                {
                    switch (currentUser.Role)
                    {
                        case "User":

                            break;
                        case "Admin":

                            break;
                    }
                }
            }

        }
    }
}
