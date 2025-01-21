namespace Trivia
{
    public class Player
    {
        public string Name { get; set; } = string.Empty;
        public int Place { get; set; }
        public int Purse { get; set; }
        public bool InPenaltyBox { get; set; }
        public bool IsGettingOutOfPenaltyBox { get; set; }
    }
}
