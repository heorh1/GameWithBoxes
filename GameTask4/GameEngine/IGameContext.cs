namespace GameTask4.GameEngine
{
    public interface IGameContext
    {
        public void StartGame();
        public void SwitchingBox(int alternativeBox);
        public int RequestNumber(int n);
        public void EndRound();
    }
}
