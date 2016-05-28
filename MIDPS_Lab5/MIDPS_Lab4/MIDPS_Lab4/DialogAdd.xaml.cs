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
using DLLSpecial;
using System.Data;
using System.Collections;

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
                switch (currentConfig[key])
                {
                    case "text":
                    case "number":
                    case "float":
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
                        break;
                    case "list.multiple":
                        {
                            Grid.SetRow(dataGrid, i);
                            i++;
                            dataGrid.Visibility = Visibility.Visible;
                            dataGrid.SelectionMode = DataGridSelectionMode.Extended;
                            if (model.currentPage == MiddleEarth.Wizard)
                            {
                                dataGrid.ItemsSource = Singleton.Instance.getData(model.typeFromString(MiddleEarth.Ring)).Tables[0].DefaultView;
                            }
                            else if (model.currentPage == MiddleEarth.Ring)
                            {
                                dataGrid.ItemsSource = Singleton.Instance.getData(model.typeFromString(MiddleEarth.Wizard)).Tables[0].DefaultView;
                            }
                        }
                        break;
                    case "list.single":
                        {
                            Grid.SetRow(dataGrid, i);
                            i++;
                            dataGrid.Visibility = Visibility.Visible;
                            dataGrid.ItemsSource = Singleton.Instance.getData(model.typeFromString(MiddleEarth.Hobbit)).Tables[0].DefaultView;
                            dataGrid.SelectionMode = DataGridSelectionMode.Single;
                        }
                        break;
                }
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
            if (dataGrid.Visibility == Visibility.Visible)
            {
                if (dataGrid.SelectionMode == DataGridSelectionMode.Extended) {
                    var myKey = currentConfig.FirstOrDefault(x => x.Value == "list.multiple").Key;
                    List<DataRowView> rows = (List<DataRowView>)((IList)dataGrid.SelectedItems).Cast<DataRowView>().ToList<DataRowView>();
                    string ids = "";
                    foreach (DataRowView row in rows)
                    {
                        ids += row.Row.Field<int>("id") + ",";
                    }
                    result.Add(myKey, ids);
                }
                else if (dataGrid.SelectionMode == DataGridSelectionMode.Single)
                {
                    var myKey = currentConfig.FirstOrDefault(x => x.Value == "list.single").Key;
                    string ids = (dataGrid.SelectedItem as DataRowView).Row.Field<int>("id").ToString();
                    result.Add(myKey, ids);
                }
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
            //bool valid = true;
            if (temp.Text.Trim().Length == 0)
            {
                temp.Text = key;
                int pos = fields.Values.ToList().IndexOf(temp);
                dirtList[pos] = false;
                //valid = false;
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
