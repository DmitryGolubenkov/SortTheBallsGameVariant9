﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SortTheBallsGameVariant9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private Game _game;
        private bool _gameInProgress = true;
        private int _selectedBall = -1;

        public GameWindow(int ballsCount)
        {
            InitializeComponent();

            _game = new Game(ballsCount, ShowVictoryScreen, ShowLossScreen, SetTurnNumber, SetTurnsLeftNumber);
            Render();
        }

        private const double ballSize = 65; //Размер шара
        private const double holeSize = 80; //Размер лунки
        private const double segmentSize = 90; //Ширина одного сегмента (лунка + шар). Высота зависит от высоты лунки.
        private const double topOffset=30; //Отступ от верхней границы при рендере лунок и шаров 

        private RadialGradientBrush _holeBrush;
        private RadialGradientBrush HoleBrush
        {
            get
            {
                if (_holeBrush is null)
                {
                    var gradientStops = new GradientStopCollection
                    {
                        new GradientStop(Color.FromRgb(80, 20, 0), 0.0),
                        new GradientStop(Color.FromRgb(51, 9, 0), 0.7),
                        new GradientStop(Color.FromRgb(41, 7, 0), 1)
                    };
                    _holeBrush = new RadialGradientBrush(gradientStops);
                }

                return _holeBrush;
            }
        }

        private RadialGradientBrush _whiteBallBrush;
        private RadialGradientBrush WhiteBallBrush
        {
            get
            {
                if (_whiteBallBrush is null)
                {
                    var gradientStops = new GradientStopCollection
                    {
                        new GradientStop(Color.FromRgb(250, 250, 250), 0.0),
                        new GradientStop(Color.FromRgb(140, 140, 140), 0.4),
                        new GradientStop(Color.FromRgb(110, 110, 110), 1)
                    };
                    _whiteBallBrush = new RadialGradientBrush(gradientStops) {GradientOrigin = new Point(0.75, 0.25)};
                }
                return _whiteBallBrush;
            }
        }


        private RadialGradientBrush _blackBallBrush;
        private RadialGradientBrush BlackBallBrush
        {
            get
            {
                if (_blackBallBrush is null)
                {
                    var gradientStops = new GradientStopCollection
                    {
                        new GradientStop(Color.FromRgb(250, 250, 250), 0.0),
                        new GradientStop(Color.FromRgb(60, 60, 60), 0.4),
                        new GradientStop(Color.FromRgb(10, 10, 10), 1)
                    };
                    _blackBallBrush = new RadialGradientBrush(gradientStops) {GradientOrigin = new Point(0.75, 0.25)};
                }
                return _blackBallBrush;
            }
        }
        public GameWindow(GameSave savedGame)
        {
            InitializeComponent();
            _game = new Game(savedGame, ShowVictoryScreen, ShowLossScreen, SetTurnNumber, SetTurnsLeftNumber);
            Render();
        }

        private void Render()
        {
            //Создаем прямоугольник для земли
            //Выставляем размеры относительно размера сегментов
            Rectangle holesBorderRectangle = new Rectangle
            {
                Width = segmentSize * _game.Balls.Length,
                Height = holeSize*2,
                Stroke = Brushes.DarkGreen,
                StrokeThickness = 2,
                Fill = Brushes.DarkGreen
            };
            GameCanvas.Children.Add(holesBorderRectangle);


            //Рисуем лунки и шары
            for (var index = 0; index < _game.Balls.Length; index++)
            {
                //Лунка
                Ellipse hole = new Ellipse
                {
                    Width = holeSize,
                    Height = holeSize,
                    Fill = HoleBrush,
                    StrokeThickness = 0
                };
                Canvas.SetLeft(hole, index * segmentSize+(segmentSize-holeSize)/2f);
                
                Canvas.SetTop(hole, topOffset);
                //Шар
                var ballType = _game.Balls[index];


                int offset = 0;
                if (index == _selectedBall) //Проверяем, является ли данный шар выделенным
                    offset = 15;


                Ellipse ball = new Ellipse
                {
                    Width = ballSize+offset,
                    Height = ballSize+offset,
                };

                Canvas.SetLeft(ball, index * segmentSize+(segmentSize-ballSize-offset)/2);
                Canvas.SetTop(ball, (segmentSize - ballSize - offset+topOffset+5) / 2f);
                //Выставляем цвет шара в зависимости от внутриигрового цвета
                if (ballType == Game.Ball.Black)
                {
                    ball.Stroke = BlackBallBrush;
                    ball.Fill = BlackBallBrush;
                }
                else
                {
                    ball.Stroke = WhiteBallBrush;
                    ball.Fill = WhiteBallBrush;
                }

                if (index == _selectedBall)
                    ball.Stroke = Brushes.DarkOrange;

                ball.Tag = index; //Устанавливаем ID эллипса для того, чтобы обрабатывать события с него
                ball.MouseUp += Ball_MouseUp;
                //Добавляем для рендера
                GameCanvas.Children.Add(hole);
                GameCanvas.Children.Add(ball);
            }

            GameCanvas.Width= segmentSize * _game.Balls.Length;
            GameCanvas.Height = segmentSize;
        }

        
        private void Ball_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(!_gameInProgress)
                return;

            Ellipse ball = sender as Ellipse;
            int ballId = (int)ball.Tag;

            //----
            //Если шар не выбран - выбираем.
            //Если шар выбран, проверяем, не нажали ли на тот же элемент.
            //Затем проверяем, не нажали ли на тот же тип шара. Такая же проверка есть на уровне класса игры,
            //тем не менее, она просто игнорирует смену элементов, но тратит ход.
            //
            if (_selectedBall == -1)
                _selectedBall = ballId;
            else if (_selectedBall == ballId)
            {
                //Убираем выделение
                _selectedBall = -1;
            }
            else
            {
                if (_game.Balls[ballId] != _game.Balls[_selectedBall])
                {
                    _game.MakeTurn(_selectedBall, ballId); //Делаем ход
                    _selectedBall = -1; //Убираем выделение
                }
                else
                {
                    MessageBox.Show("Невозможно поменять местами шары одного цвета.");
                }
            }
            Render();
        }

        private void SetTurnsLeftNumber(int turnsLeft)
        {
            TurnsLeftLabel.Content = "Осталось ходов: "+ turnsLeft.ToString();
        }

        private void SetTurnNumber(int currentTurn)
        {
            TurnNumberLabel.Content = "Ход: " + currentTurn.ToString();
        }

        private void ShowLossScreen()
        {
            SaveGameButton.IsEnabled = false;
            _gameInProgress = false;
            GameOverScreen screen = new GameOverScreen(Game.GameState.Lost);
            screen.Owner = this;
            screen.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            screen.Show();
        }

        private void ShowVictoryScreen()
        {
            SaveGameButton.IsEnabled = false;
            _gameInProgress = false;
            GameOverScreen screen = new GameOverScreen(Game.GameState.Win);
            screen.Owner = this;
            screen.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            screen.Show();
        }

        private void AboutApp_Click(object sender, RoutedEventArgs e)
        {
            AboutApp window = new AboutApp();
            window.Owner = this;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.Show();
        }
        private void AboutAuthor_Click(object sender, RoutedEventArgs e)
        {
            AboutAuthor window = new AboutAuthor();
            window.Owner = this;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.Show();
        }

        private void ComputerPlay_Enable(object sender, RoutedEventArgs e)
        {
            NewGameSettings window = new NewGameSettings();
            window.Owner = this;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.Show();
        }

        private void GoToMainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenu menu = new MainMenu();
            menu.Show();
            Close();
        }

        private void ExitGameButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SaveGameButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (SaveLoadController.SaveGameSave(new GameSave(_game.Turn, _game.TurnsLeft, _game.Balls), exception => MessageBox.Show("Ошибка сохранения: " + exception.Message + "\n\n\n" + exception.StackTrace)))
                MessageBox.Show("Сохранение выполнено успешно", "Успех");
            else
            {
                MessageBox.Show("Ошибка сохранения", "Ошибка");
            }
        }

        private void NewGameButton_OnClick(object sender, RoutedEventArgs e)
        {
            NewGameSettings window = new NewGameSettings();
            window.Owner = this;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.Show();
        }
    }
}
