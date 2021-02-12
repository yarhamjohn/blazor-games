using System;
using blazor_games.Minesweeper;
using FluentAssertions;
using NUnit.Framework;

namespace blazor_games.tests.Minesweeper
{
    [TestFixture]
    public class BoardTests
    {
        private Tile[,] Tiles { get; set; }

        [SetUp]
        public void Setup()
        {
            /*
             * Board shape:
             *      x x * x
             *      * x x x
             *      * x x x
             */
            Tiles = new Tile[,]
            {
                {new EmptyTile(), new EmptyTile(), new MineTile(), new EmptyTile()},
                {new MineTile(), new EmptyTile(), new EmptyTile(), new EmptyTile()},
                {new MineTile(), new EmptyTile(), new EmptyTile(), new EmptyTile()}
            };
        }

        [Test]
        public void Count_neighbouring_mines_should_return_0_given_no_neighbouring_mines()
        {
            var board = new Board(Tiles);
            var numMines = board.CountNeighbouringMines(Tiles[2, 2]);
            numMines.Should().Be(0);
        }

        [Test]
        public void Count_neighbouring_mines_should_return_correct_count_of_neighbouring_mines()
        {
            var board = new Board(Tiles);
            var numMines = board.CountNeighbouringMines(Tiles[0, 1]);
            numMines.Should().Be(2);
        }

        [Test]
        public void Count_neighbouring_mines_should_not_count_target_tile_even_if_its_a_mine()
        {
            var board = new Board(Tiles);
            var numMines = board.CountNeighbouringMines(Tiles[0, 2]);
            numMines.Should().Be(0);
        }

        [Test]
        public void Count_neighbouring_mines_should_throw_given_a_tile_not_on_the_board()
        {
            var board = new Board(Tiles);
            var ex = Assert.Throws<InvalidOperationException>(() => board.CountNeighbouringMines(new EmptyTile()));
            ex.Message.Should().Be("The specified tile was not found on the board.");
        }

        [Test]
        public void IsCleared_should_return_true_if_all_tiles_are_revealed()
        {
            var board = new Board(Tiles);
            board.RevealAllTiles();

            var isCleared = board.IsCleared();

            isCleared.Should().BeTrue();
        }

        [Test]
        public void IsCleared_should_return_true_if_only_mine_tiles_remain_unrevealed()
        {
            var board = new Board(Tiles);
            for (var r = 0; r < Tiles.GetLength(0); r++)
            {
                for (var c = 0; c < Tiles.GetLength(1); c++)
                {
                    if (!Tiles[r, c].IsMine())
                    {
                        Tiles[r, c].Reveal();
                    }
                }
            }

            var isCleared = board.IsCleared();

            isCleared.Should().BeTrue();
        }

        [Test]
        public void IsCleared_should_return_false_if_an_empty_tiles_remains_unrevealed()
        {
            var board = new Board(Tiles);

            var isCleared = board.IsCleared();

            isCleared.Should().BeFalse();
        }

        [Test]
        public void RevealAllTiles_should_do_nothing_if_all_tiles_are_revealed()
        {
            var board = new Board(Tiles);
            board.RevealAllTiles();

            board.RevealAllTiles();

            for (var r = 0; r < Tiles.GetLength(0); r++)
            {
                for (var c = 0; c < Tiles.GetLength(1); c++)
                {
                    Tiles[r, c].GetStatus().Should().Be(TileStatus.Revealed);
                }
            }
        }

        [Test]
        public void RevealAllTiles_should_reveal_all_previously_unrevealed_tiles()
        {
            var board = new Board(Tiles);
            for (var r = 0; r < Tiles.GetLength(0); r++)
            {
                for (var c = 0; c < Tiles.GetLength(1); c++)
                {
                    Tiles[r, c].GetStatus().Should().Be(TileStatus.Untouched);
                }
            }

            board.RevealAllTiles();

            for (var r = 0; r < Tiles.GetLength(0); r++)
            {
                for (var c = 0; c < Tiles.GetLength(1); c++)
                {
                    Tiles[r, c].GetStatus().Should().Be(TileStatus.Revealed);
                }
            }
        }

        [Test]
        public void ClickTile_should_un_flag_target_tile_and_reveal_no_tiles_if_target_is_flagged()
        {
            var board = new Board(Tiles);
            board.Tiles[0, 0].Flag();
            
            board.ClickTile(board.Tiles[0, 0]);
            
            for (var r = 0; r < Tiles.GetLength(0); r++)
            {
                for (var c = 0; c < Tiles.GetLength(1); c++)
                {
                    Tiles[r, c].GetStatus().Should().Be(TileStatus.Untouched);
                }
            }
        }
        
        [Test]
        public void ClickTile_should_only_reveal_itself_if_it_has_mine_neighbours()
        {
            var board = new Board(Tiles);
            
            board.ClickTile(board.Tiles[0, 0]);
            
            var expectedTileStatuses = new[,]
            {
                {TileStatus.Revealed, TileStatus.Untouched, TileStatus.Untouched, TileStatus.Untouched},
                {TileStatus.Untouched, TileStatus.Untouched, TileStatus.Untouched, TileStatus.Untouched},
                {TileStatus.Untouched, TileStatus.Untouched, TileStatus.Untouched, TileStatus.Untouched}
            };
            
            for (var r = 0; r < Tiles.GetLength(0); r++)
            {
                for (var c = 0; c < Tiles.GetLength(1); c++)
                {
                    Tiles[r, c].GetStatus().Should().Be(expectedTileStatuses[r, c]);
                }
            }
        }        
        
        [Test]
        public void ClickTile_should_reveal_all_neighbours_up_to_mine_count_boundary()
        {
            var board = new Board(Tiles);
            
            board.ClickTile(board.Tiles[2, 3]);

            var expectedTileStatuses = new[,]
            {
                {TileStatus.Untouched, TileStatus.Untouched, TileStatus.Untouched, TileStatus.Untouched},
                {TileStatus.Untouched, TileStatus.Revealed, TileStatus.Revealed, TileStatus.Revealed},
                {TileStatus.Untouched, TileStatus.Revealed, TileStatus.Revealed, TileStatus.Revealed}
            };
            
            for (var r = 0; r < Tiles.GetLength(0); r++)
            {
                for (var c = 0; c < Tiles.GetLength(1); c++)
                {
                    Tiles[r, c].GetStatus().Should().Be(expectedTileStatuses[r, c]);
                }
            }
        }      
        
        [Test]
        public void ClickTile_should_reveal_only_the_target_tile_if_target_was_a_mine()
        {
            // This case shouldn't be hit because the razor component doesn't call ClickTile if the target is a mine.
            
            var board = new Board(Tiles);
            
            board.ClickTile(board.Tiles[1, 0]);
            
            var expectedTileStatuses = new[,]
            {
                {TileStatus.Untouched, TileStatus.Untouched, TileStatus.Untouched, TileStatus.Untouched},
                {TileStatus.Revealed, TileStatus.Untouched, TileStatus.Untouched, TileStatus.Untouched},
                {TileStatus.Untouched, TileStatus.Untouched, TileStatus.Untouched, TileStatus.Untouched}
            };
            
            for (var r = 0; r < Tiles.GetLength(0); r++)
            {
                for (var c = 0; c < Tiles.GetLength(1); c++)
                {
                    Tiles[r, c].GetStatus().Should().Be(expectedTileStatuses[r, c]);
                }
            }
        }
    }
}