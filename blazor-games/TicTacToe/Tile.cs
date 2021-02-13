namespace blazor_games.TicTacToe
{
    public class Tile
    {
        private TileStatus Status { get; set; }
        private Counter Counter { get; set; }

        public Tile()
        {
            Status = TileStatus.Unclicked;
            Counter = Counter.None;
        }

        public void Click(Counter counter)
        {
            if (Status != TileStatus.Unclicked)
            {
                return;
            }

            Status = TileStatus.Clicked;
            Counter = counter;
        }

        public TileStatus GetStatus()
        {
            return Status;
        }

        public Counter GetCounter()
        {
            return Counter;
        }
    }
}