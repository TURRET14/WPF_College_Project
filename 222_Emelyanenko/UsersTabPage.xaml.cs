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
    /// Логика взаимодействия для UsersTabPage.xaml
    /// </summary>
    public partial class UsersTabPage : Page
    {
        public UsersTabPage()
        {
            InitializeComponent();
            UsersTable.ItemsSource = Emelyanenko_DB_PaymentEntities2.getInstance().User.ToList();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddUserPage((sender as Button).DataContext as User));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddUserPage(null));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            List<User> usersForDeletion = UsersTable.SelectedItems.Cast<User>().ToList();
            if (MessageBox.Show($"Вы уверены, что хотите удалить {usersForDeletion.Count} записей?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Emelyanenko_DB_PaymentEntities2.getInstance().User.RemoveRange(usersForDeletion);
                Emelyanenko_DB_PaymentEntities2.getInstance().SaveChanges();
                UsersTable.ItemsSource = Emelyanenko_DB_PaymentEntities2.getInstance().User.ToList();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UsersTable.ItemsSource = Emelyanenko_DB_PaymentEntities2.getInstance().User.ToList();
        }
    }
}
