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

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List <string> choices ;
        TextBlock firstSelectedTextBlock;

        public MainWindow()
        {
            choices = new List<string>();

            firstSelectedTextBlock = new TextBlock();

            InitializeComponent();

            SetUpGame();
        }

        private void SetUpGame()
        {
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
                int index = random.Next(animalEmoji.Count);
                string nextEmoji = animalEmoji[index];
                textBlock.Text = nextEmoji;
                animalEmoji.RemoveAt(index);
            }
        }

        
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (choices.Count == 0)
            {
                firstSelectedTextBlock = (TextBlock)sender;
                choices.Add(firstSelectedTextBlock.Text);
                firstSelectedTextBlock.Visibility = Visibility.Hidden;
            }
            else
            {
                TextBlock secondSelectedTextBlock = (TextBlock)sender;
                if (secondSelectedTextBlock.Text.Equals(choices[0]))
                {
                    secondSelectedTextBlock.Visibility = Visibility.Hidden;
                    firstSelectedTextBlock = new TextBlock();
                    choices.RemoveAt(0);
                }
                else 
                {
                    firstSelectedTextBlock.Visibility = Visibility.Visible;
                    choices.RemoveAt(0);                    
                }
            }
        }
    }
}
