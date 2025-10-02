using GameTask4.Morty;
namespace GameTask4.Configuration
{
    public class GameConfigDto
    {
        public required BaseMorty MortyInstance { get; set; }
        public int BoxesCount { get; set; }
    }
}
