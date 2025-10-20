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
    public partial class AddCategoryPage : Page
    {
        Category currentCategory;
        bool isNewCategory = false;
        public AddCategoryPage(Category selectedCategory)
        {
            InitializeComponent();
            if (selectedCategory != null)
            {
                currentCategory = selectedCategory;
            }
            else
            {
                currentCategory = new Category();
                isNewCategory = true;
            }
            DataContext = currentCategory;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (Name_Input.Text.Length == 0)
            {
                errors.AppendLine("Введите название!");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (isNewCategory)
            {
                try
                {
                    Emelyanenko_DB_PaymentEntities2.getInstance().Category.Add(currentCategory);
                    MessageBox.Show("Категория добавлена!");
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
                if (!isNewCategory)
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
    }
}
