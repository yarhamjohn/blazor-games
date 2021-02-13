using System;
using blazor_games.Minesweeper;
using FluentAssertions;
using NUnit.Framework;

namespace blazor_games.tests.Minesweeper
{
    [TestFixture]
    public class TileTests
    {
        [TestFixture]
        public class MineTileTests
        {
            [Test]
            public void Clicking_a_mine_tile_should_mark_it_clicked()
            {
                var tile = new MineTile();
                tile.GetStatus().Should().Be(TileStatus.Untouched);

                tile.Click();
                
                tile.GetStatus().Should().Be(TileStatus.Revealed);
            }

            [Test]
            public void Clicking_a_mine_tile_should_remove_the_flag()
            {
                var tile = new MineTile();
                tile.Flag();
                tile.GetStatus().Should().Be(TileStatus.Flagged);

                tile.Click();
                
                tile.GetStatus().Should().Be(TileStatus.Untouched);
            }

            [Test]
            public void Flagging_a_flagged_tile_should_do_nothing()
            {
                var tile = new MineTile();
                tile.Flag();
                tile.GetStatus().Should().Be(TileStatus.Flagged);

                tile.Flag();
                
                tile.GetStatus().Should().Be(TileStatus.Flagged);
            }

            [Test]
            public void Flagging_an_untouched_tile_should_flag_it()
            {
                var tile = new MineTile();
                tile.GetStatus().Should().Be(TileStatus.Untouched);

                tile.Flag();
                
                tile.GetStatus().Should().Be(TileStatus.Flagged);
            }

            [Test]
            public void Flagging_a_clicked_tile_should_throw_an_error()
            {
                var tile = new MineTile();
                tile.Click();
                tile.GetStatus().Should().Be(TileStatus.Revealed);

                var ex = Assert.Throws<InvalidOperationException>(() => tile.Flag());
                ex.Message.Should().Be("Cannot flag a clicked tile.");
            }

            [Test]
            public void Unflagging_an_untouched_tile_should_do_nothing()
            {
                var tile = new MineTile();
                tile.GetStatus().Should().Be(TileStatus.Untouched);

                tile.UnFlag();
                
                tile.GetStatus().Should().Be(TileStatus.Untouched);
            }

            [Test]
            public void Unflagging_an_flagged_tile_should_un_flag_it()
            {
                var tile = new MineTile();
                tile.Flag();
                tile.GetStatus().Should().Be(TileStatus.Flagged);

                tile.UnFlag();
                
                tile.GetStatus().Should().Be(TileStatus.Untouched);
            }

            [Test]
            public void Unflagging_a_clicked_tile_should_throw_an_error()
            {
                var tile = new MineTile();
                tile.Click();
                tile.GetStatus().Should().Be(TileStatus.Revealed);

                var ex = Assert.Throws<InvalidOperationException>(() => tile.UnFlag());
                ex.Message.Should().Be("Cannot un-flag a clicked tile.");
            }
            
            [Test]
            public void Revealing_a_clicked_tile_should_do_nothing()
            {
                var tile = new MineTile();
                tile.Click();
                tile.GetStatus().Should().Be(TileStatus.Revealed);

                tile.Reveal();
                
                tile.GetStatus().Should().Be(TileStatus.Revealed);
            }
            
            [Test]
            public void Revealing_a_flagged_tile_should_set_status_to_clicked()
            {
                var tile = new MineTile();
                tile.Flag();
                tile.GetStatus().Should().Be(TileStatus.Flagged);

                tile.Reveal();
                
                tile.GetStatus().Should().Be(TileStatus.Revealed);
            }
            
            [Test]
            public void Revealing_an_untouched_tile_should_set_status_to_clicked()
            {
                var tile = new MineTile();
                tile.GetStatus().Should().Be(TileStatus.Untouched);

                tile.Reveal();
                
                tile.GetStatus().Should().Be(TileStatus.Revealed);
            }
        }
        
        
        [TestFixture]
        public class EmptyTileTests
        {
            [Test]
            public void Clicking_a_mine_tile_should_mark_it_clicked()
            {
                var tile = new EmptyTile();
                tile.GetStatus().Should().Be(TileStatus.Untouched);

                tile.Click();
                
                tile.GetStatus().Should().Be(TileStatus.Revealed);
            }

            [Test]
            public void Clicking_a_mine_tile_should_remove_the_flag()
            {
                var tile = new EmptyTile();
                tile.Flag();
                tile.GetStatus().Should().Be(TileStatus.Flagged);

                tile.Click();
                
                tile.GetStatus().Should().Be(TileStatus.Untouched);
            }

            [Test]
            public void Flagging_a_flagged_tile_should_do_nothing()
            {
                var tile = new EmptyTile();
                tile.Flag();
                tile.GetStatus().Should().Be(TileStatus.Flagged);

                tile.Flag();
                
                tile.GetStatus().Should().Be(TileStatus.Flagged);
            }

            [Test]
            public void Flagging_an_untouched_tile_should_flag_it()
            {
                var tile = new EmptyTile();
                tile.GetStatus().Should().Be(TileStatus.Untouched);

                tile.Flag();
                
                tile.GetStatus().Should().Be(TileStatus.Flagged);
            }

            [Test]
            public void Flagging_a_clicked_tile_should_throw_an_error()
            {
                var tile = new EmptyTile();
                tile.Click();
                tile.GetStatus().Should().Be(TileStatus.Revealed);

                var ex = Assert.Throws<InvalidOperationException>(() => tile.Flag());
                ex.Message.Should().Be("Cannot flag a clicked tile.");
            }

            [Test]
            public void Unflagging_an_untouched_tile_should_do_nothing()
            {
                var tile = new EmptyTile();
                tile.GetStatus().Should().Be(TileStatus.Untouched);

                tile.UnFlag();
                
                tile.GetStatus().Should().Be(TileStatus.Untouched);
            }

            [Test]
            public void Unflagging_an_flagged_tile_should_un_flag_it()
            {
                var tile = new EmptyTile();
                tile.Flag();
                tile.GetStatus().Should().Be(TileStatus.Flagged);

                tile.UnFlag();
                
                tile.GetStatus().Should().Be(TileStatus.Untouched);
            }

            [Test]
            public void Unflagging_a_clicked_tile_should_throw_an_error()
            {
                var tile = new EmptyTile();
                tile.Click();
                tile.GetStatus().Should().Be(TileStatus.Revealed);

                var ex = Assert.Throws<InvalidOperationException>(() => tile.UnFlag());
                ex.Message.Should().Be("Cannot un-flag a clicked tile.");
            }
            
            [Test]
            public void Revealing_a_clicked_tile_should_do_nothing()
            {
                var tile = new EmptyTile();
                tile.Click();
                tile.GetStatus().Should().Be(TileStatus.Revealed);

                tile.Reveal();
                
                tile.GetStatus().Should().Be(TileStatus.Revealed);
            }
            
            [Test]
            public void Revealing_a_flagged_tile_should_set_status_to_clicked()
            {
                var tile = new EmptyTile();
                tile.Flag();
                tile.GetStatus().Should().Be(TileStatus.Flagged);

                tile.Reveal();
                
                tile.GetStatus().Should().Be(TileStatus.Revealed);
            }
            
            [Test]
            public void Revealing_an_untouched_tile_should_set_status_to_clicked()
            {
                var tile = new EmptyTile();
                tile.GetStatus().Should().Be(TileStatus.Untouched);

                tile.Reveal();
                
                tile.GetStatus().Should().Be(TileStatus.Revealed);
            }
        }
    }
}