using System;

public class Tile
    {
        public bool IsMine { get; set; }
        public bool IsClicked { get; set; }
        public int NumNeighbours { get; set; }
        // TODO: Handle right-click flagging
        
        public int Row { get; set; }
        public int Col { get; set; }

        public Tile(int row, int col)
        {
            Row = row;
            Col = col;
            IsMine = false;
            IsClicked = false;
            NumNeighbours = 0;
        }

        public void Click()
        {
            IsClicked = true;
        }
    }