namespace GameTask4.RandomGenerator
{
    public class NumberRevealDto
    {
        public int MortyNumber { get; set; }
        public int RickNumber { get; set; }

        public byte[]? SecretKey { get; set; }

        public int MaxExclusive { get; set; }
        public int ResultNumber { get; set; }
    }
}
