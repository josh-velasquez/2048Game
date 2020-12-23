import java.util.ArrayList;
import java.util.Random;

public class TwentyFortyEight {
    private static Random rand = new Random();
    public int[][] board;
    final static double twoProbability = 0.90;
    final static double fourProbability = 0.10;
    private int boardSize;

    // private int[][] sampleBoard = { { 2, 2, 4, 4 }, { 2, 4, 8, 8 }, { 4, 4, 2, 4
    // }, { 2, 2, 2, 2 } };
    // private int[][] winningBoard = { { 2, 2, 4, 4 }, { 2, 4, 8, 8 }, { 1024, 4,
    // 2, 4 }, { 1024, 2, 2, 2 } };
    // private int[][] losingBoard = { { 2, 4, 8, 4 }, { 4, 16, 32, 8 }, { 8, 2, 8,
    // 2 }, { 16, 8, 4, 4 } };
    // private int[][] specialBoard0 = { { 4, 2, 4, 8 }, { 2, 8, 16, 32 }, { 2, 16,
    // 32, 64 }, { 4, 2, 4, 256 } };
    private int[][] specialBoard1 = { { 4, 0, 0, 4 }, { 2, 0, 2, 0 }, { 0, 2, 0, 2 }, { 2, 0, 2, 0 } };

    public TwentyFortyEight(int boardSize) {
        this.boardSize = boardSize;
        // board = new int[boardSize][boardSize];
        // insertNewTilesToBoard();

        // DEBUGGING
        board = specialBoard1;
    }

    public void userMove(Moves move) {
        switch (move) {
            case Left:
                shiftBoardLeft();
                insertNewTilesToBoard();
                return;
            case Up:
                shiftBoardUp();
                insertNewTilesToBoard();
                return;
            case Right:
                shiftBoardRight();
                insertNewTilesToBoard();
                return;
            case Down:
                shiftBoardDown();
                insertNewTilesToBoard();
                return;
            case Invalid:
                return;
        }
    }

    public boolean isGameWon() {
        for (int i = 0; i < boardSize; i++) {
            for (int j = 0; j < boardSize; j++) {
                if (board[i][j] == 2048) {
                    return true;
                }
            }
        }
        return false;
    }

    public boolean isGameOver() {
        boolean allSpotsFilled = true;
        for (int i = 0; i < boardSize; i++) {
            for (int j = 0; j < boardSize; j++) {
                if (board[i][j] == 0) {
                    return false;
                }
            }
        }
        boolean noMovesAvailable = true;
        for (int i = 0; i < boardSize; i++) {
            for (int j = 0; j < boardSize; j++) {

                if (j > 0 && board[i][j] == board[i][j - 1]) { // check left adjacent
                    noMovesAvailable = false;
                } else if (i > 0 && board[i][j] == board[i - 1][j]) { // check top adjacent
                    noMovesAvailable = false;
                } else if (j < boardSize - 1 && board[i][j] == board[i][j + 1]) { // check right adjacent
                    noMovesAvailable = false;
                } else if (i < boardSize - 1 && board[i][j] == board[i + 1][j]) { // check left adjacent
                    noMovesAvailable = false;
                }
            }
        }
        return allSpotsFilled && noMovesAvailable;
    }

    private void insertNewTilesToBoard() {
        Tile tile0 = generateRandomTile();
        Tile tile1 = generateRandomTile();
        if (tile0.value != 0 || tile1.value != 0) {
            board[tile0.xPos][tile0.yPos] = tile0.value;
            board[tile1.xPos][tile1.yPos] = tile1.value;
        }
    }

    private Tile generateRandomTile() {
        double probability;
        int randTile;
        Tile tile = new Tile(0, 0, 0);
        probability = rand.nextDouble();
        int tileValue = 0;
        if (probability < fourProbability) {
            tileValue = 4;
        } else {
            tileValue = 2;
        }
        ArrayList<Tile> availableTiles = getEmptyTiles();
        if (availableTiles.size() == 0) {
            return tile;
        }
        randTile = rand.nextInt(availableTiles.size());
        tile = availableTiles.get(randTile);
        tile.value = tileValue;
        return tile;
    }

    private ArrayList<Tile> getEmptyTiles() {
        ArrayList<Tile> tiles = new ArrayList<Tile>();
        for (int i = 0; i < boardSize; i++) {
            for (int j = 0; j < boardSize; j++) {
                if (board[i][j] == 0) {
                    tiles.add(new Tile(0, i, j));
                }
            }
        }
        return tiles;
    }

    /// IF 0 is BETWEEN IT FAILS

    private void shiftBoardLeft() {
        for (int i = 0; i < boardSize; i++) {
            for (int j = 0; j < boardSize - 1; j++) {
                if (board[i][j] == board[i][j + 1]) {
                    



                    board[i][j + 1] = 0;
                    board[i][j] = board[i][j] * 2;
                    shiftRowLeft(i);
                }
                if (board[i][j] == 0) {
                    shiftRowLeft(i);
                }
            }
        }
    }

    private void shiftRowLeft(int row) {
        for (int j = 0; j < boardSize - 1; j++) {
            if (board[row][j] == 0) {
                board[row][j] = board[row][j + 1];
                board[row][j + 1] = 0;
            }
        }
    }

    private void shiftBoardRight() {
        for (int i = 0; i < boardSize; i++) {
            for (int j = boardSize - 1; j > 0; j--) {
                if (board[i][j] == board[i][j - 1]) {
                    board[i][j - 1] = 0;
                    board[i][j] = board[i][j] * 2;
                    shiftRowRight(i);
                }
                if (board[i][j] == 0) {
                    shiftRowRight(i);
                }
            }
        }
    }

    private void shiftRowRight(int row) {
        for (int j = boardSize - 1; j > 0; j--) {
            if (board[row][j] == 0) {
                board[row][j] = board[row][j - 1];
                board[row][j - 1] = 0;
            }
        }
    }

    private void shiftBoardUp() {
        for (int i = 0; i < boardSize - 1; i++) {
            for (int j = 0; j < boardSize; j++) {
                if (board[i][j] == board[i + 1][j]) {
                    board[i + 1][j] = 0;
                    board[i][j] = board[i][j] * 2;
                    shiftColUp(j);
                }
                if (board[i][j] == 0) {
                    shiftColUp(j);
                }
            }
        }
    }

    private void shiftColUp(int col) {
        for (int i = 0; i < boardSize - 1; i++) {
            if (board[i][col] == 0) {
                board[i][col] = board[i + 1][col];
                board[i + 1][col] = 0;
            }
        }
    }

    private void shiftBoardDown() {
        for (int i = boardSize - 1; i > 0; i--) {
            for (int j = 0; j < boardSize; j++) {
                if (board[i][j] == board[i - 1][j]) {
                    board[i - 1][j] = 0;
                    board[i][j] = board[i][j] * 2;
                    shiftColDown(j);
                }
                if (board[i][j] == 0) {
                    shiftColDown(j);
                }
            }
        }
    }

    private void shiftColDown(int col) {
        for (int i = boardSize - 1; i > 0; i--) {
            if (board[i][col] == 0) {
                board[i][col] = board[i - 1][col];
                board[i - 1][col] = 0;
            }
        }
    }

    class Tile {
        int value;
        int xPos;
        int yPos;

        public Tile(int val, int x, int y) {
            value = val;
            xPos = x;
            yPos = y;
        }
    }
}
