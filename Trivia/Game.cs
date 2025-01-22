// Refactoring Playlist: https://www.youtube.com/playlist?list=PLv3bW4BDh6I8tg1LSJoB7Ioz64s8Bcufz
// By: ITsLifeOverAll, https://github.com/ITsLifeOverAll

using Trivia;

namespace UglyTrivia;
public class Game
{

    private readonly List<Player> _players;
    private readonly QuestionService _questionService;

    private Player CurrentPlayer => _players[currentPlayer];

    int currentPlayer = 0;

    public Game()
    {
        var questionTypes = Enum.GetValues(typeof(QuestionType)).Cast<QuestionType>();
        _questionService = new QuestionService(50, questionTypes);
        _players = [];
    }

    public bool isPlayable()
    {
        return (howManyPlayers() >= 2);
    }

    public bool add(String playerName)
    {
        _players.Add(new Player { Name = playerName });

        Console.WriteLine(playerName + " was added");
        Console.WriteLine("They are player number " + _players.Count);
        return true;
    }

    public int howManyPlayers()
    {
        return _players.Count;
    }

    public void roll(int roll)
    {
        Player player = CurrentPlayer;
        Console.WriteLine(player.Name + " is the current player");
        Console.WriteLine("They have rolled a " + roll);

        bool isOutOfPenaltyBox = !player.InPenaltyBox || TryReleasePlayer(player, roll);
        if (isOutOfPenaltyBox)
        {
            MovePlayer(player, roll);
            askQuestion(player);
        }
    }

    private static bool TryReleasePlayer(Player player, int roll)
    {
        bool canRelease = roll % 2 != 0;
        Console.WriteLine($"{player.Name} {(canRelease ? "is" : "is not")} getting out of the penalty box");
        player.IsGettingOutOfPenaltyBox = canRelease;
        return canRelease;
    }

    private void askQuestion(Player player)
    {
        QuestionType questionType = GetQuestionType(player.Place);
        Console.WriteLine($"The category is {questionType}");
        string question = _questionService.TakeQuestion(questionType);
        Console.WriteLine(question);
    }

    private static void MovePlayer(Player player, int roll)
    {
        player.Place = (player.Place + roll) % 12;
        Console.WriteLine($"{player.Name}'s new location is {player.Place}");
    }

    private static QuestionType GetQuestionType(int place) =>
        place switch
        {
            0 or 4 or 8 => QuestionType.Pop,
            1 or 5 or 9 => QuestionType.Science,
            2 or 6 or 10 => QuestionType.Sports,
            _ => QuestionType.Rock
        };

    public bool wasCorrectlyAnswered()
    {
        if (CurrentPlayer.InPenaltyBox)
        {
            if (CurrentPlayer.IsGettingOutOfPenaltyBox)
            {
                CorrectAnswer(CurrentPlayer, "correct");
                bool winner = didPlayerWin();
                NextPlayer();
                return winner;
            }
            else
            {
                NextPlayer();
                return true;
            }
        }
        else
        {
            CorrectAnswer(CurrentPlayer, "corrent");
            bool winner = didPlayerWin();
            NextPlayer();
            return winner;
        }
    }

    public bool wrongAnswer()
    {
        Console.WriteLine("Question was incorrectly answered");
        Console.WriteLine(CurrentPlayer.Name + " was sent to the penalty box");
        CurrentPlayer.InPenaltyBox = true;

        NextPlayer();
        return true;
    }

    private static void CorrectAnswer(Player player, string msg)
    {
        Console.WriteLine($"Answer was {msg}!!!!");
        player.Purse++;
        Console.WriteLine($"{player.Name} now has {player.Purse} Gold Coins.");
    }

    private void NextPlayer()
    {
        currentPlayer = (currentPlayer + 1) % _players.Count;
    }

    private bool didPlayerWin()
    {
        return !(CurrentPlayer.Purse == 6);
    }
}