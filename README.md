# Battleships

Simple Battleships game with Console UI

## Problem
The challenge is to program a simple version of the game Battleships (<a href="https://www.youtube.com/watch?v=q0qpQ8doUp8">video</a>). Create an application to allow a single human player to play a one-sided game of Battleships against ships placed by the computer.
The program should create a 10x10 grid, and place several ships on the grid at random with the following sizes:
1x Battleship (5 squares)
2x Destroyers (4 squares)
The player enters or selects coordinates of the form “A5”, where “A” is the column and “5” is the row, to specify a square to target. Shots result in hits, misses or sinks. The game ends when all ships are sunk.

## Continuous Integration
![.NET Core](https://github.com/m1lwoj/Battleships/workflows/.NET%20Core/badge.svg?branch=master)

## Installation

### Docker
```sh
docker pull m1losz/battleships-consoleui
docker run -it m1losz/battleships-consoleui 
```

## Usage


## Decision log

+ Use **.NET Core 3.1** version
    + As the end of September 2020, this is the last stable version of .NET framework.
    + It supports multi-platform by contrast to .NET Framework (I do most of .NET development on Linux)
    + https://stackify.com/net-core-vs-net-framework/
    
+ Expose **Battleships.Core** internal implementation to **Battleships.Tests** project
    + There are voices, that this might be considered as breaking OOP Encapsulation paradigm, but in my opinion it is OK to expose it only for testing project to make tests more flexible and easier to maintain (pragmatism). Internal implementation of Battleships.Core is still hidden from other projects / libraries.

## 

## License
[MIT](https://choosealicense.com/licenses/mit/)
