using System;
using System.Windows;

namespace SortTheBallsGameVariant9
{
    /// <summary>
    /// Логика взаимодействия для NewGameSettings.xaml
    /// </summary>
    public partial class NewGameSettings : Window
    {
        public NewGameSettings()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Событие, вызываемое при нажатии на кнопку "Отмена"
        /// </summary>
        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Событие, вызываемое при нажатии на кнопку "Начать игру". 
        /// </summary>
        private void NewGameStart_Click(object sender, RoutedEventArgs e)
        {
            int count = -1;
            try
            {
                //Обрабатываем ввод
                count = Convert.ToInt32(CountTextBox.Text);
                //TODO: Добавить проверку корректного количества
                // 1) Больше одного
                // 2) Не больше установленной границы (чтобы нельзя было создать игру на миллион лунок). Думаю, 10 хватит
 
            }
            catch
            {
                MessageBox.Show("Настройки игры неверные.", "Ошибка");
            }

            //Создаем игровое окно и закрываем окна меню и настроек новой игры
            if (count != -1)
            {
                GameWindow window = new GameWindow(count);
                window.Show();
                Owner.Close();
            }
        }
    }
}
