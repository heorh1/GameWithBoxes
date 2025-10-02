using GameTask4.Configuration;
using GameTask4.RandomGenerator;
using GameTask4.UI;
using GameTask4.GameEngine;
using GameTask4.Results;

if (!GameConfigurator.Configure(args, out GameConfigDto? config)) return;

if (config == null) return;

IRandomNumberService randomGenerator = new FairRandomGenerator();
IUserInterface consoleUI = new ConsoleUI();
IResultsDisplayer statDisplay = new ConsoleTableResults();

GameEngine game = new GameEngine(config, randomGenerator, consoleUI, statDisplay);

config.MortyInstance.Init(game, consoleUI, GameConfigurator.BoxesCount);

game.StartGame();

Console.Write("Press any key to close..."); Console.ReadKey();


