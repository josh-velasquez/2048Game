using System;
using System.Collections.Generic;

namespace TwentyFortyEight
{
    public class Game
    {
        public List<List<int>> board;

        private const double twoProbability = 0.90;
        private const double fourProbability = 0.10;
        private readonly int boardSize;

        public Game(int boardSize)
        {
            this.boardSize = boardSize;
            board = PopulateBoard();
            InsertNewTilesToBoard();
        }

        public void UserMove(Moves move)
        {
            switch (move)
            {
                case Moves.Left:
                    ShiftBoardLeft();
                    InsertNewTilesToBoard();
                    return;

                case Moves.Up:
                    ShiftBoardUp();
                    InsertNewTilesToBoard();
                    return;

                case Moves.Right:
                    ShiftBoardRight();
                    InsertNewTilesToBoard();
                    return;

                case Moves.Down:
                    ShiftBoardDown();
                    InsertNewTilesToBoard();
                    return;

                case Moves.Invalid:
                    return;
            }
        }

      

        public bool IsGameWon()
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (board[i][j] == 2048)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool IsGameOver()
        {
            bool allSpotsFilled = true;
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (board[i][j] == 0)
                    {
                        return false;
                    }
                }
            }
            bool noMovesAvailable = true;
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (j > 0 && board[i][j] == board[i][j - 1])
                    { // check left adjacent
                        noMovesAvailable = false;
                    }
                    else if (i > 0 && board[i][j] == board[i - 1][j])
                    { // check top adjacent
                        noMovesAvailable = false;
                    }
                    else if (j < boardSize - 1 && board[i][j] == board[i][j + 1])
                    { // check right adjacent
                        noMovesAvailable = false;
                    }
                    else if (i < boardSize - 1 && board[i][j] == board[i + 1][j])
                    { // check left adjacent
                        noMovesAvailable = false;
                    }
                }
            }
            return allSpotsFilled && noMovesAvailable;
        }

        private List<List<int>> PopulateBoard()
        {
            List<List<int>> rows = new List<List<int>>();
            for (int i = 0; i < boardSize; i++)
            {
                List<int> row = new List<int>();
                for (int j = 0; j < boardSize; j++)
                {
                    row.Add(0);
                }
                rows.Add(row);
            }
            return rows;
        }

        private List<List<int>> RotateMatrixRight()
        {
            List<List<int>> tempBoard = PopulateBoard();
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    tempBoard[i][j] = board[j][i];
                }
            }
            for (int i = 0; i < boardSize; i++)
            {
                tempBoard[i].Reverse();
            }
            return tempBoard;
        }

        private List<List<int>> RotateMatrixLeft()
        {
            List<List<int>> tempBoard = PopulateBoard();
            List<List<int>> temp = board;
            for (int i = 0; i < boardSize; i++)
            {
                temp[i].Reverse();
            }
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    tempBoard[i][j] = temp[j][i];
                }
            }
            return tempBoard;
        }

        private void ShiftBoardDown()
        {
            board = RotateMatrixLeft();
            ShiftBoardRight();
            board = RotateMatrixRight();
        }

        private void ShiftBoardUp()
        {
            board = RotateMatrixRight();
            ShiftBoardRight();
            board = RotateMatrixLeft();
        }

        private void ShiftBoardRight()
        {
            for (int i = 0; i < boardSize; i++)
            {
                board[i].RemoveAll(val => val == 0);
                while (board[i].Count != boardSize)
                {
                    board[i].Insert(0, 0);
                }
            }
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = boardSize - 1; j > 0; j--)
                {
                    if (board[i][j] == board[i][j - 1])
                    {
                        board[i][j] = board[i][j] * 2;
                        board[i][j - 1] = 0;
                    }
                }
                board[i].RemoveAll(val => val == 0);
                while (board[i].Count != boardSize)
                {
                    board[i].Insert(0, 0);
                }
            }
        }

        private void ShiftBoardLeft()
        {
            for (int i = 0; i < boardSize; i++)
            {
                board[i].RemoveAll(val => val == 0);
                while (board[i].Count != boardSize)
                {
                    board[i].Add(0);
                }
            }
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize - 1; j++)
                {
                    if (board[i][j] == board[i][j + 1])
                    {
                        board[i][j] = board[i][j] * 2;
                        board[i][j + 1] = 0;
                    }
                }
                board[i].RemoveAll(val => val == 0);
                while (board[i].Count != boardSize)
                {
                    board[i].Add(0);
                }
            }
        }

        private void InsertNewTilesToBoard()
        {
            Tile tile0 = GenerateRandomTile();
            Tile tile1 = GenerateRandomTile();
            if (tile0 != null || tile1 != null)
            {
                board[tile0.xPos][tile0.yPos] = tile0.value;
                board[tile1.xPos][tile1.yPos] = tile1.value;
            }
        }

        private Tile GenerateRandomTile()
        {
            double probability;
            int randTile;
            Tile tile = null;
            Random random = new Random();
            probability = random.NextDouble();
            int tileValue;
            if (probability <= fourProbability)
            {
                tileValue = 4;
            }
            else
            {
                tileValue = 2;
            }
            List<Tile> availableTiles = GetEmptyTiles();
            if (availableTiles.Count == 0)
            {
                return tile;
            }
            randTile = random.Next(availableTiles.Count);
            tile = availableTiles[randTile];
            tile.value = tileValue;
            return tile;
        }

        private List<Tile> GetEmptyTiles()
        {
            List<Tile> tiles = new List<Tile>();
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (board[i][j] == 0)
                    {
                        tiles.Add(new Tile() { value = 0, xPos = i, yPos = j });
                    }
                }
            }
            return tiles;
        }
    }

    internal class Tile
    {
        public int value;
        public int xPos;
        public int yPos;
    }
}