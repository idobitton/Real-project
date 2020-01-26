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

namespace WpfApp50
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Database1Entities db1 = new Database1Entities();
        public MainWindow()
        {
            InitializeComponent();
            emp_dtgrid.ItemsSource = db1.employee.ToList();
        }

        private void add_worker_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AddWorker addWorker = new AddWorker(emp_dtgrid,db1);
            addWorker.ShowDialog();
            emp_dtgrid.ItemsSource = db1.employee.ToList();
            this.Show();
        }

        private void delete_worker_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            DeleteWorker deleteWorker = new DeleteWorker(db1);
            deleteWorker.ShowDialog();
            emp_dtgrid.ItemsSource = db1.employee.ToList();
            this.Show();
        }
        private void new_ordr_btn_Click(object sender, RoutedEventArgs e)
        {
                names names = new names(emp_dtgrid, db1);
                names.ShowDialog();
        }
        private void ext_btn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        private void expenses_btn_Click(object sender, RoutedEventArgs e)
        {
            newExpenses ne = new newExpenses(db1);
            ne.ShowDialog();
        }

        private void ordr_supplier_btn_Click(object sender, RoutedEventArgs e)
        {
            List<products> lst_p = new List<products>();
            lst_p = db1.products.ToList();
            foreach (products p in lst_p)
            {
                db1.products.Remove(p);
                db1.SaveChanges();
            }
            orderSupplier op = new orderSupplier(db1);
            op.ShowDialog();
        }

        private void shifts_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void all_ordr_btn_Click(object sender, RoutedEventArgs e)
        {
            allOrder ao = new allOrder(db1);
            ao.ShowDialog();
        }
    }
}
