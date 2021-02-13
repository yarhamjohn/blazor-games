using System;
using System.Collections.Generic;
using System.Linq;

namespace blazor_games.TicTacToe
{
    public class Game
    {
        public Tile[,] Board { get; set; }
        public GameStatus Status { get; set; }
        public Counter Counter { get; set; }
        private Counter Winner { get; set; }

        public Game()
        {
            Board = new Tile[,] {{new(), new(), new()}, {new(), new(), new()}, {new(), new(), new()}};
            Status = GameStatus.Started;
            Counter = Counter.Circle;
            Winner = Counter.None;
        }

        public void PlaceCounter(Tile tile)
        {
            tile.Click(Counter);
            if (GameWon())
            {
                Status = GameStatus.Won;
                Winner = Counter;
            }

            SwitchCounter();
        }

        public Counter GetWinner()
        {
            return Winner;
        }

        private bool GameWon()
        {
            for (var row = 0; row < Board.GetLength(0); row++)
            {
                if (GroupMatchCounters(GetRow(row)))
                {
                    return true;
                }
            }
            
            for (var col = 0; col < Board.GetLength(1); col++)
            {
                if (GroupMatchCounters(GetColumn(col)))
                {
                    return true;
                }
            }

            var diagonals = new List<Tile[]>
                {new[] {Board[0, 0], Board[1, 1], Board[2, 2]}, new[] {Board[0, 2], Board[1, 1], Board[2, 0]}};

            return diagonals.Any(GroupMatchCounters);
        }

        private bool GroupMatchCounters(Tile[] tiles)
        {
            if (tiles.Any(t => t.GetCounter() == Counter.None))
            {
                return false;
            }

            return tiles.Select(t => t.GetCounter()).Distinct().Count() == 1;
        }
        
        public Tile[] GetColumn(int columnNumber)
        {
            return Enumerable.Range(0, Board.GetLength(0))
                .Select(x => Board[x, columnNumber])
                .ToArray();
        }

        public Tile[] GetRow(int rowNumber)
        {
            return Enumerable.Range(0, Board.GetLength(1))
                .Select(x => Board[rowNumber, x])
                .ToArray();
        }

        private void SwitchCounter()
        {
            Counter = Counter == Counter.Circle ? Counter.Cross : Counter.Circle;
        }
    }
}