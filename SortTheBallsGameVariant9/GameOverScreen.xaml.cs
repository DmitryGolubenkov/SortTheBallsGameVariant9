using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace SortTheBallsGameVariant9
{
    /// <summary>
    /// Логика взаимодействия для GameOverScreen.xaml
    /// </summary>
    public partial class GameOverScreen : Window
    {
        public GameOverScreen()
        {
            InitializeComponent();
        }

        public GameOverScreen(Game.GameState state)
        {
            InitializeComponent();
            if (state == Game.GameState.Continue)
            {
                Exception e = new Exception("Окончание игры не может использовался со статусом игры Continue.");
                throw e;
            }

            if (state == Game.GameState.Win)
            {
                GameOverStateHeader.Content = "Победа";
                GameOverStateHeader.Foreground = Brushes.LimeGreen;
                GameOverScreenText.Text =
                    "Вам удалось справиться с задачей за отведенное количество ходов. Так держать!";

            }
            else
            {
                GameOverStateHeader.Content = "Поражение";
                GameOverStateHeader.Foreground = Brushes.DarkRed;
                GameOverScreenText.Text =
                    "Вам не удалось справиться с задачей за отведенное количество ходов. Попробуйте ещё раз!";
            }
        }

        private void GoToMainMenuButton_Click(object sender, RoutedEventArgs e)
        {
           MainMenu menu = new MainMenu();
           menu.Show();
           Owner.Close();
           Close();
        }

        private bool isNewGameStarting = false;
        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            NewGameSettings window = new NewGameSettings();
            isNewGameStarting = true;
            window.Owner = this;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.Show();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if(isNewGameStarting) 
                Owner.Close();

            base.OnClosing(e);
        }
    }
}
