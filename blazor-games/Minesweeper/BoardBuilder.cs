using System;
using System.Collections.Generic;
using System.Linq;

namespace blazor_games.Minesweeper
{
    public class BoardBuilder
    {
        private readonly BoardSize _boardSize;

        public BoardBuilder(BoardSize boardSize)
        {
            _boardSize = boardSize;
        }
        
        public Board Build()
        {
            var board = _boardSize switch
            {
                BoardSize.Small => BuildSmallBoard(),
                BoardSize.Medium => BuildMediumBoard(),
                BoardSize.Large => BuildLargeBoard(),
                _ => throw new ArgumentOutOfRangeException(nameof(_boardSize), _boardSize, "Invalid board size")
            };

            return board;
        }

        private static Board BuildSmallBoard()
        {
            var tiles = GetTiles(10, 10, 10);
            return new Board(tiles);
        }
        
        private static Board BuildMediumBoard()
        {
            var tiles = GetTiles(16, 16, 40);
            return new Board(tiles);
        }
        
        private static Board BuildLargeBoard()
        {
            var tiles = GetTiles(16, 30, 99);
            return new Board(tiles);
        }
        
        private static Tile[,] GetTiles(int numRows, int numCols, int numMines)
        {
            var minePositions = GetRandomMinePositions(numRows, numCols, numMines);
            return CreateTiles(numRows, numCols, minePositions);
        }

        private static Tile[,] CreateTiles(int numRows, int numCols, IReadOnlyCollection<(int row, int col)> minePositions)
        {
            var tiles = new Tile[numRows, numCols];
            for (var row = 0; row < numRows; row++)
            {
                for (var col = 0; col < numCols; col++)
                {
                    var isMineTile = minePositions.Any(x => x.row == row && x.col == col);
                    tiles[row, col] = isMineTile ? new MineTile() : new EmptyTile();
                }
            }

            return tiles;
        }

        private static IReadOnlyCollection<(int row, int col)> GetRandomMinePositions(int numRows, int numCols, int numMines)
        {
            var positions = new HashSet<(int row, int col)>();
            while (positions.Count < numMines) {
                var rand = new Random();
                var targetRow = (int) (rand.NextDouble() * numRows);
                var targetCol = (int) (rand.NextDouble() * numCols);

                positions.Add((targetRow, targetCol));
            }

            return positions;
        }
    }
}