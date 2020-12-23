# 2048Game
2048 Game

## Author
Josh Velasquez

### About
2048 is a single - player puzzle game inwhich the objective is to slide numbered tiles on a grid to combine them and create a tile with the number 2048.

### Game Scalability
The game is written to be scalable. A sample driver file called Program.cs is written to show the utilization of the game itselt. With this in mind, only a handful of methods are exposed to the user.  
  
UserMove(Moves move) : Move that the user has chosen to move the board (l - left, u - up, r - right, d - down)  
IsGameWon() -> Boolean : Checks the current game state to see if the game is already won (2048 is achieved)  
IsGameOver() -> Boolean : Checks the game state if the game is already over (no valid moves left and no spaces for new random tiles)  

### Running the game
You can launch the application as a console application. The folder SampleBuild contains a sample build of the application. Simply run the .exe program and the application will launch shortly.

### .NET Core Version
.NET Core 3.1