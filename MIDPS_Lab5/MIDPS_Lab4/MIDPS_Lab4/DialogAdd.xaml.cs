using System;
using System.Collections.Generic;
using System.Linq;
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
using Microsoft.Win32;
using System.IO;

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
        private Button openDialog;
        public DialogAdd(Model model)
        {
            InitializeComponent();
            controller = new ModalController();
            controller.model = model;
            currentConfig = model.addNewConfig().viewDescription;

            fields = new Dictionary<string, TextBox>();
            int i = 0;
            dataGrid.Visibility = Visibility.Hidden;
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
                            dataGrid.Margin = new Thickness(0, 10, 0, 0);
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
                            dataGrid.Margin = new Thickness(0, 10, 0, 0);
                            Grid.SetRow(dataGrid, i);
                            i++;
                            dataGrid.Visibility = Visibility.Visible;
                            dataGrid.ItemsSource = Singleton.Instance.getData(model.typeFromString(MiddleEarth.Hobbit)).Tables[0].DefaultView;
                            dataGrid.SelectionMode = DataGridSelectionMode.Single;
                        }
                        break;
                    case "image":
                        {
                            openDialog = new Button(); openDialog.Click += openDialog_Clik; openDialog.Content = "Select Image"; openDialog.Margin = new Thickness(10, 10, 10, 10);

                            RowDefinition row = new RowDefinition(); row.Height = new GridLength(1, GridUnitType.Star);
                            grid.RowDefinitions.Add(row);

                            Grid.SetRow(openDialog, i);
                            grid.Children.Add(openDialog);
                            i++;
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
            Dictionary<string, object> result = new Dictionary<string, object>();
            foreach (string key in fields.Keys)
            {
                result.Add(key, fields[key].Text);
            }
            if (dataGrid.Visibility == Visibility.Visible)
            {
                if (dataGrid.SelectionMode == DataGridSelectionMode.Extended)
                {
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
                    string ids = "";
                    if (dataGrid.SelectedIndex != -1) { ids = (dataGrid.SelectedItem as DataRowView).Row.Field<int>("id").ToString(); }
                    result.Add(myKey, ids);
                }
            }
            if (openDialog != null)
            {
                if (!(openDialog.Content as string).Equals("Select Image"))
                {
                    string filePath = (string)openDialog.Content;
                    FileInfo info = new FileInfo(filePath);
                    byte[] data = new byte[info.Length];
                    FileStream fs = new FileStream(filePath, FileMode.Open,
                              FileAccess.Read, FileShare.Read);
                    fs.Read(data, 0, (int)info.Length);
                    fs.Close();

                    var myKey = currentConfig.FirstOrDefault(x => x.Value == "image").Key;
                    result.Add(myKey, data);
                }
            }
            controller.OnNotification(Notification.AddNewOK, this, result);
        }



        private void openDialog_Clik(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog(); dialog.Title = "Select a profile pic";
            dialog.Filter = "JPEG Images|*.jpg|GIF Images|*.gif|BITMAPS|*.bmp|PNG Images|*.png";
            if (dialog.ShowDialog() == true)
            {
                openDialog.Content = dialog.FileName;
            }
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
            if (temp.Text.Trim().Length == 0)
            {
                temp.Text = key;
                int pos = fields.Values.ToList().IndexOf(temp);
                dirtList[pos] = false;
            }
        }
    }
}
