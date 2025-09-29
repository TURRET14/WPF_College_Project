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
    enum DictionaryType
    {
        Default = 0,
        MyDictionary = 1
    }
    public partial class MainWindow : Window
    {
        private DictionaryType currentDictionary = DictionaryType.Default;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { DateTimeNow.Text = DateTime.Now.ToString(); };
            timer.Start();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите закрыть окно?", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void Style_Button_Click(object sender, RoutedEventArgs e)
        {
            Uri dictionaryUri;
            if (currentDictionary == DictionaryType.Default)
            {
                dictionaryUri = new Uri("MyDictionary.xaml", UriKind.Relative);
                currentDictionary = DictionaryType.MyDictionary;
            }
            else
            {
                dictionaryUri = new Uri("Dictionary.xaml", UriKind.Relative);
                currentDictionary = DictionaryType.Default;
            }
            ResourceDictionary resourceDict = Application.LoadComponent(dictionaryUri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.NavigationService.CanGoBack)
            {
                MainFrame.NavigationService.GoBack();
            }
        }
    }
}
