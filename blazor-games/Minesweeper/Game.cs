namespace blazor_games.Minesweeper
{
    public class Game
    {
        public Board Board { get; set; }
        public GameStatus Status { get; set; }

        public Game(BoardSize boardSize)
        {
            var builder = new BoardBuilder(boardSize);
            Board = builder.Build();
            Status = GameStatus.Started;
        }
        
        public void LeftClickTile(Tile tile)
        {
            if (tile.GetStatus() == TileStatus.Flagged)
            {
                tile.Click();
                return;
            }
            
            if (tile.IsMine())
            {
                Status = GameStatus.Lost;
                Board.RevealAllTiles();
            }
            else
            {
                Board.ClickTile(tile);

                if (Board.IsCleared())
                {
                    Status = GameStatus.Won;
                    Board.RevealAllTiles();
                }
            }
        }

        public void RightClickTile(Tile tile)
        {
            if (tile.GetStatus() == TileStatus.Flagged)
            {
                tile.UnFlag();
            }
            else
            {
                tile.Flag();
            }
        }
    }
}