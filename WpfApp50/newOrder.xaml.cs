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
    /// Interaction logic for newOrder.xaml
    /// </summary>
    public partial class newOrder : Window
    {
        Database1Entities db1 = new Database1Entities();
        employee emp = new employee();
        order ordr;
        products prod = new products();
        public newOrder(Database1Entities db1, employee emp,order ordr)
        {
            this.db1 = db1;
            this.ordr = ordr;
            this.emp = emp;
            InitializeComponent();

        }
        private int Calculating_price(string name)
        {
            if (name.Contains("Penne"))
                return 37;
            else if (name.Contains("Quiche"))
                return 35;
            else if (name.Contains("Salad"))
                return 40;
            else if (name.Contains("rings"))
                return 20;
            else if (name.Contains("Personal"))
                return 22;
            else if (name.Contains("Family"))
                return 32;
            else if (name.Contains("Ziva"))
                return 22;
            else if (name.Contains("B_"))
                return 13;
            else if (name.Contains("-"))
                return 9;
            else if (name.Contains("+"))
                return 10;
            else if (name.Contains("Small"))
                return 55;
            else if (name.Contains("Large"))
                return 65 ;
            else if (name.Contains("Extra"))
                return 75;
            return 0;
        }
        private void fd_aply_btn_Click(object sender, RoutedEventArgs e)
        {
            List<products> lstp = db1.products.ToList();
            order_dtgrid.ItemsSource = lstp;
            string nm = food_cmbbx.Text;
            int qn = qnty_cmbbx.SelectedIndex + 1;
            int prc = 0;
            if (nm != "")
            {
                    if (nm == "Pizza")
                    {
                        if (dgh_type_cmbbx.Text != "" && size_cmbbx.Text != "")
                        {
                            string size = size_cmbbx.Text;
                            size += " " + nm;
                            prc += Calculating_price(size);
                            products products = new products { name = size, price = prc, quantity = qn, details = dgh_type_cmbbx.Text };
                            Pizza pz = new Pizza(qn, db1, order_dtgrid);
                            db1.products.Add(products);
                            db1.SaveChanges();
                            pz.ShowDialog();
                            order_dtgrid.ItemsSource = db1.products.ToList();
                            order_dtgrid.Columns[5].Visibility = Visibility.Collapsed;

                    }
                }
                    else
                    {
                        prc += Calculating_price(nm);
                        products p = new products { name = nm, quantity = qn, price = prc };
                        db1.products.Add(p);
                        db1.SaveChanges();
                        order_dtgrid.ItemsSource = db1.products.ToList();
                        order_dtgrid.Columns[5].Visibility = Visibility.Collapsed;

                }
            }
            dgh_type_cmbbx.Visibility = Visibility.Hidden;
            size_cmbbx.Visibility = Visibility.Hidden;
            size_lbl.Visibility = Visibility.Hidden;
            dgh_type_lbl.Visibility = Visibility.Hidden;
            food_cmbbx.SelectedIndex = -1;
            qnty_cmbbx.SelectedIndex = 0;

        }

        private void food_cmbbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(food_cmbbx.SelectedItem== pizza)
            {
                dgh_type_cmbbx.Visibility = Visibility.Visible;
                size_cmbbx.Visibility = Visibility.Visible;
                size_lbl.Visibility = Visibility.Visible;
                dgh_type_lbl.Visibility = Visibility.Visible;
            }
            else
            {
                dgh_type_cmbbx.Visibility = Visibility.Hidden;
                size_cmbbx.Visibility = Visibility.Hidden;
                size_lbl.Visibility = Visibility.Hidden;
                dgh_type_lbl.Visibility = Visibility.Hidden;

            }
        }

        private void bvg_aply_btn_Click(object sender, RoutedEventArgs e)
        {
            
            List<products> lstp = db1.products.ToList();
            order_dtgrid.ItemsSource = lstp;
            string nm = beverage_cmbbx.Text;
            if (nm != "")
            {
                int qn = qnty_cmbbx.SelectedIndex + 1;
                int prc = 0;
                prc += Calculating_price(nm);
                products p = new products { name = nm, quantity = qn, price = prc };
                db1.products.Add(p);
                db1.SaveChanges();
                order_dtgrid.ItemsSource = db1.products.ToList();
                order_dtgrid.Columns[5].Visibility = Visibility.Collapsed;
            }
            beverage_cmbbx.SelectedIndex = -1;
            qnty_cmbbx.SelectedIndex = 0;
        }

        private void order_aply_btn_Click(object sender, RoutedEventArgs e)
        {
            if (discount_txb.Text != "")
            {
                string discount = discount_txb.Text;
                string notes = notes_txb.Text;
                ordr.notes = notes;
                invoice invc = new invoice(db1, order_dtgrid, emp,ordr, discount);
                invc.ShowDialog();
                
            }
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
