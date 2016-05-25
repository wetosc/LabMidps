using System;
using System.Collections.Generic;
//using System.Linq;
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

namespace MIDPS_Lab4
{
    /// <summary>
    /// Interaction logic for DialogAdd.xaml
    /// </summary>
    public partial class DialogAdd : Window
    {

        public ModalController controller { get; set; }
        private Dictionary<string, TextBox> fields;
        Dictionary<string, string> currentConfig;
        public DialogAdd(Model model)
        {
            InitializeComponent();
            controller = new ModalController();
            controller.model = model;
            currentConfig = model.addNewConfig().viewDescription;

            fields = new Dictionary<string, TextBox>();
            int i = 0;
            foreach (string key in currentConfig.Keys)
            {
                TextBox txt = new TextBox(); txt.HorizontalAlignment = HorizontalAlignment.Stretch;
                txt.Text = key;

                RowDefinition row = new RowDefinition(); row.Height = new GridLength(1, GridUnitType.Star);
                grid.RowDefinitions.Add(row);

                Grid.SetRow(txt, i);
                grid.Children.Add(txt);
                fields.Add(key, txt);
                i++;
            }
            Grid.SetRow(buttonParent, i);
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {

        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach(string key in fields.Keys)
            {
                result.Add(key, fields[key].Text);
            }
            controller.OnNotification(Notification.AddNewOK, this, result);
        }


        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Clear();
        }
    }
}
