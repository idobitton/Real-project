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
    /// Interaction logic for newExpenses.xaml
    /// </summary>
    public partial class newExpenses : Window
    {
        Database1Entities db1 = new Database1Entities();
        bool flg;
        public newExpenses( Database1Entities db1)
        {
            this.db1 = db1;
            this.flg = true;
            InitializeComponent();
            emp_dtgrid.ItemsSource = db1.employee.ToList();
        }

        private void sbmt_btn_Click(object sender, RoutedEventArgs e)
        {
            string p_method = "";
            employee emp = new employee();
            if (name_txb.Text == "")
                msg_lsb.Items.Add("Failure! Enter the expense' name");
            else if (price_txb.Text == "")
                msg_lsb.Items.Add("Failure! Enter the expense' price");
            else if (year_txb.Text == "")
                msg_lsb.Items.Add("Failure! Enter the expense' year");
            else if (month_txb.Text == "")
                msg_lsb.Items.Add("Failure! Enter the expense' month");
            else if (day_txb.Text == "")
                msg_lsb.Items.Add("Failure! Enter the expense' day");

            else if (emp_dtgrid.SelectedItem == null)
                msg_lsb.Items.Add("Failure! Select your identity");
            else if (pay_mth_cmbbx.SelectedItem == null)
                msg_lsb.Items.Add("Failure! Select the payment method");
            else
            {
                if (pay_mth_cmbbx.SelectedItem == credit)
                    p_method = "credit";
                else
                    p_method = "cash";
                emp = (employee)emp_dtgrid.SelectedItem;
                date dt = new date { year = Int32.Parse(year_txb.Text), month = Int32.Parse(month_txb.Text), day = Int32.Parse(day_txb.Text)};
                expense expnse = new expense { date = dt, employee= emp, name = name_txb.Text, pay_method = p_method, price = Int32.Parse(price_txb.Text), employee_name = emp.name};
                db1.date.Add(dt);
                db1.expense.Add(expnse);
                db1.SaveChanges();
                expense_dtgrid.Visibility = Visibility.Visible;
                date_dtgrid.Visibility = Visibility.Visible;
                date_dtgrid.ItemsSource = db1.date.ToList();
                expense_dtgrid.ItemsSource = db1.expense.ToList();
                date_dtgrid.Columns[4].Visibility = Visibility.Collapsed;
                expense_dtgrid.Columns[4].Visibility = Visibility.Collapsed;
                expense_dtgrid.Columns[5].Visibility = Visibility.Collapsed;
                expense_dtgrid.Columns[7].Visibility = Visibility.Collapsed;
                expense_dtgrid.Columns[8].Visibility = Visibility.Collapsed;
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            if (flg)
            {
                date_dtgrid.Visibility = Visibility.Collapsed;
                expense_dtgrid.Visibility = Visibility.Collapsed;
                flg = false;
            }
        }
    }
}
