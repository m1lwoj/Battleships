# Battleships

Simple Battleships game with Console UI

## Problem

The challenge is to program a simple version of the game Battleships (<a href="https://www.youtube.com/watch?v=q0qpQ8doUp8">video</a>). Create an application to allow a single human player to play a one-sided game of Battleships against ships placed by the computer.
The program should create a 10x10 grid, and place several ships on the grid at random with the following sizes:
1x Battleship (5 squares)
2x Destroyers (4 squares)
The player enters or selects coordinates of the form “A5”, where “A” is the column and “5” is the row, to specify a square to target. Shots result in hits, misses or sinks. The game ends when all ships are sunk.

## Solution

Game UI supports only **simple** game mode, which is one-sided shooting to opponent.


> One of the basic tenets of agile development is that requirements changes aren't just expected, they are welcomed.
> -Martin Fowler


Library has been implemented in a way that default game settings can be easily customized (number of ships, size of the board) and extended with additional game modes (like regular gameplay against opponent) and additional game logic.


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
    + There are voices, that this might be considered as breaking OOP Encapsulation paradigm, but in my opinion it is OK to expose it only for testing project to make tests more flexible and easier to maintain (pragmatism). Internal implementation of Battleships.Core is still hidden from other projects / libraries.
    + Reference between the library and test project (Battleships.Core -> Battleships.Test) adds in coupling solution.
    
+ Use design patterns are not outright design patterns implementation. 
    + For solving simple problems (Factory, Builder, Singleton) I prefer to do implementation *towards* design pattern instead of *to* design pattern. https://industriallogic.com/xp/refactoring/
    
+ No constant convention in naming tests
    + Different conventions suits different test scenarios. Each one should be considered separately without any convention restrictions 

+ Use **Exceptions** for violating game rules
    + I am aware that it is not the most efficient solution in terms of performance, but I am not a game developer and don't really know how normally it is handled in game dev.
    + I think each game (or business) rules violation should be covered by an exception, rather than returning complex type with status of action inside.
  
## Some random thougts about wider usage and scalability

I wouldn't be myself if I wouldn't look on the project, requirements and potential project future from wider perspective..

First question I asked to myself was: 
*What if we expand board to size 1000000x1000000?*
- Obviously, my solution with storing game state in an multi-dimensional array wouldn't be optimal (in terms of memory usage).
- Probably a better idea would be to store only ships and their coordinates in a separate structure. After each move, look through it to check if any ship was hit or not.

*What if we would like to make game online?*
- We would have to provide web client.
- We would have to create simple websocket receiver (or other quick protocol), which would use Battleships.Library, do the move and forward result.

*What if we would like to save online game?*
- We would have to provide some state management (injected into Game.cs), which could serialize and store whole Game.cs object).

*What if we would like to scale up solution to support 1000000 of players?*
- Web clients would connect to Load Balancer layer using (web sockets protocol- or some other fast way).
- Load balancer would take the game id and forward connection directly to selected Game Instance
- Game Instance could be scaled horizontally (it could be even serverless), or other cloud / on-prem component that could be scalable by Kubernetes / docker-swarm or others
- Each Game Instance would store state of the game (to use it for next shoots).
- From time to time game storages would be stored somewhere externally in case of lost connection, or one of Game Instance went down


## License
[MIT](https://choosealicense.com/licenses/mit/)
