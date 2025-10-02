using GameTask4.UI;
using System.Security.Cryptography;
namespace GameTask4.RandomGenerator
{
    public class FairRandomGenerator : IRandomNumberService
    {
        private const int KeySizeInBytes = 32;
        private IUserInterface _ui;

        public FairRandomGenerator()
        {
            _ui = new ConsoleUI();
        }

        public (int, string, byte[]) GetMortyNum(int n)
        {
            int mortyKey = RandomNumberGenerator.GetInt32(n);
            byte[] secretKey = this.GenerateSecretKey();
            string hmac = HMACGenerator.GenerateHMAC(mortyKey.ToString(), secretKey);

            return (mortyKey, hmac, secretKey);
        }

        public int GetResultNumber(int mortyKey, int rickKey, int n)
        {
            return (mortyKey + rickKey) % n;
        }

        public byte[] GenerateSecretKey()
        {
            byte[] key = RandomNumberGenerator.GetBytes(KeySizeInBytes);
            return key;
        }
    }
}