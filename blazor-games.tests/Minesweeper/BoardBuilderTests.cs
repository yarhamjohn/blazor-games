using blazor_games.Minesweeper;
using FluentAssertions;
using NUnit.Framework;

namespace blazor_games.tests.Minesweeper
{
    [TestFixture]
    public class BoardBuilderTests
    {
        [TestCase(BoardSize.Small, 10, 10)]
        [TestCase(BoardSize.Medium, 16, 16)]
        [TestCase(BoardSize.Large, 16, 30)]
        public void BoardBuilder_creates_correct_size_board(BoardSize boardSize, int numRows, int numCols)
        {
            var builder = new BoardBuilder(boardSize);
            var board = builder.Build();

            board.Tiles.GetLength(0).Should().Be(numRows);
            board.Tiles.GetLength(1).Should().Be(numCols);
        }
        
        [TestCase(BoardSize.Small, 10)]
        [TestCase(BoardSize.Medium, 40)]
        [TestCase(BoardSize.Large, 99)]
        public void BoardBuilder_creates_board_with_correct_number_of_mines(BoardSize boardSize, int numMines)
        {
            var builder = new BoardBuilder(boardSize);
            var board = builder.Build();

            CountMines(board.Tiles).Should().Be(numMines);
        }

        private static int CountMines(Tile[,] tiles)
        {
            var count = 0;
            for (var row = 0; row < tiles.GetLength(0); row++)
            {
                for (var col = 0; col < tiles.GetLength(1); col++)
                {
                    if (tiles[row, col].IsMine())
                    {
                        count++;
                    }
                }                
            }

            return count;
        }
    }
}