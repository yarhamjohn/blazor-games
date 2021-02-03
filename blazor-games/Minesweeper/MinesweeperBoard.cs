using System;

public class MinesweeperBoard
{
    private int NumRows { get; set; }
    private int NumCols { get; set; }
    public MinesweeperTile[,] Tiles { get; set; }

    public MinesweeperBoard(int numRows, int numCols)
    {
        NumRows = numRows;
        NumCols = numCols;
        Tiles = new MinesweeperTile[numRows, numCols];

        AddTiles();
    }

    private void AddTiles()
    {
        for (var row = 0; row < NumRows; row++)
        {
            for (var col = 0; col < NumCols; col++)
            {
                Tiles[row, col] = new MinesweeperTile();
            }
        }
    }

    public void AddMines(int numMines)
    {
        for (var i = 0; i < numMines; i++)
        {
            var rand = new Random();
            var targetRow = (int) (rand.NextDouble() * NumRows);
            var targetCol = (int) (rand.NextDouble() * NumCols);

            Tiles[targetRow, targetCol].IsMine = true;
        }
    }
}
