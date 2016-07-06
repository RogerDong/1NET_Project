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
    /// Interaction logic for EditDishes.xaml
    /// </summary>
    public partial class EditDishes : Page
    {
    
        public EditDishes()
        {
            InitializeComponent();
            ShowDish();
            
        }
        //Add an item to the menu include the name and the money.
        private void AddDish(object sender, RoutedEventArgs e)
        {
            Dishes dish = new Dishes();
            try
            {
                dish.DishName = DishNameA.Text;
                dish.Money = Convert.ToDecimal( Convert.ToInt32((decimal.Parse(MoneyA.Text)) * 100) )/100;
                Db.db.Dishes.Add(dish);
                Db.db.SaveChanges();
                AddError.Content = "";
                DishNameA.Text = "";
                MoneyA.Text = "";
            }
            catch
            {
                AddError.Content = "Your input has something wrong. Try again";
            }
            
            ShowDish();
        }
        //Update the datagrid and show
        private void ShowDish()
        {
            var query = from i in Db.db.Dishes
                        select i;
            DishData.ItemsSource = query.ToList();
        }
        //Delete one item based on ID
        private void DeleteDish(object sender, RoutedEventArgs e)
        {
            try
            {
                int getId = Convert.ToInt32(DishIdD.Text);
                var query = from i in Db.db.Dishes
                            where i.Id == getId
                            select i;
                foreach (var item in query)
                {
                    Db.db.Dishes.Remove(item);
                }
                Db.db.SaveChanges();
                DeleteError.Content = "";
                DishIdD.Text = "";
            }
            catch
            {
                DeleteError.Content = "Your input has something wrong. Try again";
            }
            
            ShowDish();
            

        }
        //Update an item based on ID
        private void DishUpdate(object sender, RoutedEventArgs e)
        {
            try
            {

                int getId = Convert.ToInt32(DishIdU.Text);
                var query = from i in Db.db.Dishes
                            where i.Id == getId
                            select i;

                foreach (var item in query)
                {
                    item.DishName = DishNameU.Text;
                    item.Money = Convert.ToDecimal(Convert.ToInt32((decimal.Parse( MoneyU.Text)) * 100)) / 100; ;
                }
                Db.db.SaveChanges();

                UpdateError.Content = "";
                DishIdU.Text = "";
                DishNameU.Text = "";
                MoneyU.Text = "";
            }
            catch
            {

                UpdateError.Content = "Your input has something wrong. Try again";
            }
            ShowDish();

        }
        //Back to AdminPanelPage
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminPanelPage());
        }

        
        
        

        
    }
}
