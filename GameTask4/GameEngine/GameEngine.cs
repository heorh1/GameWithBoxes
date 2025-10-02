using GameTask4.Configuration;
using GameTask4.RandomGenerator;
using GameTask4.Results;
using GameTask4.UI;

namespace GameTask4.GameEngine
{
    public class GameEngine : IGameContext
    {
        private IRandomNumberService randomNumberService;
        private IUserInterface ui;
        private IResultsDisplayer statisticsDisplayer;
        private GameConfigDto config;
        private ResultsCollector statistics;

        private List<NumberRevealDto> roundGenerations = new List<NumberRevealDto>();

        private int rickInitialGuess;
        private int rickFinalGuess;
        private int boxWithGun;
        private bool isSwitching;
        private bool isRickWinner;

        public GameEngine(GameConfigDto config, IRandomNumberService randomNumberService, IUserInterface ui, IResultsDisplayer statDisplay)
        {
            this.randomNumberService = randomNumberService;
            this.ui = ui;
            this.config = config;
            this.statisticsDisplayer = statDisplay;
            this.statistics = new ResultsCollector(config.MortyInstance);
        }

        public void StartGame()
        {
            this.ui.DisplayMessage($"{this.config.MortyInstance.Messages.Welcome} I hide ur GUN in one of these {this.config.BoxesCount} boxes.");

            this.boxWithGun = this.RequestNumber(this.config.BoxesCount);
            this.config.MortyInstance.HideGunInBox(this.boxWithGun);

            this.ui.DisplayMessage($"{this.config.MortyInstance.Messages.AskForGuess} Your guess [0; {this.config.BoxesCount})?");

            rickInitialGuess = this.ui.GetNumber(this.config.BoxesCount);

            this.config.MortyInstance.SetRickGuess(rickInitialGuess);
            this.config.MortyInstance.PlayRound();
        }

        public void SwitchingBox(int alternativeBox)
        {
            this.isSwitching = this.ui.YesNoQuestion(this.config.MortyInstance.Messages.SwitchingQuestion);

            if (this.isSwitching)
            {
                this.ui.DisplayMessage($"Ok, Rick. U switched ur box to {alternativeBox}");
                this.rickFinalGuess = alternativeBox;
            }
            else
            {
                this.ui.DisplayMessage($"Ok, Rick. U kept ur box {rickInitialGuess}");
                this.rickFinalGuess = rickInitialGuess;
            }
        }

        public int RequestNumber(int n)
        {
            (int mortyKey, string hmac, byte[] secretKey) = this.randomNumberService.GetMortyNum(GameConfigurator.BoxesCount);

            this.ui.DisplayMessage($"HMAC{this.roundGenerations.Count + 1}={hmac}");
            this.ui.DisplayMessage($"Enter your number [0; {n}), Rick. Oh, and don't blame me in cheating, though!");
            int rickKey = this.ui.GetNumber(n);

            int result = this.randomNumberService.GetResultNumber(mortyKey, rickKey, n);

            roundGenerations.Add(new NumberRevealDto
            {
                MortyNumber = mortyKey,
                RickNumber = rickKey,
                SecretKey = secretKey,
                ResultNumber = result,
                MaxExclusive = n
            });

            return result;
        }

        public void EndRound()
        {
            this.Reveal();
            this.ComputeResults();

            this.statistics.RegisterRound(this.isSwitching, this.isRickWinner);

            bool playAgain = this.ui.YesNoQuestion(this.config.MortyInstance.Messages.PlayAgainQuestion);

            if (playAgain)
            {
                this.ui.DisplayMessage(this.config.MortyInstance.Messages.Restart);

                Console.ReadKey();

                this.RestartRound();
            }
            else
            {
                this.ui.DisplayMessage(this.config.MortyInstance.Messages.Goodbye);
                this.statisticsDisplayer.DisplayResults(this.statistics.GetResults());
            }
        }

        private void Reveal()
        {
            for (int i = 1; i <= this.roundGenerations.Count; i++)
            {
                var round = roundGenerations[i - 1];

                string numEnding = i == 1 ? "st" : i == 2 ? "nd" : i == 3 ? "rd" : "th";

                this.ui.DisplayMessage($"Hm, my {i}{numEnding} random number is {round.MortyNumber}..");
                this.ui.DisplayMessage($"KEY{i}={BitConverter.ToString(round.SecretKey!).Replace("-", "").ToLower()}");
                this.ui.DisplayMessage($"So, the first fair number is ({round.MortyNumber} + {round.RickNumber}) % {round.MaxExclusive} = {round.ResultNumber}..");
            }
        }

        private void ComputeResults()
        {
            this.ui.DisplayMessage($"Your portal gun wa-a-as... in the box {this.boxWithGun}.");
            this.ui.DisplayMessage($"Your final guess was {this.rickFinalGuess}.");

            if (this.rickFinalGuess == this.boxWithGun)
            {
                isRickWinner = true;
                this.ui.DisplayMessage(this.config.MortyInstance.Messages.RickWinner);
            }
            else
            {
                this.ui.DisplayMessage(this.config.MortyInstance.Messages.RickLoser);
            }
        }

        private void RestartRound()
        {
            this.ui!.Clear();
            this.roundGenerations.Clear();
            this.isSwitching = false;
            this.isRickWinner = false;
            this.rickInitialGuess = -1;
            this.rickFinalGuess = -1;
            this.boxWithGun = -1;

            this.StartGame();
        }
    }
}
