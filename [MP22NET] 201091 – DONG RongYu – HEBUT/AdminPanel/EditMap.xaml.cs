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
    /// Interaction logic for EditMap.xaml
    /// </summary>
    public partial class EditMap : Page
    {
        public EditMap()
        {
            InitializeComponent();
            ShowMap();
        }



        //To update Datagrid
        private void ShowMap()
        {
            var query = from i in Db.db.RestaurantMap
                        select i;
            MapData.ItemsSource = query.ToList();
        }

       
        //Navigate to AdminPanelPage
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminPanelPage());
        }
        //Edit the restaurant map. Choose a position To change exist or not and number of chairs. Meanwhile, check the input of the user
        private void EditRestaurantMap(object sender, RoutedEventArgs e)
        {
            int getId = 0;
            int chairNumber = 0;
            try
            {
                getId = Convert.ToInt32( Position.Text);
                chairNumber = Convert.ToInt32( ChairNumber.Text );
            }
            catch
            {
                EditError.Content = "Your input has something wrong. Try again!";
                return;
            }
            if (chairNumber < 0 || chairNumber > 12)
            {
                EditError.Content = "The number of chairs should be from 1 to 12.";
                return;
            }
            if (Existing.Text != "YES" && Existing.Text != "NO")
            {
                EditError.Content = "The Existing should be YES or NO";
                return;
            }
            else if(Existing.Text == "NO" && chairNumber != 0)
            {
                EditError.Content = "The number of chair shoube be 0 due to no table.";
                return;
            }
            
            
            var query = from i in Db.db.RestaurantMap
                        where i.Position == getId
                        select i;
            
            foreach (var item in query)
            {
                if(item.Status == "EATING")
                {
                    EditError.Content = "You cannot edit EATING table";
                    return;
                }
                item.Existing = Existing.Text;
                item.Chairs = chairNumber;
                
                
            }
            Db.db.SaveChanges();
            Existing.Text = "";
            ChairNumber.Text = "";
            EditError.Content = "";
            Position.Text = "";
            ShowMap();


        }
    }

    
}
