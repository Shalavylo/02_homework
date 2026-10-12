using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _02_homework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAnalyze_Click(object sender, RoutedEventArgs e)
        {
            string text = txtText.Text;
            if(string.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show("Введіть текст!");
                return;
            }

            Task<string> task1 = new Task<string>(() =>
            {
                return AnalyzeText(text);
            });

            Task task2 = task1.ContinueWith(prevTask =>
            {
                string result = prevTask.Result;

                Dispatcher.Invoke(() =>
                {
                    txtResult.Text = result;
                });
            });
            task1.Start();
        }

        static string AnalyzeText(string text)
        {
            int characters = text.Length;
            int words = Regex.Matches(
                text, @"\b[\p{L}\p{N}]+\b").Count;

            int sentences = Regex.Matches(
                text, @"[.!?]+").Count;

            int questions = Regex.Matches(
                text, @"\?").Count;

            int exclamations = Regex.Matches(
                text, @"!").Count;

            return "РЕЗУЛЬТАТ АНАЛІЗУ ТЕКСТУ\r\n\r\n" +
                $"Кількість речень: {sentences}\r\n" +
                $"Кількість символів: {characters}\r\n" +
                $"Кількість слів: {words}\r\n" +
                $"Кількість питальних речень: {questions}\r\n" +
                $"Кількість окличних речень: {exclamations}";
        }
    }
}