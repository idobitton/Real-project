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
    /// Interaction logic for Pizza.xaml
    /// </summary>
    public partial class Pizza : Window
    {
        Database1Entities db1 = new Database1Entities();
        DataGrid dataGrid = new DataGrid();
        private int quantity;
        private string name;
        private int price = 10;
        private string details;
        public Pizza(int quantity, Database1Entities db1, DataGrid dataGrid)
        {
            this.dataGrid = dataGrid;
            this.db1 = db1;
            this.quantity = quantity;
            InitializeComponent();
        }

        private void finish_Click(object sender, RoutedEventArgs e)
        {
            
            name = extra_cmbbx.Text;
            details = location_cmbbx.Text;
            if (name != "" && details != "")
            {
                if (name == "Nothing")
                    price = 0;
                products products = new products { name = name, price = price, quantity = quantity, details = details };
                db1.products.Add(products);
                dataGrid.ItemsSource = db1.products.ToList();
                db1.SaveChanges();
                this.Close();
            }
        }

        private void add_extra_Click(object sender, RoutedEventArgs e)
        {
            name = extra_cmbbx.Text;
            details = location_cmbbx.Text;
            if (name != "" && details != "")
            {
                if (name == "Nothing")
                    price = 0;
                products products = new products { name = name, price = price, quantity = quantity, details = details };
                db1.products.Add(products);
                dataGrid.ItemsSource = db1.products.ToList();
                db1.SaveChanges();
            }
        }

        private void extra_cmbbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
