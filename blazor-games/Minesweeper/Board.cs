using System;
using blazor_games.Minesweeper;

public class Board
{
    private int NumRows { get; set; }
    private int NumCols { get; set; }
    public Tile[,] Tiles { get; set; }

    private Board(int numRows, int numCols, int numMines)
    {
        NumRows = numRows;
        NumCols = numCols;
        Tiles = new Tile[numRows, numCols];

        AddTiles();
        AddMines(numMines);
        AddNeighbourCount();
    }

    private void AddNeighbourCount()
    {
        for (var row = 0; row < Tiles.GetLength(0); row++)
        {
            for (var col = 0; col < Tiles.GetLength(1); col++)
            {
                Tiles[row, col].NumNeighbours = GetMineCount(row, col);
            }
        }
    }
    
    private int GetMineCount(int row, int col)
    {
        var count = 0;

        for (var r = row - 1; r <= row + 1; r++)
        {
            for (var c = col - 1; c <= col + 1; c++)
            {
                if (IsMine( r, c))
                {
                    count++;
                }
            }
        }

        return count;
    }

    private bool IsMine(int row, int col)
    {
        return IsOnBoard(row, col) && Tiles[row, col].IsMine;
    }

    private bool IsOnBoard(int row, int col)
    {
        return row >= 0 && col >= 0 && row < NumRows && col < NumCols;
    }
    
    public static Board Build(Size size)
    {
        return size switch
        {
            Size.Small => new Board(10, 10, 10),
            Size.Medium => new Board(16, 16, 40),
            Size.Large => new Board(16, 30, 99),
            _ => throw new ArgumentOutOfRangeException(nameof(size), size, "Invalid board size")
        };
    }

    private void AddTiles()
    {
        for (var row = 0; row < NumRows; row++)
        {
            for (var col = 0; col < NumCols; col++)
            {
                Tiles[row, col] = new Tile();
            }
        }
    }

    private void AddMines(int numMines)
    {
        for (var i = 0; i < numMines; i++)
        {
            var rand = new Random();
            var targetRow = (int) (rand.NextDouble() * NumRows);
            var targetCol = (int) (rand.NextDouble() * NumCols);

            Tiles[targetRow, targetCol].IsMine = true;
        }
    }

    public bool OnlyMinesRemaining()
    {
        for (var row = 0; row < NumRows; row++)
        {
            for (var col = 0; col < NumCols; col++)
            {
                if (!Tiles[row, col].IsMine && !Tiles[row, col].IsClicked)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void ClickAllTiles()
    {
        for (var row = 0; row < NumRows; row++)
        {
            for (var col = 0; col < NumCols; col++)
            {
                if (!Tiles[row, col].IsClicked)
                {
                    Tiles[row, col].Click();
                }
            }
        }

    }
}
