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
        private bool isCaptchaVisible = false;
        public LoginPage()
        {
            InitializeComponent();
        }

        public static string GetHash(string password)
        {
            SHA1 hash = SHA1.Create();
            return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
        }

        private void ToggleCaptcha()
        {
            if (isCaptchaVisible)
            {
                CaptchaLabel.Visibility = Visibility.Collapsed;
                CaptchaText.Visibility = Visibility.Collapsed;
                CaptchaGrid.Visibility = Visibility.Collapsed;
                
                failedAttempts = 0;
                CaptchaText.Text = "";
                CaptchaInput.Text = "";
            }
            else
            {
                CaptchaLabel.Visibility = Visibility.Visible;
                CaptchaText.Visibility = Visibility.Visible;
                CaptchaGrid.Visibility = Visibility.Visible;
                
                CaptchaText.Text = "";
                CaptchaInput.Text = "";
                CaptchaText.Text = GenerateCaptcha();
            }

            isCaptchaVisible = !isCaptchaVisible;
        }

        private string GenerateCaptcha()
        {
            StringBuilder captcha = new StringBuilder();
            Random random = new Random();
            for (int count = 0; count < 10; count++)
            {
                int type = random.Next(1, 4);
                int generatedNumber = 0;
                switch (type)
                {
                    case 1:
                        generatedNumber = random.Next(48, 58);
                        captcha.Append((char)generatedNumber);
                        break;
                    case 2:
                        generatedNumber = random.Next(65, 91);
                        captcha.Append((char)generatedNumber);
                        break;
                    case 3:
                        generatedNumber = random.Next(97, 123);
                        captcha.Append((char)generatedNumber);
                        break;
                }
            }
            return captcha.ToString();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (isCaptchaVisible)
            {
                MessageBox.Show("Введите и проверьте Captcha!");
                return;
            }
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
                    currentUser = Emelyanenko_DB_PaymentEntities2.getInstance().User.AsNoTracking().FirstOrDefault(user => user.Login == login && user.Password == password);
                    if (currentUser == null)
                    {
                        MessageBox.Show("Логин или пароль неверны!");
                        failedAttempts++;
                        if (failedAttempts >= 3)
                        {
                            ToggleCaptcha();
                        }
                    }
                    else
                    {
                        switch (currentUser.Role)
                        {
                            case "User":

                                break;
                            case "Admin":
                            NavigationService.Navigate(new UsersPage());
                                break;
                        }
                    }
                }

        }

        private void CaptchaText_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Copy || e.Command == ApplicationCommands.Cut || e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        private void CaptchaValidate_Click(object sender, RoutedEventArgs e)
        {
            if (CaptchaInput.Text == CaptchaText.Text)
            {
                ToggleCaptcha();
            }
            else
            {
                MessageBox.Show("Неверная Captcha. Попробуйте снова!");
                isCaptchaVisible = false;
                ToggleCaptcha();
            }
        }
    }
}
