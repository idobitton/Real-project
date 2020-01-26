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
    /// Interaction logic for orderSupplier.xaml
    /// </summary>
    public partial class orderSupplier : Window
    {
        Database1Entities db1 = new Database1Entities();
        products prod;
        order ordr = new order();
        public orderSupplier(Database1Entities db1)
        {
            this.db1 = db1;
            InitializeComponent();
        }
        private int Calculating_price(string name)
        {
            if (name.Contains("Pasta"))
                return 30;
            else if (name.Contains("Quiche"))
                return 20;
            else if (name.Contains("rings"))
                return 10;
            else if (name.Contains("Personal"))
                return 15;
            else if (name.Contains("Family"))
                return 20;
            else if (name.Contains("Ziva"))
                return 15;
            else if (name.Contains("B_"))
                return 10;
            else if (name.Contains("-"))
                return 7;
            else if (name.Contains("+"))
                return 8;
            else if (name.Contains("S_"))
                return 5;
            else if (name.Contains("E_"))
                return 40;
            else if (name.Contains("sauce"))
                return 30;
            else if (name.Contains("Egg"))
              return 1;
            else if (name.Contains("Lemon"))
               return 5 ;
            else if (name.Contains("Dough"))
               return 45;
            else if (name.Contains("oil"))
                return 5;
            else if (name.Contains("paste"))
                return 20;
            return 0;
        }
        private void fd_aply_btn_Click(object sender, RoutedEventArgs e)
        {
            List<products> lstp = db1.products.ToList();
            order_dtgrid.ItemsSource = lstp;
            string nm = food_cmbbx.Text;
            int qn = qnty_cmbbx.SelectedIndex + 1;
            string qnty = (string)qnty_lbl.Content;
            if (qnty == "Quantity: (8 Units)")
                qn *= 8;
            else if (qnty == "Quantity: (crates of 30)")
                qn *= 30;
            else if (qnty == "Quantity: (4 Units)")
                qn *= 4;
            else
                qn *= 1;
            int prc = 0;
            if (nm != "")
            {
                prc += Calculating_price(nm);
                products p = new products { name = nm, quantity = qn, price = prc };
                db1.products.Add(p);
                db1.SaveChanges();
                order_dtgrid.ItemsSource = db1.products.ToList();
                order_dtgrid.Columns[4].Visibility = Visibility.Collapsed;
                order_dtgrid.Columns[5].Visibility = Visibility.Collapsed;

            }
            food_cmbbx.SelectedIndex = -1;
            qnty_cmbbx.SelectedIndex = 0;
            qnty_lbl.Content = "Quantity:";
        }

        private void food_cmbbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var fd = food_cmbbx.SelectedItem;
            if(fd == z || fd == m_q || fd == b_q || fd == p_g_b || fd == f_g_b || fd == o_r)
                qnty_lbl.Content = "Quantity: (8 Units)";
            else if(fd== egg)
                qnty_lbl.Content = "Quantity: (crates of 30)";
            else if (fd == oil)
                qnty_lbl.Content = "Quantity: (4 Units)";
            else
                qnty_lbl.Content = "Quantity: (Kilograms)";
        }

        private void beverage_cmbbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            qnty_lbl.Content = "Quantity: (crates of 24)";
        }

        private void bvg_aply_btn_Click(object sender, RoutedEventArgs e)
        {
            List<products> lstp = db1.products.ToList();
            order_dtgrid.ItemsSource = lstp;
            string nm = beverage_cmbbx.Text;
            if (nm != "")
            {
                int qn = (qnty_cmbbx.SelectedIndex + 1)*24;
                int prc = 0;
                prc += Calculating_price(nm);
                products p = new products { name = nm, quantity = qn, price = prc };
                db1.products.Add(p);
                db1.SaveChanges();
                order_dtgrid.ItemsSource = db1.products.ToList();
                order_dtgrid.Columns[4].Visibility = Visibility.Collapsed;
                order_dtgrid.Columns[5].Visibility = Visibility.Collapsed;
            }
            beverage_cmbbx.SelectedIndex = -1;
            qnty_cmbbx.SelectedIndex = 0;
            qnty_lbl.Content = "Quantity:";
        }

        private void order_aply_btn_Click(object sender, RoutedEventArgs e)
        {
            string notes = notes_txb.Text;
            ordr.notes = notes;
            ordr.name = supplier_name_txb.Text;
            ordr.identity = "Supplier";
            invoiceSupplier invc = new invoiceSupplier(db1, order_dtgrid, ordr);
            invc.ShowDialog();
        }

        private void order_dtgrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                prod = (products)order_dtgrid.SelectedItem;

            }
            catch
            {
                MessageBox.Show("you selected a non-existent product", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void dlt_btn_Click(object sender, RoutedEventArgs e)
        {
            if (prod != null)
            {
                if (prod.name != null)
                {
                    db1.products.Remove(prod);
                    db1.SaveChanges();
                    order_dtgrid.ItemsSource = db1.products.ToList();
                }
            }
        }
    }
}
