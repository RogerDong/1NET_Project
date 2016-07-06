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
    /// Interaction logic for PayBillPage.xaml
    /// </summary>
    public partial class PayBillPage : Page
    {
        public int tableNumber;

        public decimal money;//账单总费用   the money of one table

        public PayBillPage()
        {
            InitializeComponent();

        }
        //Show the name and money of dishes choosed 
        public void LoadPage()//显示所选择的Dishes的 Name & Money
        {
            var queryBill = from i in Db.db.Bill
                            where i.TableNumber == tableNumber
                            select i;
            PayBillData.ItemsSource = queryBill.ToList();
            money = 0m;
        }

        //Get the information of the table Position
        public void GetTableNumber()//获取被选择的桌子的Position信息
        {
            var query = from i in Db.db.RestaurantMap
                        select i;
            foreach (var item in query)
            {
                if (item.IfSelected == 1)
                {
                    tableNumber = item.Position;
                    item.IfSelected = 0;
                }
            }
        }
        //Calculate the money
        public void CalculateMoney()//计算Money的值
        {
            var queryMoney = from i in Db.db.Bill
                             where i.TableNumber == tableNumber
                             select i;
            foreach (var item in queryMoney)
            {
                money += item.Money;
            }
        }
        //Get some information when the page load
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetTableNumber();
            LoadPage();
            CalculateMoney();
        }
        //Navigate to MainPage
        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("MainPage.xaml", UriKind.Relative));
        }
        //Pay Money, remove dishes and so on
        private void ButtonPayBill_Click(object sender, RoutedEventArgs e)//按下Pay,调用该函数
        {
            //给Waiter发服务费   
            //Waiter earn the money
            var queryTable = from i in Db.db.RestaurantMap
                             where i.Position == tableNumber
                             select i;
            int waiterId = 0;
            foreach (var item in queryTable)
            {
                waiterId = (int)item.HandledBy;
            }
            var queryWaiter = from i in Db.db.Waiters
                              where i.Id == waiterId
                              select i;
            foreach (var item in queryWaiter)
            {
                item.EarnedMoney += Convert.ToDecimal(Convert.ToInt32((decimal)money * 100)) / 100;
            }

            //将table的状态改回EMPTY，以便于下一次使用，并将该桌子的菜单从数据库中删除，然后返回主页
            //Change the table to empty for the next time, remove this table's dishes.
            foreach (var item in queryTable)
            {
                item.Status = "EMPTY     ";
                item.HandledBy = null;
            }
            var queryDish = from i in Db.db.Bill
                            where i.TableNumber == tableNumber
                            select i;
            foreach (var item in queryDish)
            {

                Db.db.Bill.Remove(item);
            }
            Db.db.SaveChanges();
            NavigationService.Navigate(new Uri("MainPage.xaml", UriKind.Relative));
        }
        //Add dishes
        private void ButtonAddDishes_Click(object sender, RoutedEventArgs e)//增加Dish
        {
            var query = from i in Db.db.RestaurantMap
                        where i.Position == tableNumber
                        select i;
            foreach (var item in query)
            {
                item.IfSelected = 2;
            }
            NavigationService.Navigate(new Uri("OrderDishes/OrderPage.xaml", UriKind.Relative));
        }
        //Delete the dish of one table
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)//删除不要的Dish
        {
            Bill bill = (Bill)PayBillData.SelectedItem;
            if (bill == null)
            {
                MessageBox.Show("Please choose one dish.");
                return;
            }
            else
            {
                var queryDish = from i in Db.db.Bill
                                where i.Id == bill.Id
                                select i;
                foreach (var item in queryDish)
                {

                    Db.db.Bill.Remove(item);
                }
                Db.db.SaveChanges();
                LoadPage();

            }
        }

    }
}
