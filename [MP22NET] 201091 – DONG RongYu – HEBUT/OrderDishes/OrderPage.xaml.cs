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
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        private int tableNumber;

        public int? waiterId;//用来确定用户选择的waiter的Id     Used to choose waiter id for user

        private int order;//order标记是否已经选择服务员，是否是Add Dishes   Used to mark that wheter user has choose a waiter or add dishes
        public OrderPage()
        {
            InitializeComponent();
        }

        //When loading page, show Datagrid
        private void LoadPage()
        {
            var queryWaiter = from i in Db.db.Waiters
                              select i;

            OrderWaitersData.ItemsSource = queryWaiter.ToList();

        }//第一次进入下订单界面，将所有的waiters显示出来让用户选择，之后进入Choose dish环节
            //The first time to choose an empty table, show the data of all waiters for user. And then to choose dish
        private void ShowYourWaiter()//选择waiter之后，将waiterId改为选择的Id，并显示被选择者的信息
        {
            var queryTable = from i in Db.db.RestaurantMap
                             where i.Position == tableNumber
                             select i;
            foreach (var item in queryTable)
            {
                waiterId = item.HandledBy;
            }
            var queryWaiter = from i in Db.db.Waiters
                              where i.Id == waiterId
                              select i;
            OrderWaitersData.ItemsSource = queryWaiter.ToList();
        }

        //After choosing a waiter, use this method to show dishes 
        private void ReloadPage()//选择waiter之后，调用ReloadPage()来显示Dishes
        {

            var queryDish = from j in Db.db.Dishes
                            select j;
            OrderDishesData.ItemsSource = queryDish.ToList();
            
            //如果order为2，表示是Add Dishes则order不变
            //否则，order为1，表示刚选择完waiter，将order改成1
            //If order is 2, represent the stage of adding dish
            //If order is 1, represent that the user just choosed waiter, and change order to 1
            if (order != 2)
            {
                order = 1;
            }
        }
        //Gt the information of the position of table
        public void GetTableNumber()//获取被选择的桌子的Position信息
        {
            var query = from i in Db.db.RestaurantMap
                        select i;
            foreach (var item in query)
            {
                //IfSelected = 1表示第一次进入预定环节
                //enter the stage of first time to the page, if IfSelected equal 1
                if (item.IfSelected == 1)
                {
                    tableNumber = item.Position;
                    item.IfSelected = 0;
                    order = 0;
                }
                //IfSelected = 2表示是从检查菜单处Add Dishes
                //If IfSelected equal 2, then choose the dish from different page.
                else if (item.IfSelected == 2)
                {
                    tableNumber = item.Position;
                    item.IfSelected = 1;
                    order = 2;
                }
            }
        }
        //If user click back button, remove all dishes
        public void CancelTheOrder()//用户点击Back返回，需要取消所点的Dishes
        {
            var queryOrder = from i in Db.db.Bill
                             where i.TableNumber == tableNumber
                             select i;
            foreach (var item in queryOrder)
            {
                Db.db.Bill.Remove(item);
            }
            Db.db.SaveChanges();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetTableNumber();
            if (order == 0)//表示第一次进入Order Dishes
            {                 //The first time to order dishes
                LoadPage();
            }
            else//表示是Add Dishes，则无需选择waiter所以调用ShowYourWaiter();
            {           //The second time to add dishes
                ShowYourWaiter();
                ReloadPage();
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            //order = 0 --> choose a waiter.
            if (order == 0)
            {
                Waiters waiter = (Waiters)OrderWaitersData.SelectedItem;
                if (waiter == null)
                {
                    MessageBox.Show("Please choose a waiter.");
                    return;
                }
                if (waiter != null)
                {

                    var queryTable = from i in Db.db.RestaurantMap
                                     where i.Position == tableNumber
                                     select i;
                    foreach (var item in queryTable)
                    {
                        item.HandledBy = waiter.Id;
                    }
                    Db.db.SaveChanges();
                    ShowYourWaiter();
                    ReloadPage();
                    return;

                }
            }
            else//order = 1/order = 2 --> choose a dish. 
            {
                Dishes dish = (Dishes)OrderDishesData.SelectedItem;
                if (dish == null)
                {
                    MessageBox.Show("Please choose some dish.");
                    return;
                }
                else
                {
                    var queryDish = from i in Db.db.Dishes
                                    where i.Id == dish.Id
                                    select i;
                    foreach (var item in queryDish)
                    {
                        Bill bill = new Bill();
                        bill.TableNumber = tableNumber;
                        bill.DishName = item.DishName;
                        bill.Money = item.Money;
                        Db.db.Bill.Add(bill);
                    }
                    Db.db.SaveChanges();
                    ReloadPage();
                }
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (order == 1 || order == 0)
            {
                CancelTheOrder();
                NavigationService.Navigate(new Uri("MainPage.xaml", UriKind.Relative));
            }
            else
            {
                NavigationService.Navigate(new Uri("OrderDishes/PayBillPage.xaml", UriKind.Relative));
            }

        }
        //After adding dish, change the state of table to eating. And check whether should make the HandledTable increase.
        private void ButtonOrder_Click(object sender, RoutedEventArgs e)//确认订单，将Table的状态改为EATING
        {
            
            //确认订单，如果不是Add Dishes，将所选waiter的HandledTable数值增加1
            if (order != 2)
            {
                var queryWaiter = from i in Db.db.Waiters
                                  where i.Id == waiterId
                                  select i;
                foreach (var item in queryWaiter)
                {
                    item.HandledTable += 1;
                }
                Db.db.SaveChanges();
            }

            var query = from i in Db.db.RestaurantMap
                        where i.Position == tableNumber
                        select i;

            foreach (var item in query)
            {
                item.Status = "EATING";
                item.IfSelected = 1;
                NavigationService.Navigate(new Uri("OrderDishes/PayBillPage.xaml", UriKind.Relative));

            }
        }

    }
}
