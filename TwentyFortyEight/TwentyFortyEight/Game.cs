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

        /// <summary>
        /// Constructor that initializes the game board
        /// </summary>
        /// <param name="boardSize"></param>
        public Game(int boardSize)
        {
            this.boardSize = boardSize;
            board = PopulateBoard();
            InsertNewTileOnBoard();
            InsertNewTileOnBoard();
        }

        /// <summary>
        /// Exposed method that takes in user move and moves the board accordingly. Pass in a valid move to shift the tiles on the board.
        /// </summary>
        /// <param name="move"></param>
        public void UserMove(Moves move)
        {
            if (!isValidMove(move))
            {
                return;
            }
            switch (move)
            {
                case Moves.Left:
                    ShiftBoardLeft();
                    InsertNewTileOnBoard();
                    return;

                case Moves.Up:
                    ShiftBoardUp();
                    InsertNewTileOnBoard();
                    return;

                case Moves.Right:
                    ShiftBoardRight();
                    InsertNewTileOnBoard();
                    return;

                case Moves.Down:
                    ShiftBoardDown();
                    InsertNewTileOnBoard();
                    return;
            }
        }

        /// <summary>
        /// Checks if the game is won already (i.e. the value 2048 is achieved).
        /// </summary>
        /// <returns>True if the game is won; false otherwise.</returns>
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

        /// <summary>
        /// Checks if the game state is game over. Checks if all spots are filled (can't generate new random tiles) and if there are no moves available.
        /// </summary>
        /// <returns>True if the game is over; false otherwise.</returns>
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

        /// <summary>
        /// Populates the board with 0 values initially.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Rotates the board to the right (used for shifting tiles). To rotate to the right
        /// first the matrix is transposed and then each rows were reversed.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Rotates the board to the left (used for shifting tiles). To rotate to thr left,
        /// first the matrix is reversed every row and then transposed.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Shifts the board downwards (rotated left first, then shifted to the right, then rotated back to the right)
        /// </summary>
        private void ShiftBoardDown()
        {
            board = RotateMatrixLeft();
            ShiftBoardRight();
            board = RotateMatrixRight();
        }

        /// <summary>
        /// Shifts the board upwards (rorated right first, then shifted to the right, then rotated back to the left)
        /// </summary>
        private void ShiftBoardUp()
        {
            board = RotateMatrixRight();
            ShiftBoardRight();
            board = RotateMatrixLeft();
        }

        /// <summary>
        /// Shifts the board to the right. First removes all the 0's from the array, then merges
        /// similar adjacent values together on the right.
        /// </summary>
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

        /// <summary>
        /// Corner case that checks if the move is valid. Ex. if the very first row is populated ONLY and a move up is used, this should be registered as
        /// an invalid move and no new tiles should be generated.
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        private bool isValidMove(Moves move)
        {
            switch (move)
            {
                case Moves.Left:
                    return !isEmpty(0, boardSize, 1, boardSize);

                case Moves.Right:
                    return !isEmpty(0, boardSize, 0, boardSize - 1);

                case Moves.Down:
                    return !isEmpty(0, boardSize - 1, 0, boardSize);

                case Moves.Up:
                    return !isEmpty(1, boardSize, 0, boardSize);
            }
            return false;
        }

        private bool isEmpty(int startRow, int endRow, int startCol, int endCol)
        {
            for (int i = startRow; i < endRow; i++)
            {
                for (int j = startCol; j < endCol; j++)
                {
                    if (board[i][j] != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Shifts the board to the left. Removes all the 0's in the row first, then merges similar
        /// adjacent values together on the right.
        /// </summary>
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

        /// <summary>
        /// Inserts randomly generated tiles to the board. This function gets called when a new board is initialized,
        /// or when the user has made a move and available spaces can be occupied.
        /// </summary>
        private void InsertNewTileOnBoard()
        {
            Tile tile = GenerateRandomTile();
            if (tile != null)
            {
                board[tile.xPos][tile.yPos] = tile.value;
            }
        }

        /// <summary>
        /// Generates a random tile based on the probability chosen. Gets all the available empty spots on the board
        /// and a random spot is chosen randomly from the open spaces.
        /// </summary>
        /// <returns>Returns the randomly generated tile</returns>
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

        /// <summary>
        /// Gets all the empty spaces of the board for new tiles to populate.
        /// </summary>
        /// <returns>List of available spots for new tiles</returns>
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

    /// <summary>
    /// Tile class for generating random tiles
    /// </summary>
    internal class Tile
    {
        public int value;
        public int xPos;
        public int yPos;
    }
}