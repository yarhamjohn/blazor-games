using System;

namespace blazor_games.Minesweeper
{
    public abstract class Tile
    {
        private TileStatus Status { get; set; }

        protected Tile()
        {
            Status = TileStatus.Untouched;
        }

        public abstract bool IsMine();

        public void Click()
        {
            if (Status == TileStatus.Flagged)
            {
                UnFlag();
            }
            else
            {
                Reveal();
            }
        }

        public void Flag()
        {
            if (Status == TileStatus.Revealed)
            {
                throw new InvalidOperationException("Cannot flag a clicked tile.");
            }

            Status = TileStatus.Flagged;
        }

        public void UnFlag()
        {
            if (Status == TileStatus.Revealed)
            {
                throw new InvalidOperationException("Cannot un-flag a clicked tile.");
            }

            Status = TileStatus.Untouched;
        }

        public void Reveal()
        {
            Status = TileStatus.Revealed;
        }

        public TileStatus GetStatus()
        {
            return Status;
        }
    }

    public class MineTile : Tile
    {
        public override bool IsMine() => true;
    }

    public class EmptyTile : Tile
    {
        public override bool IsMine() => false;
    }
}