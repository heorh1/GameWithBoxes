namespace GameTask4.RandomGenerator
{
    public interface IRandomNumberService
    {
        public (int, string, byte[]) GetMortyNum(int n);

        public int GetResultNumber(int mortyKey, int rickKey, int n);

        public byte[] GenerateSecretKey();
    }
}