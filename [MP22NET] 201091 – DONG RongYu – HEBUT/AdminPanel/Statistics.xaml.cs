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

namespace _MP22NET__201091___DONG_RongYu___HEBUT.AdminPanel
{
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : Page
    {
        public Statistics()
        {
            InitializeComponent();
            ShowWaiterStatistics();
        }
        //Update the Datagrid and show
        private void ShowWaiterStatistics()
        {
            var query = from i in Db.db.Waiters
                        select i;
            WaiterAllData.ItemsSource = query.ToList();
        }
        //Back to AdminPanelPage
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminPanelPage());
        }
    }
}
