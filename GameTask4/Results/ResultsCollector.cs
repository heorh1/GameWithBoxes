using GameTask4.Morty;
namespace GameTask4.Results
{
    public class ResultsCollector
    {
        public int TotalRounds { get; private set; }
        public int RickStayedCount { get; private set; }
        public int RickSwitchedCount => TotalRounds - RickStayedCount;
        public int WinsTotal { get; private set; }
        public int WinsStayed { get; private set; }
        public int WinsSwitched => WinsTotal - WinsStayed;
        public float WinRateStayedActual
        {
            get
            {
                if (RickStayedCount == 0) return 0;
                return (float)WinsStayed / RickStayedCount;
            }
        }
        public float WinRateSwitchedActual
        {
            get
            {
                if (RickSwitchedCount == 0) return 0;
                return (float)WinsSwitched / RickSwitchedCount;
            }
        }


        private BaseMorty morty;

        public ResultsCollector(BaseMorty morty)
        {
            this.morty = morty;
        }

        public void RegisterRound(bool switched, bool win)
        {
            this.TotalRounds++;
            if (!switched) this.RickStayedCount++;
            if (win)
            {
                this.WinsTotal++;
                if (!switched) this.WinsStayed++;
            }
        }

        public ResultsData GetResults()
        {
            return new ResultsData
            {
                TotalRounds = this.TotalRounds,
                RickStayedCount = this.RickStayedCount,
                WinsTotal = this.WinsTotal,
                WinsStayed = this.WinsStayed,
                WinRateStayedActual = this.WinRateStayedActual,
                WinRateSwitchedActual = this.WinRateSwitchedActual,
                WinRateStayedEstimated = this.morty.WinRateStayed,
                WinRateSwitchedEstimated = this.morty.WinRateSwitched
            };
        }
    }
}
