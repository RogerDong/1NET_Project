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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        //Link to AdminPanelPage
        private void ToAdminPanel(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("AdminPanel/AdminPanelPage.xaml", UriKind.Relative));
            
        }
        //Check the database whether the table exists or not and choose to show it or not.
        private void Button_Loaded(object sender, RoutedEventArgs e)
        {
            Button table = (Button)sender;
            int tableNum = Convert.ToInt32(table.Name.Substring(1));
            var query = from i in Db.db.RestaurantMap
                        where i.Position == tableNum
                        select i;
            foreach (var item in query)
            {
                if (item.Existing == "YES")
                {
                    table.Visibility = Visibility.Visible;
                    table.Content = item.Status;

                }
                else
                {
                    table.Visibility = Visibility.Hidden;
                }
            }

        }
        //Check the database and choose to show the number of chairs or not
        private void Label_Loaded(object sender, RoutedEventArgs e)
        {
            Label chair = (Label)sender;
            int chairNum = Convert.ToInt32(chair.Name.Substring(1));
            var query = from i in Db.db.RestaurantMap
                        where i.Position == chairNum
                        select i;
            foreach (var item in query)
            {
                if (item.Existing == "YES")
                {
                    chair.Content = item.Chairs;


                }

            }
        }
        //Check the database and choose to show the shape of chair or not.
        private void Ellipse_Loaded(object sender, RoutedEventArgs e)
        {
            Ellipse chair = (Ellipse)sender;
            int chairNum = Convert.ToInt32(chair.Name.Substring(2));
            var query = from i in Db.db.RestaurantMap
                        where i.Position == chairNum
                        select i;
            foreach (var item in query)
            {
                if (item.Existing == "YES")
                {
                    chair.Visibility = Visibility.Visible;

                }
                else
                {
                    chair.Visibility = Visibility.Hidden;
                }
            }
        }
        //Check the table status and link to right page
        private void TableClick(object sender, RoutedEventArgs e)
        {

            Button table = (Button)sender;
            int tableNum = Convert.ToInt32(table.Name.Substring(1));
            var query = from i in Db.db.RestaurantMap
                        where i.Position == tableNum
                        select i;
            foreach (var item in query)
            {
                item.IfSelected = 1;
                //在数据库中Status为"EMPTY     "而非"EMPTY"
                //To check the status of the table and navigate to different page
                if (item.Status == "EMPTY     ")
                {
                    NavigationService.Navigate(new Uri("OrderDishes/OrderPage.xaml", UriKind.Relative));
                }
                else
                {
                    NavigationService.Navigate(new Uri("OrderDishes/PayBillPage.xaml", UriKind.Relative));
                }
            }

        }

    }
}
