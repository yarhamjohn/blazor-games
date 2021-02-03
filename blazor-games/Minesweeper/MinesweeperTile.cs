using System;

public class MinesweeperTile
    {
        public bool IsMine { get; set; }
        private bool IsClicked { get; set; }
        // TODO: Handle right-click flagging

        public MinesweeperTile()
        {
            IsMine = false;
            IsClicked = false;
        }

        public void Click()
        {
            IsClicked = true;
            Console.WriteLine("clicked");
        }
    }