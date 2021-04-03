namespace SortTheBallsGameVariant9
{
    internal class Bot
    {
        private int _blackCount = 0;
        private int _whiteCount = 0;
        internal void MakeTurn(Game game)
        {
            //Бот считает, сколько шаров каждого цвета на поле, если не посчитал заранее
            if(_blackCount == 0 || _whiteCount == 0)
            foreach (var ball in game.Balls)
            {
                if (ball == Game.Ball.White)
                    _whiteCount++;
                else
                    _blackCount++;
            }

            //Выгоднее перемещать те шарики, которых меньше всего на поле
            //Алгоритм при этом похож:
            //1) Выбираем, кого брать. Это - самый дальний от своей границы шарик перемещаемого цвета.
            //2) Выбираем, куда ставить. Это - самая близкая к своей границе позиция, не занятая черным шариком
            //
            //Таким образом, задача будет решена за достаточное для победы количество ходов (если бот был включен изначально)
            //Бот не сможет выиграть невыигрываемую игру, если пользователь потратил все ходы.
            //Также бот не может написать симфонию, или взять чистый холст и превратить его в шедевр.
            if (_blackCount < _whiteCount)
            {
                //Перемещаем черных
                //Черные должны быть в конце.
                int chosenBallId = -1;
                for (int i = 0; i < game.Balls.Length; i++)
                    if (game.Balls[i] == Game.Ball.Black)
                    {
                        chosenBallId = i;
                        break;
                    }

                for (int i = game.Balls.Length - 1; i >= 0; i--)
                    if (game.Balls[i] != Game.Ball.Black)
                    {
                        game.MakeTurn(chosenBallId, i);
                        break;
                    }
            }
            else
            {
                //Перемещаем белых
                int chosenBallId = -1;
                for (int i = game.Balls.Length - 1; i >= 0; i--)
                    
                    if (game.Balls[i] == Game.Ball.White)
                    {
                        chosenBallId = i;
                        break;
                    }

                for (int i = 0; i < game.Balls.Length; i++)
                    if (game.Balls[i] != Game.Ball.White)
                    {
                        game.MakeTurn(chosenBallId, i);
                        break;
                    }
            }
        }
    }
}