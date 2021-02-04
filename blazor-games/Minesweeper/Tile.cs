using System;

public class Tile
    {
        public bool IsMine { get; set; }
        public bool IsClicked { get; set; }
        public int NumNeighbours { get; set; }
        // TODO: Handle right-click flagging

        public Tile()
        {
            IsMine = false;
            IsClicked = false;
            NumNeighbours = 0;
        }

        public void Click()
        {
            IsClicked = true;
        }
    }