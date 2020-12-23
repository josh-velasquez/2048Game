using NUnit.Framework;
using System.Collections.Generic;
using TwentyFortyEight;

namespace TwentyFortyEightUnitTest
{
    public class Tests
    {
        private const int boardSize = 4;

        [Test]
        public void InitialGameState_Test()
        {
            Game game = new Game(boardSize);
            int validTileCount = 0;
            // Check if there are only two values that are shown when starting the game
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (game.board[i][j] != 0)
                    {
                        validTileCount++;
                    }
                }
            }
            Assert.AreEqual(2, validTileCount);
        }

        [Test]
        public void WinningBoard_Test()
        {
            Game game = new Game(boardSize);
            List<List<int>> winningBoard = new List<List<int>> { new List<int> { 2, 2, 4, 4 }, new List<int> { 2, 4, 8, 8 }, new List<int> { 1024, 4, 2, 4 }, new List<int> { 1024, 2, 2, 2 } };
            game.board = winningBoard;
            game.UserMove(Moves.Down);
            Assert.IsTrue(game.IsGameWon());
        }

        [Test]
        public void LosingBoard_Test()
        {
            Game game = new Game(boardSize);
            List<List<int>> losingBoard = new List<List<int>> { new List<int> { 16, 32, 4, 8 }, new List<int> { 2, 8, 16, 32 }, new List<int> { 2, 64, 32, 64 }, new List<int> { 16, 32, 4, 256 } };
            game.board = losingBoard;
            game.UserMove(Moves.Down);
            Assert.IsTrue(game.IsGameOver());
        }

        [Test]
        public void EmptyTileBetweenTheSameNumber_LeftShift_Test()
        {
            Game game = new Game(boardSize);
            List<List<int>> specialBoard = new List<List<int>> { new List<int> { 4, 0, 0, 4 }, new List<int> { 2, 0, 2, 0 }, new List<int> { 0, 2, 0, 2 }, new List<int> { 2, 0, 2, 0 } };
            List<List<int>> resultingBoard = new List<List<int>> { new List<int> { 8, 0, 0, 0 }, new List<int> { 4, 0, 0, 0 }, new List<int> { 4, 0, 0, 0 }, new List<int> { 4, 0, 0, 0 } };
            game.board = specialBoard;
            game.UserMove(Moves.Left);
            Assert.IsFalse(game.IsGameWon());
            Assert.IsFalse(game.IsGameOver());
            // The resulting matrix should be the same with the exception of the newly created tiles
            for (int i = 0; i < boardSize; i++)
            {
                Assert.AreEqual(game.board[i][0], resultingBoard[i][0]);
            }
        }

        [Test]
        public void EmptyTileBetweenTheSameNumber_RightShift_Test()
        {
            Game game = new Game(boardSize);
            List<List<int>> specialBoard = new List<List<int>> { new List<int> { 4, 0, 0, 4 }, new List<int> { 2, 0, 2, 0 }, new List<int> { 0, 2, 0, 2 }, new List<int> { 2, 0, 2, 0 } };
            List<List<int>> resultingBoard = new List<List<int>> { new List<int> { 0, 0, 0, 8 }, new List<int> { 0, 0, 0, 4 }, new List<int> { 0, 0, 0, 4 }, new List<int> { 0, 0, 0, 4 } };
            game.board = specialBoard;
            game.UserMove(Moves.Right);
            Assert.IsFalse(game.IsGameWon());
            Assert.IsFalse(game.IsGameOver());
            // The resulting matrix should be the same with the exception of the newly created tiles
            for (int i = 0; i < boardSize; i++)
            {
                Assert.AreEqual(game.board[i][boardSize - 1], resultingBoard[i][boardSize - 1]);
            }
        }

        [Test]
        public void EmptyTileBetweenTheSameNumber_UpShift_Test()
        {
            Game game = new Game(boardSize);
            List<List<int>> specialBoard = new List<List<int>> { new List<int> { 4, 0, 0, 4 }, new List<int> { 2, 0, 2, 0 }, new List<int> { 0, 2, 0, 2 }, new List<int> { 2, 0, 2, 0 } };
            List<List<int>> resultingBoard = new List<List<int>> { new List<int> { 4, 2, 4, 4 }, new List<int> { 4, 0, 0, 2 }, new List<int> { 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0 } };
            game.board = specialBoard;
            game.UserMove(Moves.Up);
            Assert.IsFalse(game.IsGameWon());
            Assert.IsFalse(game.IsGameOver());
            // The resulting matrix should be the same with the exception of the newly created tiles
            for (int j = 0; j < boardSize; j++)
            {
                Assert.AreEqual(game.board[0][j], resultingBoard[0][j]);
            }
        }

        [Test]
        public void EmptyTileBetweenTheSameNumber_DownShift_Test()
        {
            Game game = new Game(boardSize);
            List<List<int>> specialBoard = new List<List<int>> { new List<int> { 4, 0, 0, 4 }, new List<int> { 2, 0, 2, 0 }, new List<int> { 0, 2, 0, 2 }, new List<int> { 2, 0, 2, 0 } };
            List<List<int>> resultingBoard = new List<List<int>> { new List<int> { 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0 }, new List<int> { 4, 0, 0, 4 }, new List<int> { 4, 2, 4, 2 } };
            game.board = specialBoard;
            game.UserMove(Moves.Down);
            Assert.IsFalse(game.IsGameWon());
            Assert.IsFalse(game.IsGameOver());
            // The resulting matrix should be the same with the exception of the newly created tiles
            for (int j = 0; j < boardSize; j++)
            {
                Assert.AreEqual(game.board[boardSize-1][j], resultingBoard[boardSize-1][j]);
            }
        }

        [Test]
        public void MovingTheTilesCreatesTwoNewTiles_Test()
        {
            Game game = new Game(boardSize);
            List<List<int>> specialBoard = new List<List<int>> { new List<int> { 0, 0, 0, 4 }, new List<int> { 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 8 }, new List<int> { 0, 0, 0, 0 } };
            game.board = specialBoard;
            game.UserMove(Moves.Right);
            int validTileCount = 0;
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (game.board[i][j] != 0)
                    {
                        validTileCount++;
                    }
                }
            }
            Assert.AreEqual(4, validTileCount);
        }

        [Test]
        public void AlmostGameOverButIsAWin_Test()
        {
            Game game = new Game(boardSize);
            List<List<int>> winningBoard = new List<List<int>> { new List<int> { 16, 32, 4, 8 }, new List<int> { 1024, 8, 16, 32 }, new List<int> { 1024, 64, 32, 64 }, new List<int> { 16, 32, 4, 256 } };
            game.board = winningBoard;
            game.UserMove(Moves.Down);
            Assert.IsTrue(game.IsGameWon());
        }
    }
}