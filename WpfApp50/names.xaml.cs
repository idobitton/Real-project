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
    /// Interaction logic for names.xaml
    /// </summary>
    public partial class names : Window
    {
        Database1Entities db1 = new Database1Entities();
        employee emp = new employee();

        public names(DataGrid emp_dtgrid, Database1Entities db1)
        {
            this.emp_dtgrid = emp_dtgrid;
            this.db1 = db1;
            db1.SaveChanges();
            this.emp_dtgrid.ItemsSource = db1.employee.ToList();

            InitializeComponent();
        }

        private void cntue_Click(object sender, RoutedEventArgs e)
        {
            if (client_name_txb.Text != "" && emp != null)
            {
                List<products> lst_p = new List<products>();
                lst_p = db1.products.ToList();
                foreach (products p in lst_p)
                {
                    db1.products.Remove(p);
                    db1.SaveChanges();
                }
                string client_name = client_name_txb.Text;
                order ordr = new order { name = client_name , identity = "Client"};
                newOrder newOrder = new newOrder(db1, emp, ordr);
                this.Close();
                newOrder.ShowDialog();
                lst_p = db1.products.ToList();
            }
        }
        private void load_table_Click(object sender, RoutedEventArgs e)
        {
            emp_dtgrid.ItemsSource = db1.employee.ToList();
        }

        private void emp_dtgrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                try
                {
                     emp = (employee)emp_dtgrid.SelectedItem;

                }
            catch
            {
                MessageBox.Show("you selected a non-existent employee","Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
