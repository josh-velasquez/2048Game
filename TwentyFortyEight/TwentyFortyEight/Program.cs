using System;

namespace TwentyFortyEight
{
    internal class Program
    {
        private const int boardSize = 4;
        private static Game game;

        private static void Main(string[] args)
        {
            PrintInstructions();
            game = new Game(boardSize);
            StartGame();
        }

        private static void StartGame()
        {
            while (true)
            {
                PrintBoard();
                Console.Write("\nWhich direction would you like to move? (l = left, u = up, r = right, d = down): ");
                string userInput = Console.ReadLine();

                Moves move = convertUserMove(userInput);
                if (move != Moves.Invalid)
                {
                    game.UserMove(move);
                    if (game.IsGameWon())
                    {
                        PrintBoard();
                        Console.WriteLine("You won! :). Congratulations!");
                        return;
                    }
                    else if (game.IsGameOver())
                    {
                        PrintBoard();
                        Console.WriteLine("You lost! :(. Better luck next time!");
                        return;
                    }
                }
                else if (userInput == ("exit"))
                {
                    Console.WriteLine("Thanks for playing!");
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid move! Please try again.");
                }
            }
        }

        private static Moves convertUserMove(String move)
        {
            string m = move.ToLower();
            if (m == "l")
            {
                return Moves.Left;
            }
            else if (m == ("u"))
            {
                return Moves.Up;
            }
            else if (m == ("r"))
            {
                return Moves.Right;
            }
            else if (m == ("d"))
            {
                return Moves.Down;
            }
            else
            {
                return Moves.Invalid;
            }
        }

        private static void PrintBoard()
        {
            Console.WriteLine("\n#######################################################################");
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (game.board[i][j] == 0)
                    {
                        Console.Write("\t0\t");
                    }
                    else
                    {
                        Console.Write("\t" + game.board[i][j] + "\t");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("#######################################################################\n\n");
        }

        private static void PrintInstructions()
        {
            Console.WriteLine("2048 is a single - player puzzle game inwhich the objective is to slide numbered tiles on a grid to combine them and create a tile with the number 2048.Type exit to exit the game.");
        }
    }
}