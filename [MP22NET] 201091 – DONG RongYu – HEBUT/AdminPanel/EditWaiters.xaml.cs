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
    /// Interaction logic for EditWaiters.xaml
    /// </summary>
    public partial class EditWaiters : Page
    {
        public EditWaiters()
        {
            InitializeComponent();
            ShowWaiter();
        }
       
        //Update the datagrid and show
        private void ShowWaiter()
        {
            var query = from i in Db.db.Waiters
                        select i;
            WaiterData.ItemsSource = query.ToList();
        }
        //Back to AdminPanelPage
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminPanelPage());
        }
        //Add a waiter to the Waiter list include sex, name and age. And check the input.
        private void AddWaiter(object sender, RoutedEventArgs e)
        {
            Waiters waiter = new Waiters();
            waiter.EarnedMoney = 0.00m;
            waiter.HandledTable = 0;
            try
            {
                if (SexA.Text == "M" || SexA.Text == "F")
                {
                    waiter.Sex = SexA.Text;
                }
                else
                {
                    AddError.Content = "Your input has something wrong. Try again!";
                    return;
                }
                waiter.FirstName = FirstNameA.Text;
                waiter.LastName = LastNameA.Text;
                waiter.Age = Convert.ToInt32(AgeA.Text);
                FirstNameA.Text = "";
                LastNameA.Text = "";
                AgeA.Text = "";
                SexA.Text = "";
                AddError.Content = "";

                Db.db.Waiters.Add(waiter);
                Db.db.SaveChanges();
            }
            catch
            {
                AddError.Content = "Your input has something wrong. Try again!";
            }
            ShowWaiter();
        }

        //Delete one item based on ID
        private void Delete(object sender, RoutedEventArgs e)
        {
            int getId = 0;
            try
            {
                 getId = Convert.ToInt32(IdD.Text);
            }
            catch
            {
                DeleteError.Content = "Your input has something wrong. Try again!";
                return;
            }
            
            var query = from i in Db.db.Waiters
                        where i.Id == getId
                        select i;
            foreach (var item in query)
            {
                Db.db.Waiters.Remove(item);
            }
            Db.db.SaveChanges();
            ShowWaiter();
            DeleteError.Content = "";
            IdD.Text = "";

        }
        //Update the information of one waiter based on ID. And check the input.
        private void WaiterUpdate(object sender, RoutedEventArgs e)
        {
            int getId = 0;
            try
            {
                getId = Convert.ToInt32(IdU.Text);
                var query = from i in Db.db.Waiters
                            where i.Id == getId
                            select i;
                foreach (var item in query)
                {
                    item.FirstName = FirstNameU.Text;
                    item.LastName = LastNameU.Text;
                    item.Age = Convert.ToInt32(AgeU.Text);
                    if (SexU.Text == "M" || SexU.Text == "F")
                    {
                        item.Sex = SexU.Text;
                    }
                    else
                    {
                        UpdateError.Content = "Your input has something wrong. Try again!";
                        return;
                    }
                }
                Db.db.SaveChanges();
                ShowWaiter();
                FirstNameU.Text = "";
                LastNameU.Text = "";
                AgeU.Text = "";
                SexU.Text = "";
                UpdateError.Content = "";
                IdU.Text = "";
            }
            catch
            {
                UpdateError.Content = "Your input has something wrong. Try again!";
            }
            
        }

       
    }

    
}
