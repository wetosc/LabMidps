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
using System.Data;
using System.Diagnostics;
using MaterialDesignThemes.Wpf;

namespace MIDPS_Lab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Controller myController { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            myController = new MainController(this);
            myController.OnNotification(Notification.PageChange,this,MiddleEarth.Ring);
        }

        public void hideList(bool hide)
        {
            if (hide) { dataGrid2.Visibility = Visibility.Hidden; label2.Visibility = Visibility.Hidden; }
            else { dataGrid2.Visibility = Visibility.Visible; label2.Visibility = Visibility.Visible; }
        }

        public void setData1(DataSet data)
        {
            dataGrid.ItemsSource = data.Tables[0].DefaultView;
            label.Content = myController.model.table1Title();
        }
        public void setData2(DataSet data)
        {
            if (data.Tables.Count > 1)
            {
                data.Tables[0].Merge(data.Tables[1]);
            }
            dataGrid2.ItemsSource = data.Tables[0].DefaultView;
            label2.Content = myController.model.table2Title();
        }

        private void Ring_Page(object sender, RoutedEventArgs e)
        {
            myController.OnNotification(Notification.PageChange, this, MiddleEarth.Ring);
        }
        private void Wizard_Page(object sender, RoutedEventArgs e)
        {
            myController.OnNotification(Notification.PageChange, this, MiddleEarth.Wizard);
        }
        private void Elf_Page(object sender, RoutedEventArgs e)
        {
            myController.OnNotification(Notification.PageChange, this, MiddleEarth.Elf);
        }
        private void Orc_Page(object sender, RoutedEventArgs e)
        {
            myController.OnNotification(Notification.PageChange, this, MiddleEarth.Orc);
        }
        private void Hobbit_Page(object sender, RoutedEventArgs e)
        {
            myController.OnNotification(Notification.PageChange, this, MiddleEarth.Hobbit);
        }

        private void selectRow1(object sender, MouseButtonEventArgs e)
        {
            DataRowView row = dataGrid.SelectedItem as DataRowView;
            if (row != null)
            {
                int id = row.Row.Field<int>("id");
                myController.OnNotification(Notification.RowSelected, this, id);
            }
            
        }

        private void newItem(object sender, RoutedEventArgs e)
        {
        }

        private void Sample2_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("SAMPLE 2: Closing dialog with parameter: " + (eventArgs.Parameter ?? ""));
        }
    }
}
