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

namespace MIDPS_Lab5
{
    public interface AlertViewDelegate
    {
        void buttonClicked(AlertView view, bool isFirstButton);
    }

    public partial class AlertView : Window
    {
        public AlertViewDelegate delegat { get; set; }
        public AlertView()
        {
            InitializeComponent();
        }

        public AlertView(AlertViewDelegate delegat)
        {
            this.delegat = delegat;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            delegat.buttonClicked(this, true);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            delegat.buttonClicked(this, false);
        }
    }
}
