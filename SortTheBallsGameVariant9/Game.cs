using System;
using static SortTheBallsGameVariant9.Game;

namespace SortTheBallsGameVariant9
{
    public class Game
    {
        private readonly ShowVictoryScreen _victoryScreen;
        private readonly ShowLossScreen _lossScreen;
        private readonly SetTurnNumberInUI _setTurnNumberInUi;
        private readonly SetTurnsLeftInUI _setTurnsLeftInUi;
        /// <summary>
        /// Номер хода.
        /// </summary>
        public int Turn { get; private set; } = 1;
        /// <summary>
        /// Сколько осталось ходов.
        /// </summary>
        public int TurnsLeft { get; private set; }
        /// <summary>
        /// Состояние игрового поля. Выражено в белых и черных шарах.
        /// </summary>
        public Ball[] Balls { get; private set; }

        private Random _random;

        //API для связи с UI
        public delegate void ShowVictoryScreen();
        public delegate void ShowLossScreen();
        public delegate void SetTurnNumberInUI(int currentTurn);
        public delegate void SetTurnsLeftInUI(int turnsLeft);

        public Game(int ballsCount, ShowVictoryScreen victoryScreen, ShowLossScreen lossScreen, SetTurnNumberInUI setTurnNumberInUI, SetTurnsLeftInUI setTurnsLeftInUI)
        {
            _victoryScreen = victoryScreen;
            _lossScreen = lossScreen;
            _setTurnNumberInUi = setTurnNumberInUI;
            _setTurnsLeftInUi = setTurnsLeftInUI;

            GenerateNewField(ballsCount);
            TurnsLeft = ballsCount / 2;

            //Записываем в UI информацию о ходах
            _setTurnNumberInUi(Turn);
            _setTurnsLeftInUi(TurnsLeft);
        }

        public Game(GameSave savedGame, ShowVictoryScreen victoryScreen, ShowLossScreen lossScreen, SetTurnNumberInUI setTurnNumberInUI, SetTurnsLeftInUI setTurnsLeftInUI)
        {
            _victoryScreen = victoryScreen;
            _lossScreen = lossScreen;
            _setTurnNumberInUi = setTurnNumberInUI;
            _setTurnsLeftInUi = setTurnsLeftInUI;

            TurnsLeft = savedGame.TurnsLeft;
            Turn = savedGame.Turn;
            Balls = savedGame.Balls;

            //Записываем в UI информацию о ходах
            _setTurnNumberInUi(Turn);
            _setTurnsLeftInUi(TurnsLeft);
        }

        /// <summary>
        /// Метод создаёт новое игровое поле и сохраняет его в свойство Balls
        /// </summary>
        /// <param name="ballsCount">Количество шаров на поле</param>
        private void GenerateNewField(in int ballsCount)
        {
            Balls = new Ball[ballsCount];
            _random = new Random(Guid.NewGuid().GetHashCode() + Environment.TickCount);
            for (var ballIndex = 0; ballIndex < Balls.Length; ballIndex++)
                Balls[ballIndex] = Convert.ToBoolean(_random.Next(0, 2)) ? Ball.Black : Ball.White;

            //TODO: написать проверку на то, что массив не должен состоять из одинаковых элементов
            //Есть вероятность, что игра будет создана только с чёрными или только с берыми шариками. Требуется исключить это.
            //Если поле было создано именно таким - нужно его пересоздать.
        }

        /// <summary>
        /// Метод позволяет выполнить ход в игре. Он меняет шары местами и решает, что дальше: победа, поражение или продолжение игры.
        /// </summary>
        /// <param name="firstBallToSwap">ID первого шара в массиве, который был выбран для хода.</param>
        /// <param name="secondBallToSwap">ID второго шара в массиве, который был выбран для хода.</param>
        public void MakeTurn(int firstBallToSwap, int secondBallToSwap)
        {
            TurnsLeft--;
            Turn++;
            _setTurnsLeftInUi(TurnsLeft);

            SwapBalls(firstBallToSwap, secondBallToSwap);
            GameState state = CheckGameOver();
            switch (state)
            {
                case GameState.Continue:
                    break;
                case GameState.Lost:
                    GameOver();
                    break;
                case GameState.Win:
                    Victory();
                    break;
            }

            _setTurnNumberInUi(Turn);
        }

        private void Victory()
        {
            _victoryScreen();
        }

        private void GameOver()
        {
            _lossScreen();
        }

        /// <summary>
        /// Меняет местами шары в игровом массиве.
        /// </summary>
        /// <param name="ballA">ID первого шара.</param>
        /// <param name="ballB">ID второго шара.</param>
        private void SwapBalls(int ballA, int ballB)
        {
            if (ballA != ballB)
            {
                var firstBall = Balls[ballA];
                Balls[ballA] = Balls[ballB];
                Balls[ballB] = firstBall;
            }
        }

        /// <summary>
        /// Метод проверяет, окончена ли игра. И, если да - почему.
        /// </summary>
        /// <returns>
        /// GameState.Win, если победа т.е. все шары отсортированы.
        /// GameState.Lost, если поражение т.е. были потрачены все ходы, и шары не отсортированы.
        /// GameState.Continue, если игра продолжается.
        /// </returns>
        private GameState CheckGameOver()
        {
            var isSorted = true;
            bool isSecondPart = false;
            for (int i = 0; i < Balls.Length; i++)
            {
                //Если мы находим в первой половине черный шар - считаем, что дальше вторая половина с только черными шарами
                if (Balls[i] == Ball.Black && !isSecondPart)
                    isSecondPart = true;
                //Если мы находим во второй половине белый шар - значит, сортировка не выполнена
                else if (Balls[i] == Ball.White && isSecondPart)
                {
                    isSorted = false;
                    break;
                }
            }

            //Если отсортировали - победа
            if (isSorted)
                return GameState.Win;

            //Продолжаем, если остались ходы. Возвращаем поражение, если нет.
            return TurnsLeft > 0 ? GameState.Continue : GameState.Lost;
        }

        public enum Ball
        {
            Black,
            White
        }

        public enum GameState
        {
            Continue,
            Lost,
            Win
        }
    }
}