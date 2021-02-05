using System;
using System.Collections.Generic;
using System.Linq;

public class Board
{
    private int NumRows { get; }
    private int NumCols { get; }
    public Tile[,] Tiles { get; }

    public Board(Tile[,] tiles)
    {
        Tiles = tiles;
        NumRows = tiles.GetLength(0);
        NumCols = tiles.GetLength(1);
    }

    public void RevealAllTiles()
    {
        for (var row = 0; row < NumRows; row++)
        {
            for (var col = 0; col < NumCols; col++)
            {
                Tiles[row, col].Reveal();
            }
        }
    }
    
    public void ClickTile(Tile tile)
    {
        tile.Click();

        if (tile.IsMine())
        {
            // We don't want to reveal any neighbours if the target is a mine
            return;
        }
        
        if (tile.GetStatus() != TileStatus.Revealed)
        {
            // We don't want to reveal any neighbours as we simply un-flagged the target tile
            return;
        }

        if (CountNeighbouringMines(tile) == 0)
        {
            RevealAdjacentTiles(tile);
        }
    }
    
    public int CountNeighbouringMines(Tile tile)
    {
        var (row, col) = GetTilePosition(tile);
        return CountNeighbouringMines(row, col);
    }
    
    public bool IsCleared()
    {
        for (var row = 0; row < NumRows; row++)
        {
            for (var col = 0; col < NumCols; col++)
            {
                var isMine = Tiles[row, col].IsMine();
                var isNotClicked = Tiles[row, col].GetStatus() != TileStatus.Revealed;
                if (!isMine && isNotClicked)
                {
                    return false;
                }
            }
        }

        return true;
    }
    
    private int CountNeighbouringMines(int row, int col)
    {
        var count = 0;
        for (var r = row - 1; r <= row + 1; r++)
        {
            for (var c = col - 1; c <= col + 1; c++)
            {
                var isTargetTile = row == r && col == c;
                if (IsOnBoard(r, c) && Tiles[r, c].IsMine() && !isTargetTile)
                {
                    count++;
                }
            }
        }

        return count;
    }
    
    private void RevealAdjacentTiles(Tile tile)
    {
        var (row, col) = GetTilePosition(tile);
        if (tile.IsMine())
        {
            // Don't reveal the tile if its a mine
            return;
        }

        tile.Reveal();
        if (CountNeighbouringMines(row, col) > 0)
        {
            // Don't reveal any neighbouring tiles as its a boundary
            return;
        }

        var unrevealedNeighbours = GetNeighbouringTiles(row, col).Where(t => t.GetStatus() != TileStatus.Revealed);
        foreach (var neighbour in unrevealedNeighbours)
        {
            RevealAdjacentTiles(neighbour);
        }
    }
    
    private bool IsOnBoard(int row, int col)
    {
        return row >= 0 && col >= 0 && row < NumRows && col < NumCols;
    }

    private (int row, int col) GetTilePosition(Tile tile)
    {
        for (var r = 0; r < NumRows; r++)
        {
            for (var c = 0; c < NumCols; c++)
            {
                if (Tiles[r, c].Equals(tile))
                {
                    return (r, c);
                }
            }
        }

        throw new InvalidOperationException("The specified tile was not found on the board.");
    }
    
    private IEnumerable<Tile> GetNeighbouringTiles(int row, int col)
    {
        for (var r = row - 1; r <= row + 1; r++)
        {
            for (var c = col - 1; c <= col + 1; c++)
            {
                var isCurrentTile = r == row && c == col;
                if (IsOnBoard(r, c) && !isCurrentTile)
                {
                    yield return Tiles[r, c];
                }
            }
        }
    }
}
