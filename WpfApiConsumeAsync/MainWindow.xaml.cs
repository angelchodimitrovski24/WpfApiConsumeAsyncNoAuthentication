using BackEnd;
using BackEnd.Models;
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

namespace WpfApiConsumeAsync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int maxNumber = 0;
        private int currentNumber = 0;
        public MainWindow()
        {
            InitializeComponent();
            ApiConfig.InitializeClient();
            btnNext.IsEnabled = false;
        }

        private async Task LoadImage(int imageNumber=0)
        {
            var processor = await Processor.LoadRecord(imageNumber);

            if (imageNumber == 0)
            {
                maxNumber = processor.Num;

            }
            currentNumber = processor.Num;

            var uriSource = new Uri(processor.Img, UriKind.Absolute);


            imgSource.Source = new BitmapImage(uriSource);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadImage();
        }

        private async void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (currentNumber > 1)
            {
                currentNumber -= 1;
                btnNext.IsEnabled = true;

                await LoadImage(currentNumber);

                if (currentNumber == 1)
                {
                    btnPrevious.IsEnabled = false;
                }
            }
        }

        private async void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (currentNumber < maxNumber)
            {
                currentNumber += 1;
                btnPrevious.IsEnabled = true;

                await LoadImage(currentNumber);

                if (currentNumber == maxNumber)
                {
                    btnNext.IsEnabled = false;
                }
            }
        }
    }
}
