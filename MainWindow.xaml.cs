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
using System.Windows.Threading;

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isFirstSelection;
        TextBlock firstSelectedTextBlock;

        DispatcherTimer timer = new DispatcherTimer();
        int tenthsofSecondsElapsed;
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;

            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsofSecondsElapsed++;
            timeTextBlock.Text = (tenthsofSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " -Play Again?";
            }
        }

        private void SetUpGame()
        {
            isFirstSelection = true;

            firstSelectedTextBlock = new TextBlock();

            List<string> animalEmoji = new List<string>()
            {
                "🐙","🐙",
                "🐼","🐼",
                "🐷","🐷",
                "🐮","🐮",
                "🦝","🦝",
                "🐔","🐔",
                "🦍","🦍",
                "🦏","🦏",
            };

            Random random = new Random();

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>()) 
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }
            }

            timer.Start();
            tenthsofSecondsElapsed = 0;
            matchesFound = 0;
        }

        
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isFirstSelection)
            {
                firstSelectedTextBlock = (TextBlock)sender;
                firstSelectedTextBlock.Visibility = Visibility.Hidden;
                isFirstSelection = false;
            }
            else
            {
                TextBlock secondSelectedTextBlock = (TextBlock)sender;
                if (secondSelectedTextBlock.Text.Equals(firstSelectedTextBlock.Text))
                {
                    secondSelectedTextBlock.Visibility = Visibility.Hidden;
                    matchesFound++;                    
                }
                else 
                {
                    firstSelectedTextBlock.Visibility = Visibility.Visible;
                }
                isFirstSelection = true;
            }
        }

        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}
