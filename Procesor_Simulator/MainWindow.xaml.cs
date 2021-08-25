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

namespace Procesor_Simulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Random random = new Random();
        public static string GetRandomHexNumber(int digits)
        {
            byte[] buffer = new byte[digits / 2];
            random.NextBytes(buffer);
            string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            if (digits % 2 == 0)
                return result;
            return result + random.Next(16).ToString("X");
        }
        public string moveTo { get; set; }
        public string moveFrom { get; set; }

        private Dictionary<string, TextBox> textBoxBound = new Dictionary<string, TextBox>();
        public MainWindow()
        {
            InitializeComponent();
            textBoxBound.Add("AX", AX);
            textBoxBound.Add("BX", BX);
            textBoxBound.Add("CX", CX);
            textBoxBound.Add("DX", DX);
            ResetTextButtons();
        }

        private void Button_MoveClick(object sender, RoutedEventArgs e)
        {
            var buttonName = (sender as Button).Content.ToString();
            SetMovePairs(buttonName);
            TextBox moveFromBox;
            TextBox moveToBox;

            if (textBoxBound.TryGetValue(moveTo, out moveToBox) && textBoxBound.TryGetValue(moveFrom, out moveFromBox))
                moveToBox.Text = moveFromBox.Text;
        }
        private void SetMovePairs(string buttonName)
        {
            moveTo = buttonName.Split(',')[1].Trim();
            moveFrom = buttonName.Split(',')[0].Split(' ')[1];
        }
        private void Button_XCHGClick(object sender, RoutedEventArgs e)
        {
            var buttonName = (sender as Button).Content.ToString();
            SetMovePairs(buttonName);
            TextBox moveFromBox;
            TextBox moveToBox;

            if (textBoxBound.TryGetValue(moveTo, out moveToBox) && textBoxBound.TryGetValue(moveFrom, out moveFromBox))
            {
                var tempValueMoveTo = moveToBox.Text;
                moveToBox.Text = moveFromBox.Text;
                moveFromBox.Text = tempValueMoveTo;
            }
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            ResetTextButtons();
        }
        private void ResetTextButtons()
        {
            AX.Text = "0000";
            BX.Text = "0000";
            CX.Text = "0000";
            DX.Text = "0000";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AX.Text = GetRandomHexNumber(4);
            BX.Text = GetRandomHexNumber(4);
            CX.Text = GetRandomHexNumber(4);
            DX.Text = GetRandomHexNumber(4);
        }
    }
}
