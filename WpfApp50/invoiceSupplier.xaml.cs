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
    /// Interaction logic for invoiceSupplier.xaml
    /// </summary>
    public partial class invoiceSupplier : Window
    {
        Database1Entities db1 = new Database1Entities();
        order ordr;
        int discount;
        bool flg;
        public invoiceSupplier(Database1Entities db1, DataGrid order_dtgrid, order ordr)
        {
            this.product_dtgrid = order_dtgrid;
            this.db1 = db1;
            this.ordr = ordr;
            this.flg = true;
            this.discount = 0;
            InitializeComponent();
        }
        private int CalculatingDigits(int num)
        {
            if (num > 10)
                return 1 + CalculatingDigits(num / 10);
            return 1;
        }
        private int CalculatingDiscount(int price, int quantity)
        {
            int digits = CalculatingDigits(price);
            int r_num=1;
            int num_return;
            for (int i = 0; i < digits; i++)
            {
                r_num *= 10;
            }
            num_return = price - ((price * (quantity - 1) / r_num)*10);
            return num_return;
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            if (flg)
            {
                int sum_qnty = 0;
                int sp = 0;
                supplier_name_lbl.Content += ordr.name;
                notes_lbl.Content += ordr.notes;
                product_dtgrid.ItemsSource = db1.products.ToList();
                product_dtgrid.Columns[0].Visibility = Visibility.Collapsed;
                product_dtgrid.Columns[4].Visibility = Visibility.Collapsed;
                product_dtgrid.Columns[5].Visibility = Visibility.Collapsed;
                List<products> lstp = db1.products.ToList();
                foreach (products p in lstp)
                {
                    sp += (p.price * p.quantity);
                    sum_qnty += p.quantity;
                }
                int f_price = CalculatingDiscount(sp, sum_qnty);
                int discount = 100 -((100 *f_price) /sp) ;
                if(discount>20)
                {
                    discount = 20;
                    f_price = sp - (sp * discount)/100;
                }
                payment_name_lbl.Content += f_price.ToString() + "₪";
                final_price fp = new final_price { s_price = sp, discount = discount, f_price = f_price };
                ordr.final_price = db1.final_price.Add(fp);
                db1.order.Add(ordr);
                db1.SaveChanges();
                fprice_dtgrid.ItemsSource = db1.final_price.ToList();
                object row = new object();
                for (int i = 0; i < fprice_dtgrid.Items.Count - 1; i++)
                {
                    row = fprice_dtgrid.ItemContainerGenerator.Items[i];
                }
                List<object> lstrow = new List<object>();
                lstrow.Add(row);
                fprice_dtgrid1.ItemsSource = lstrow;
                fprice_dtgrid1.Columns[0].Visibility = Visibility.Collapsed;
                fprice_dtgrid1.Columns[3].Visibility = Visibility.Collapsed;
                fprice_dtgrid1.Columns[5].Visibility = Visibility.Collapsed;
                fprice_dtgrid1.Columns[6].Visibility = Visibility.Collapsed;
                fprice_dtgrid.Visibility = Visibility.Collapsed;
                flg = false;
            }
        }

        private void payment_btn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
