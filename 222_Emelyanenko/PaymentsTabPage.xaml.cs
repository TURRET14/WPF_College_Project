using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для CategoriesTabPage.xaml
    /// </summary>
    public partial class PaymentsTabPage : Page
    {
        public PaymentsTabPage()
        {
            InitializeComponent();
            try
            {
                PaymentsTable.ItemsSource = Emelyanenko_DB_PaymentEntities2.getInstance().Payment.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPaymentPage((sender as Button).DataContext as Payment));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPaymentPage(null));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Payment> usersForDeletion = PaymentsTable.SelectedItems.Cast<Payment>().ToList();
                if (MessageBox.Show($"Вы уверены, что хотите удалить {usersForDeletion.Count} записей?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Emelyanenko_DB_PaymentEntities2.getInstance().Payment.RemoveRange(usersForDeletion);
                    Emelyanenko_DB_PaymentEntities2.getInstance().SaveChanges();
                    PaymentsTable.ItemsSource = Emelyanenko_DB_PaymentEntities2.getInstance().Payment.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PaymentsTable.ItemsSource = Emelyanenko_DB_PaymentEntities2.getInstance().Payment.ToList();
        }
    }
}
