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
    public partial class CategoriesTabPage : Page
    {
        public CategoriesTabPage()
        {
            InitializeComponent();
            CategoriesTable.ItemsSource = Emelyanenko_DB_PaymentEntities2.getInstance().Category.ToList();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddCategoryPage((sender as Button).DataContext as Category));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddCategoryPage(null));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Category> categoriesForDeletion = CategoriesTable.SelectedItems.Cast<Category>().ToList();
                if (MessageBox.Show($"Вы уверены, что хотите удалить {categoriesForDeletion.Count} записей?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Emelyanenko_DB_PaymentEntities2.getInstance().Category.RemoveRange(categoriesForDeletion);
                    Emelyanenko_DB_PaymentEntities2.getInstance().SaveChanges();
                    CategoriesTable.ItemsSource = Emelyanenko_DB_PaymentEntities2.getInstance().Category.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CategoriesTable.ItemsSource = Emelyanenko_DB_PaymentEntities2.getInstance().Category.ToList();
        }
    }
}
