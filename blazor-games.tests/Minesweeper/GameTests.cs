using System;
using blazor_games.Minesweeper;
using FluentAssertions;
using NUnit.Framework;

namespace blazor_games.tests.Minesweeper
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void LeftClickTile_should_un_flag_tile_when_target_is_flagged()
        {
            var game = new Game(BoardSize.Small);
            game.Status.Should().Be(GameStatus.Started);

            var targetTile = game.Board.Tiles[0, 0];
            targetTile.Flag();
            game.LeftClickTile(targetTile);

            targetTile.GetStatus().Should().Be(TileStatus.Untouched);
            game.Status.Should().Be(GameStatus.Started);
        }        
        
        [Test]
        public void LeftClickTile_should_lose_the_game_if_target_is_a_mine_and_not_flagged()
        {
            var game = new Game(BoardSize.Small);
            game.Status.Should().Be(GameStatus.Started);

            var targetTile = GetAnyTile(game.Board.Tiles, mineTile: true);
            game.LeftClickTile(targetTile);

            game.Status.Should().Be(GameStatus.Lost);
            
            for (var r = 0; r < game.Board.Tiles.GetLength(0); r++)
            {
                for (var c = 0; c < game.Board.Tiles.GetLength(1); c++)
                {
                    game.Board.Tiles[r, c].GetStatus().Should().Be(TileStatus.Revealed);
                }
            }
        }        
        
        [Test]
        public void LeftClickTile_should_not_change_game_status_when_board_is_not_cleared()
        {
            var game = new Game(BoardSize.Small);
            game.Status.Should().Be(GameStatus.Started);

            var targetTile = GetAnyTile(game.Board.Tiles, mineTile: false);
            game.LeftClickTile(targetTile);

            game.Status.Should().Be(GameStatus.Started);
        }
        
        [Test]
        public void LeftClickTile_should_win_the_game_if_only_mines_remain()
        {
            var game = new Game(BoardSize.Small);
            game.Status.Should().Be(GameStatus.Started);

            for (var row = 0; row < game.Board.Tiles.GetLength(0); row++)
            {
                for (var col = 0; col < game.Board.Tiles.GetLength(1); col++)
                {
                    var targetTile = game.Board.Tiles[row, col];
                    if (!targetTile.IsMine())
                    {
                        game.LeftClickTile(targetTile);
                    }
                }
            }

            game.Status.Should().Be(GameStatus.Won);
                
            for (var r = 0; r < game.Board.Tiles.GetLength(0); r++)
            {
                for (var c = 0; c < game.Board.Tiles.GetLength(1); c++)
                {
                    game.Board.Tiles[r, c].GetStatus().Should().Be(TileStatus.Revealed);
                }
            }
        }
        
        [Test]
        public void RightClickTile_should_flag_an_untouched_tile()
        {
            var game = new Game(BoardSize.Small);
            var targetTile = game.Board.Tiles[0, 0];
            targetTile.GetStatus().Should().Be(TileStatus.Untouched);
            
            game.RightClickTile(targetTile);
            
            targetTile.GetStatus().Should().Be(TileStatus.Flagged);
        }
        
        [Test]
        public void RightClickTile_should_un_flag_an_flagged_tile()
        {
            var game = new Game(BoardSize.Small);
            var targetTile = game.Board.Tiles[0, 0];
            targetTile.Flag();
            targetTile.GetStatus().Should().Be(TileStatus.Flagged);
            
            game.RightClickTile(targetTile);
            
            targetTile.GetStatus().Should().Be(TileStatus.Untouched);
        }        
        
        [Test]
        public void RightClickTile_should_throw_if_target_tile_is_revealed()
        {
            // This case should never be hit because the razor page does not support right-clicking revealed tiles.
            
            var game = new Game(BoardSize.Small);
            var targetTile = GetAnyTile(game.Board.Tiles, mineTile: false);
            targetTile.Reveal();
            targetTile.GetStatus().Should().Be(TileStatus.Revealed);
            
            var ex = Assert.Throws<InvalidOperationException>(() => game.RightClickTile(targetTile));
            ex.Message.Should().Be("Cannot flag a clicked tile.");
        }

        private Tile GetAnyTile(Tile[,] tiles, bool mineTile)
        {
            for (var row = 0; row < tiles.GetLength(0); row++)
            {
                for (var col = 0; col < tiles.GetLength(1); col++)
                {
                    switch (mineTile)
                    {
                        case true when tiles[row, col].IsMine():
                            return tiles[row, col];
                        case false when !tiles[row, col].IsMine():
                            return tiles[row, col];
                    }
                }                
            }

            throw new InvalidOperationException("No empty tiles were found.");
        }
    }
}