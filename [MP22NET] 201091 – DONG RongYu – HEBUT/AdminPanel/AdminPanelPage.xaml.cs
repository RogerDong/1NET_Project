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

namespace _MP22NET__201091___DONG_RongYu___HEBUT
{
    /// <summary>
    /// Interaction logic for AdminPanelPage.xaml
    /// </summary>
    public partial class AdminPanelPage : Page
    {
        public AdminPanelPage()
        {
            InitializeComponent();
        }
        //Back to MainPage
        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("../MainPage.xaml", UriKind.Relative));
        }
        //Navigate to EditDishes Page
        private void EditDishes(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("AdminPanel/EditDishes.xaml", UriKind.Relative));

        }
        //Navigate to EditWaiters page
        private void EditDWaiters(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("AdminPanel/EditWaiters.xaml", UriKind.Relative));
            
        }

        
        //Navigate to Statistics page.
        private void ShowStatistics(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("AdminPanel/Statistics.xaml",UriKind.Relative));
        }
        //Navigate to EditMap page.
        private void EditMap(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("AdminPanel/EditMap.xaml", UriKind.Relative));
        }
    }
}
