namespace GameTask4.Results
{
    public class ResultsData
    {
        public int TotalRounds { get; init; }
        public int RickStayedCount { get; init; }
        public int RickSwitchedCount => TotalRounds - RickStayedCount;
        public int WinsTotal { get; init; }
        public int WinsStayed { get; init; }
        public int WinsSwitched => WinsTotal - WinsStayed;
        public float WinRateStayedActual { get; init; }
        public float WinRateSwitchedActual { get; init; }
        public float WinRateStayedEstimated { get; init; }
        public float WinRateSwitchedEstimated { get; init; }
    }
}
