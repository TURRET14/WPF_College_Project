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
    public partial class PaymentsPage : Page
    {
        List<Payment> payments;
        public PaymentsPage()
        {
            InitializeComponent();
            try
            {
                payments = Emelyanenko_DB_PaymentEntities2.getInstance().Payment.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            PaymentsView.ItemsSource = payments;
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
            if (PaymentsView != null)
            {
                List<Payment> paymentsFiltered = payments;
                
                if (Name_Input.Text.Length > 0)
                {
                    paymentsFiltered = paymentsFiltered.Where(payment => payment.Name.ToLower().Contains(Name_Input.Text.ToLower())).ToList();
                }

                if (SortComboBox.SelectedItem as string == "Название, По убыванию")
                {
                    paymentsFiltered.Sort((payment1, payment2) => payment1.Name.CompareTo(payment2.Name));
                    paymentsFiltered.Reverse();
                }
                else if (SortComboBox.SelectedItem as string == "Название, По возрастанию")
                {
                    paymentsFiltered.Sort((payment1, payment2) => payment1.Name.CompareTo(payment2.Name));
                }

                PaymentsView.ItemsSource = null;
                PaymentsView.ItemsSource = paymentsFiltered;
            }
        }
    }
}
