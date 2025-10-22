using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class AddPaymentPage : Page
    {
        Payment currentPayment;
        bool isNewPayment = false;
        public AddPaymentPage(Payment selectedPayment)
        {
            InitializeComponent();
            if (selectedPayment != null)
            {
                currentPayment = selectedPayment;
            }
            else
            {
                currentPayment = new Payment();
                isNewPayment = true;
            }
            DataContext = currentPayment;
            try
            {
                User_Input.ItemsSource = Emelyanenko_DB_PaymentEntities2.getInstance().User.ToList();
                Category_Input.ItemsSource = Emelyanenko_DB_PaymentEntities2.getInstance().Category.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (User_Input.SelectedItem == null)
            {
                errors.AppendLine("Выберите пользователя!");
            }
            if (Category_Input.SelectedItem == null)
            {
                errors.AppendLine("Выберите категорию!");
            }
            if (!Date_Input.SelectedDate.HasValue)
            {
                errors.AppendLine("Выберите дату!");
            }
            if (Name_Input.Text.Length == 0)
            {
                errors.AppendLine("Введите название!");
            }
            if (Num_Input.Text.Length == 0)
            {
                errors.AppendLine("Введите количество!");
            }
            else
            {
                try
                {
                    if (Convert.ToDecimal(Num_Input.Text) < 0)
                    {
                        errors.AppendLine("Количество не может быть отрицательным!");
                    }
                    else if (Convert.ToDecimal(Num_Input.Text) > 10000000)
                    {
                        errors.AppendLine("Количество слишком большое!");
                    }
                }
                catch (Exception ex)
                {
                    errors.AppendLine("Введите корректное количество!");
                }
            }
            if (Price_Input.Text.Length == 0)
            {
                errors.AppendLine("Введите цену!");
            }
            else
            {
                try
                {
                    if (Convert.ToDecimal(Price_Input.Text) < 0)
                    {
                        errors.AppendLine("Цена не может быть отрицательной!");
                    }
                    else if (Convert.ToDecimal(Price_Input.Text) > 10000000)
                    {
                        errors.AppendLine("Цена слишком большая!");
                    }
                }
                catch (Exception ex)
                {
                    errors.AppendLine("Введите корректную цену!");
                }
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (isNewPayment)
            {
                try
                {
                    Emelyanenko_DB_PaymentEntities2.getInstance().Payment.Add(currentPayment);
                    MessageBox.Show("Платеж добавлен!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            try
            {
                Emelyanenko_DB_PaymentEntities2.getInstance().SaveChanges();
                if (!isNewPayment)
                {
                    MessageBox.Show("Изменения сохранены!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void Input_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^\d*\.?\d*$");
            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true;
            }
        }
    }
}
