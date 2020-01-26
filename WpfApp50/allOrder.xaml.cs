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
using System.Windows.Shapes;

namespace WpfApp50
{
    /// <summary>
    /// Interaction logic for allWindow.xaml
    /// </summary>
    public partial class allOrder : Window
    {
        Database1Entities db1 = new Database1Entities();
        int sum;
        public allOrder(Database1Entities db1)
        {
            this.db1 = db1;
            this.sum = 0;
            InitializeComponent();
            ordr_dtgrid.ItemsSource = db1.order.ToList();
            price_dtgrid.ItemsSource = db1.final_price.ToList();
            expense_dtgrid.ItemsSource = db1.expense.ToList();
            date_dtgrid.ItemsSource = db1.date.ToList();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            if (ordr_dtgrid.Columns.Count > 0)
            {
                ordr_dtgrid.Columns[3].Visibility = Visibility.Collapsed;
                ordr_dtgrid.Columns[5].Visibility = Visibility.Collapsed;
            }
            if (price_dtgrid.Columns.Count > 0)
            {
                price_dtgrid.Columns[3].Visibility = Visibility.Collapsed;
                price_dtgrid.Columns[5].Visibility = Visibility.Collapsed;
                price_dtgrid.Columns[6].Visibility = Visibility.Collapsed;
            }
            if (date_dtgrid.Columns.Count > 0)
            {
                date_dtgrid.Columns[4].Visibility = Visibility.Collapsed;
            }
            if (expense_dtgrid.Columns.Count > 0)
            {
                expense_dtgrid.Columns[4].Visibility = Visibility.Collapsed;
                expense_dtgrid.Columns[5].Visibility = Visibility.Collapsed;
                expense_dtgrid.Columns[7].Visibility = Visibility.Collapsed;
                expense_dtgrid.Columns[8].Visibility = Visibility.Collapsed;
            }
            List<order> lst_o = new List<order>();
            lst_o = db1.order.ToList();
            List<final_price> lst_fp = new List<final_price>();
            lst_fp = db1.final_price.ToList();
            foreach (order o in lst_o)
            {
                if(o.identity == "Supplier")
                {
                    foreach(final_price fp in lst_fp)
                    {
                        if(o.Id == fp.Id)
                        {
                            sum -= fp.f_price;
                        }
                    }
                }
                else
                {
                    foreach (final_price fp in lst_fp)
                    {
                        if (o.Id == fp.Id)
                        {
                            sum += fp.f_price;
                        }
                    }
                }
            }
            List<expense> lst_exp = new List<expense>();
            lst_exp = db1.expense.ToList();
            foreach (expense exp in lst_exp)
            {
                sum -= exp.price;
            }
            ac_bc_lbl.Content += sum.ToString() + "₪";
        }
    }
}
