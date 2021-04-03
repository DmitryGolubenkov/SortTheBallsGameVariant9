namespace SortTheBallsGameVariant9
{
    /// <summary>
    /// Сохранение игры. Хранит все данные, необходимые для восстановления игрового процесса.
    /// </summary>
    public class GameSave
    {
        /// <summary>
        /// Номер хода.
        /// </summary>
        public int Turn { get; set; }

        /// <summary>
        /// Сколько осталось ходов.
        /// </summary>
        public int TurnsLeft { get; set; }

        /// <summary>
        /// Состояние игрового поля. Выражено в белых и черных шарах.
        /// </summary>
        public Game.Ball[] Balls { get; set; }

        public GameSave(int turn, int turnsLeft, Game.Ball[] balls)
        {
            Turn = turn;
            TurnsLeft = turnsLeft;
            Balls = balls;
        }
    }
}