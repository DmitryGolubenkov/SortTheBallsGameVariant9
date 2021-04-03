using System;
using System.Windows;

namespace SortTheBallsGameVariant9
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();

            ContinueGameButton.IsEnabled = SaveLoadController.CanLoadGame();
        }

        /// <summary>
        /// Событие, открывающее окно "Об игре" при нажатии на кнопку "Об игре"
        /// </summary>
        private void AboutGameButton_Click(object sender, RoutedEventArgs e)
        {
            AboutApp window = new AboutApp();
            window.Owner = this;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.Show();
        }

        /// <summary>
        /// Событие, вызывающее загрузку уже сохраненной игры.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContinueGameButton_Click(object sender, RoutedEventArgs e)
        {
            var saveGame = SaveLoadController.LoadGameSave(exception => MessageBox.Show("Ошибка загрузки сохранения: " + exception.Message + "\n\n\n" + exception.StackTrace));
            if (saveGame != null)
            {
                GameWindow window = new GameWindow(saveGame);
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                window.Show();
                Close();
            }
        }

        /// <summary>
        /// Событие, открывающее окно "Настройка новой игры" при нажатии на кнопку "Настройка новой игры"
        /// </summary>
        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            NewGameSettings window = new NewGameSettings();
            window.Owner = this;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            window.Show();
        }

        /// <summary>
        /// Событие, открывающее окно "Об авторе" при нажатии на кнопку "Об авторе"
        /// </summary>
        private void AuthorButton_Click(object sender, RoutedEventArgs e)
        {
            AboutAuthor window = new AboutAuthor();
            window.Owner = this;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
         
            window.Show();
        }


        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
           Close();
        }
    }
}
