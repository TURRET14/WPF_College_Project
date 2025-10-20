using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
    public partial class CategoriesPage : Page
    {
        List<Category> categories;
        public CategoriesPage()
        {
            InitializeComponent();
            categories = Emelyanenko_DB_PaymentEntities2.getInstance().Category.ToList();
            CategoriesView.ItemsSource = categories;
            Sort();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Emelyanenko_DB_PaymentEntities2.getInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Name_Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            Sort();
        }

        private void ClearFilters_Button_Click(object sender, RoutedEventArgs e)
        {
            Name_Input.Text = "";
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sort();
        }

        private void Sort()
        {
            if (CategoriesView != null)
            {
                List<Category> categoriesFiltered = categories;
                
                if (Name_Input.Text.Length > 0)
                {
                    categoriesFiltered = categoriesFiltered.Where(category => category.Name.ToLower().Contains(Name_Input.Text.ToLower())).ToList();
                }

                if (SortComboBox.SelectedItem as string == "Название, По убыванию")
                {
                    categoriesFiltered.Sort((category1, category2) => category1.Name.CompareTo(category2.Name));
                    categoriesFiltered.Reverse();
                }
                else if (SortComboBox.SelectedItem as string == "Название, По возрастанию")
                {
                    categoriesFiltered.Sort((category1, category2) => category1.Name.CompareTo(category2.Name));
                }

                CategoriesView.ItemsSource = null;
                CategoriesView.ItemsSource = categoriesFiltered;
            }
        }
    }
}
