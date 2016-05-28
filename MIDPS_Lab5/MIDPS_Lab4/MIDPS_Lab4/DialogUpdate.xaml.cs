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

namespace MIDPS_Lab4
{
    /// <summary>
    /// Interaction logic for DialogUpdate.xaml
    /// </summary>
    public partial class DialogUpdate : Window
    {
        public ModalControllerUpdate controller;
        private bool dirtyBit;
        public DialogUpdate()
        {
            InitializeComponent();
            controller = new ModalControllerUpdate();
            dirtyBit = false;
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            //controller.OnNotification(Notification.UpdateRowOk, this, textBox.Text);
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!dirtyBit)
            {
                textBox.Clear();
                dirtyBit = true;
            }
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBox.Text.Trim().Length == 0)
            {
                textBox.Text = "Name";
                dirtyBit = false;
            }
        }
    }
}
