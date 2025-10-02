namespace GameTask4.UI
{
    public class ConsoleUI : IUserInterface
    {
        public int GetNumber(int maxValue)
        {
            Console.Write("Rick: ");

            while (true)
            {
                try
                {
                    int rickKey = int.Parse(Console.ReadLine() ?? string.Empty);

                    if (rickKey < 0 || rickKey >= maxValue)
                    {
                        Console.WriteLine($"Morty: No-no-no! That will not act like this. Pick a number from 0 to {maxValue - 1} inclusive!");
                        Console.Write("Rick: ");
                        continue;
                    }

                    return rickKey;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Morty: Are u kidding me? That's not a number!");
                    Console.Write("Rick: ");
                }
            }
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine($"Morty: {message}");
        }

        public bool YesNoQuestion(string message)
        {
            Console.WriteLine($"Morty: {message} (y/n)");
            Console.Write($"Rick: ");

            string answer = Console.ReadLine() ?? string.Empty;

            if (answer.ToLower() == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}
