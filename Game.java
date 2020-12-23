import java.util.Scanner;

public class Game {
    final static int boardSize = 4;
    static TwentyFortyEight board;

    public static void main(String[] args) {
        printInstructions();
        board = new TwentyFortyEight(boardSize);
        startGame(board);
    }

    private static void printInstructions() {
        System.out.println(
                "2048 is a single-player puzzle game inwhich the objective is to slide numbered tiles on a grid to combine them and create a tile with the number 2048. Type exit to exit the game.");
    }

    public static void startGame(TwentyFortyEight board) {
        Scanner userInput = new Scanner(System.in);
        String userMove = "";
        while (true) {
            printBoard();
            System.out.print("\nWhich direction would you like to move? (l = left, u = up, r = right, d = down): ");
            userMove = userInput.nextLine();

            Moves move = convertUserMove(userMove);
            if (move != Moves.Invalid) {
                board.userMove(move);
                if (board.isGameWon()) {
                    printBoard();
                    System.out.println("You won! :). Congratulations!");
                    userInput.close();
                    return;
                } else if (board.isGameOver()) {
                    printBoard();
                    System.out.println("You lost! :(. Better luck next time!");
                    userInput.close();
                    return;
                }
            } else if (userMove.equals("exit")) {
                System.out.println("Thanks for playing!");
                userInput.close();
                return;
            } else {
                System.out.println("Invalid move! Please try again.");
            }
        }
    }

    private static void printBoard() {
        System.out.println("\n#######################################################################");
        for (int i = 0; i < boardSize; i++) {
            for (int j = 0; j < boardSize; j++) {
                System.out.print("\t" + board.board[i][j] + "\t");
            }
            System.out.println();
        }
        System.out.println("#######################################################################\n\n");
    }

    private static Moves convertUserMove(String move) {
        String m = move.toLowerCase();
        if (m.equals("l")) {
            return Moves.Left;
        } else if (m.equals("u")) {
            return Moves.Up;
        } else if (m.equals("r")) {
            return Moves.Right;
        } else if (m.equals("d")) {
            return Moves.Down;
        } else {
            return Moves.Invalid;
        }
    }
}
