namespace GameTask4.Configuration
{
    public static class GameConfigurator
    {
        public static int BoxesCount { get; private set; }
        public static string MortyImplementationPath { get; private set; } = string.Empty;

        private static string MortyClassName { get; set; } = string.Empty;

        public static bool Configure(string[] args, out GameConfigDto? config)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("You need pass boxes count, path to Morty's implementation and Morty's class name as command line arguments!");
                Console.WriteLine("Class name is optional, though, if dll name and class name are the same.");
                Console.WriteLine("Example: Task4.exe 5 C:\\path\\to\\MortyImplementation.dll BestMorty");
                Console.WriteLine("Remind: first argument is boxes count and should be integer number greater than 2.");

                config = null;

                return false;
            }

            bool isBoxesCorrect = int.TryParse(args[0], out int boxesCount);

            if (!isBoxesCorrect)
            {
                Console.WriteLine("Remind: first argument is boxes count and should be integer number greater than 2!");

                config = null;
                return false;
            }

            if (boxesCount <= 2)
            {
                Console.WriteLine("How u suppose to play with 2 or less boxes?! Provide me with boxes count greater than 2!");
                config = null;
                return false;
            }

            MortyImplementationPath = args[1];

            if (args.Length > 2)
            {
                MortyClassName = args[2];
            }
            else
            {
                MortyClassName = Path.GetFileNameWithoutExtension(MortyImplementationPath);
            }

            BoxesCount = boxesCount;

            try
            {
                config = new GameConfigDto()
                {
                    BoxesCount = boxesCount,
                    MortyInstance = MortyPlugin.PlugIn(MortyImplementationPath, MortyClassName)
                };

                return true;
            }
            catch (ArgumentException)
            {
                config = null;
                return false;
            }
        }
    }
}
