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
        Console.WriteLine(CurrentPlayer.Name + " is the current player");
        Console.WriteLine("They have rolled a " + roll);

        if (CurrentPlayer.InPenaltyBox)
        {
            if (roll % 2 != 0)
            {
                CurrentPlayer.IsGettingOutOfPenaltyBox = true;

                Console.WriteLine(CurrentPlayer.Name + " is getting out of the penalty box");
                CurrentPlayer.Place = CurrentPlayer.Place + roll;
                if (CurrentPlayer.Place > 11)
                    CurrentPlayer.Place = CurrentPlayer.Place - 12;

                Console.WriteLine(CurrentPlayer.Name
                                  + "'s new location is "
                                  + CurrentPlayer.Place);

                askQuestion(CurrentPlayer);
            }
            else
            {
                Console.WriteLine(CurrentPlayer.Name + " is not getting out of the penalty box");
                CurrentPlayer.IsGettingOutOfPenaltyBox = false;
            }

        }
        else
        {

            CurrentPlayer.Place = CurrentPlayer.Place + roll;
            if (CurrentPlayer.Place > 11)
                CurrentPlayer.Place = CurrentPlayer.Place - 12;

            Console.WriteLine(CurrentPlayer.Name
                              + "'s new location is "
                              + CurrentPlayer.Place);

            askQuestion(CurrentPlayer);
        }

    }

    private void askQuestion(Player player)
    {
        QuestionType questionType = GetQuestionType(player.Place);
        Console.WriteLine($"The category is {questionType}");
        string question = _questionService.TakeQuestion(questionType);
        Console.WriteLine(question);
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
                Console.WriteLine("Answer was correct!!!!");
                CurrentPlayer.Purse++;
                Console.WriteLine(CurrentPlayer.Name
                                  + " now has "
                                  + CurrentPlayer.Purse
                                  + " Gold Coins.");

                bool winner = didPlayerWin();
                currentPlayer++;
                if (currentPlayer == _players.Count) currentPlayer = 0;

                return winner;
            }
            else
            {
                currentPlayer++;
                if (currentPlayer == _players.Count) currentPlayer = 0;
                return true;
            }
        }
        else
        {
            Console.WriteLine("Answer was corrent!!!!");
            CurrentPlayer.Purse++;
            Console.WriteLine(CurrentPlayer.Name
                              + " now has "
                              + CurrentPlayer.Purse
                              + " Gold Coins.");

            bool winner = didPlayerWin();
            currentPlayer++;
            if (currentPlayer == _players.Count)
                currentPlayer = 0;

            return winner;
        }
    }

    public bool wrongAnswer()
    {
        Console.WriteLine("Question was incorrectly answered");
        Console.WriteLine(CurrentPlayer.Name + " was sent to the penalty box");
        CurrentPlayer.InPenaltyBox = true;

        currentPlayer++;
        if (currentPlayer == _players.Count)
            currentPlayer = 0;
        return true;
    }


    private bool didPlayerWin()
    {
        return !(CurrentPlayer.Purse == 6);
    }
}