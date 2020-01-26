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
    /// Interaction logic for invoice.xaml
    /// </summary>
    public partial class invoice : Window
    {
        Database1Entities db1 = new Database1Entities();
        employee emp = new employee();
        order ordr;
        int discount;
        bool flg;
        public invoice(Database1Entities db1, DataGrid order_dtgrid,employee emp,order ordr, string discount)
        {
            this.product_dtgrid = order_dtgrid;
            this.db1 = db1;
            this.emp = emp;
            this.discount = Int32.Parse(discount);
            this.ordr = ordr;
            this.flg = true;
            InitializeComponent();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            if (flg)
            {
                if (discount > 100)
                    discount = 100;
                int sp = 0;
                client_name_lbl.Content += ordr.name;
                worker_name_lbl.Content += emp.name;
                notes_lbl.Content += ordr.notes;
                product_dtgrid.ItemsSource = db1.products.ToList();
                product_dtgrid.Columns[0].Visibility = Visibility.Collapsed;
                product_dtgrid.Columns[5].Visibility = Visibility.Collapsed;
                List<products> lstp = db1.products.ToList();
                foreach (products p in lstp)
                {
                    sp += (p.price * p.quantity);
                }
                int f_price = sp - (sp * discount) / 100;
                payment_name_lbl.Content += f_price.ToString()+ "₪";
                final_price fp = new final_price { s_price = sp, discount = discount, f_price = f_price };
                ordr.final_price = db1.final_price.Add(fp);
                db1.order.Add(ordr);
                db1.SaveChanges();
                fprice_dtgrid.ItemsSource = db1.final_price.ToList();
                object row = new object();
                for (int i = 0; i < fprice_dtgrid.Items.Count-1; i++)
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

        private void cash_btn_Click(object sender, RoutedEventArgs e)
        {
            cash c = new cash();
            c.ShowDialog();
        }

        private void credit_btn_Click(object sender, RoutedEventArgs e)
        {
            credit cr = new credit();
            cr.ShowDialog();
        }
    }
}
