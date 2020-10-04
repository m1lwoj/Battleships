# Battleships

Simple Battleships game with Console UI

## Problem

The challenge is to program a simple version of the game Battleships (<a href="https://www.youtube.com/watch?v=q0qpQ8doUp8">video</a>). Create an application to allow a single human player to play a one-sided game of Battleships against ships placed by the computer.
The program should create a 10x10 grid, and place several ships on the grid at random with the following sizes:
1x Battleship (5 squares)
2x Destroyers (4 squares)
The player enters or selects coordinates of the form “A5”, where “A” is the column and “5” is the row, to specify a square to target. Shots result in hits, misses or sinks. The game ends when all ships are sunk.

## Solution

Game UI supports **simple** game mode (one-sided shooting to opponent).


> One of the basic tenets of agile development is that requirements changes aren't just expected, they are welcomed.
> -Martin Fowler


Library is implemented in a way that default game settings can be customized (number of ships, size of the board) and extended with additional game modes (e.g. standard gameplay against opponent) and additional game logic.

![Battleships.ConsoleUI](https://github.com/m1lwoj/Battleships/blob/master/README/BattleshipsUI.JPG)

## Continuous Integration
![.NET Core](https://github.com/m1lwoj/Battleships/workflows/.NET%20Core/badge.svg?branch=master)

## Installation

### From repository

Clone repository first:

```sh
git clone https://github.com/m1lwoj/Battleships.git
cd Battleships
```
#### .NET Core

```sh
cd Battleships.ConsoleUI
dotnet build --configuration Release
dotnet run
```

#### Docker

```sh
docker build -f Battleships.ConsoleUI/Dockerfile -t battleships-consoleui .
docker run -it battleships-consoleui
```

#### Tests (Docker)

```sh
cd Battleships.Tests/
dotnet build
dotnet test
```

#### Tests (.NET Core)
```sh
docker build -f Battleships.Tests/Dockerfile -t battleships-tests .
docker run -it battleships-tests 
```

### Standalone

### Docker 

```sh
docker pull m1losz/battleships-consoleui
docker run -it m1losz/battleships-consoleui 
```

## Decision log

+ Use **.NET Core 3.1** version
    + As the end of September 2020, this is the last stable version of .NET framework.
    + It supports multi-platform by contrast to .NET Framework (I do most of .NET development on Linux)
    + https://stackify.com/net-core-vs-net-framework/
    
+ Use **Docker** 
    + Easier installation.
    + Multi-platform.
    
+ Expose **Battleships.Core** internal implementation to **Battleships.Tests** project
    + I thing it is OK to expose it only for testing project to make tests more flexible and easier to maintain (pragmatism). Internal implementation of Battleships.Core is still hidden from other projects / libraries.
    + Implicit reference between the library and test project (Battleships.Core -> Battleships.Test) adds "hidden coupling" to the solution.
    
+ Used design patterns are not outright design patterns implementation. 
    + For solving simple problems (Factory, Builder, Singleton) I prefer to do implementation *towards* design pattern instead of *to* design pattern. https://industriallogic.com/xp/refactoring/
    
+ No constant convention in naming tests
    + Different conventions suits different test scenarios. I think that each of the test can be considered separately without top-down restrictions if only the name describres correctly testing scenario.

+ Use **Exceptions** for violating game rules
    + I am aware that it is not the most efficient solution in terms of performance.
    + I think each business rules violation has to be covered by an exception
  
## Some random thougts about wider usage and scalability

I wouldn't be myself if I didn't look on the project, requirements and development from wider perspective.

*What if we expand board to size 1000000x1000000?*
- Obviously, my solution with storing game state in an multi-dimensional array wouldn't be optimal (considering memory usage).
- Probably a better idea would be to store only ships and its coordinates in a separate structure. After each move, look through it to check if any ship was hit or not.

*What if we want to make the game online?*
- We would have to provide web client.
- We would have to create simple messages receiver (maybe using websockets, or other fast protocol), with reference to Battleships.Library.

*What if we would like to save online game?*
- We would have to provide some state manager (injected into Game.cs), which could serialize and store game state. (memento pattern?).

*What if we would like to support 1000000 of players?*
- Web clients connect to Load Balancer layer using some fast protocols like websockets.
- Load balancer maps game id with game instance.
- Scaling out. Kubernetes or docker-swarm for horizontal scaling containerized application or use dedicated cloud solution.
- Game instance saves current state of the game and uses it for next shoots (redis or quick memory storage?)
- From time to time game stores the state somewhere externally to recover it in case of any disturbance or failure.

Simple draft below
![](https://github.com/m1lwoj/Battleships/blob/master/README/Battleships-scaling.png)

## License
[MIT](https://choosealicense.com/licenses/mit/)
