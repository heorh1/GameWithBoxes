using GameTask4.Morty;
using System.Reflection;

namespace GameTask4.Configuration
{
    public static class MortyPlugin
    {
        public static BaseMorty PlugIn(string path, string className)
        {
            try
            {
                var asm = Assembly.LoadFrom(path);

                string fullClassName = className + "." + className;

                var type = asm.GetType(fullClassName);

                if (type == null || !typeof(BaseMorty).IsAssignableFrom(type))
                {
                    Console.WriteLine($"Uhm, well.. I can't find Morty {fullClassName} or it does not inherit from BaseMorty class.");
                    throw new ArgumentException("Type not found or does not inherit from BaseMorty");
                }

                return Activator.CreateInstance(type) as BaseMorty
                       ?? throw new ArgumentException("Could not create instance of Morty");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Uhm, does this Morty even exsit? Check ur file path and class name.");
                throw new ArgumentException("File not found");
            }
        }
    }
}
