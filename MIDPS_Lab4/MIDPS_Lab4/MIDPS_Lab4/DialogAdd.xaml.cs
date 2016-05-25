using System;
using System.Collections.Generic;
using System.Linq;
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
        private List<bool> dirtList;
        private Dictionary<string, string> currentConfig;
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
                txt.Text = key; txt.GotFocus += textBox_GotFocus; txt.LostFocus += Txt_LostFocus;

                RowDefinition row = new RowDefinition(); row.Height = new GridLength(1, GridUnitType.Star);
                grid.RowDefinitions.Add(row);

                Grid.SetRow(txt, i);
                grid.Children.Add(txt);
                fields.Add(key, txt);
                i++;
            }
            Grid.SetRow(buttonParent, i);
            dirtList = Enumerable.Repeat(false, i).ToList(); ;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (string key in fields.Keys)
            {
                result.Add(key, fields[key].Text);
            }
            controller.OnNotification(Notification.AddNewOK, this, result);
        }


        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox temp = sender as TextBox;
            int pos = fields.Values.ToList().IndexOf(temp);
            if (!dirtList[pos])
            {
                temp.Clear();
                dirtList[pos] = true;
            }
        }

        private void Txt_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox temp = sender as TextBox;
            string key = fields.FirstOrDefault(x => x.Value == temp).Key;
            bool valid = true;
            if (temp.Text.Trim().Length == 0)
            {
                temp.Text = key;
                int pos = fields.Values.ToList().IndexOf(temp);
                dirtList[pos] = false;
                valid = false;
            }
            //else
            //{
            //    switch (key)
            //    {
            //        case "list":
            //            {
            //                uint t;
            //                foreach (string id in temp.Text.Split(','))
            //                {
            //                    if (!uint.TryParse(id.Trim(), out t))
            //                    {
            //                        valid = false;
            //                        break;
            //                    }
            //                }
            //            }
            //            break;
            //        case "number":
            //            {
            //                uint t;
            //                valid = uint.TryParse(temp.Text, out t);
            //            }
            //            break;
            //        case "float":
            //            {
            //                float t;
            //                valid = float.TryParse(temp.Text, out t);
            //            }
            //            break;
            //    }
            //}

            //if (valid)
            //{
            //    temp.BorderBrush = Brushes.White;
            //}
            //else
            //{
            //    temp.BorderBrush = Brushes.Red;
            //}
            //btnDialogOk.IsEnabled = valid;
        }
    }
}
