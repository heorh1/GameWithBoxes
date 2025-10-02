namespace GameTask4.UI
{
    public interface IUserInterface
    {
        public int GetNumber(int maxValue);
        public void DisplayMessage(string message);

        public bool YesNoQuestion(string message);

        public void Clear();
    }
}
