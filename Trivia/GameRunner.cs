// Refactoring Playlist: https://www.youtube.com/playlist?list=PLv3bW4BDh6I8tg1LSJoB7Ioz64s8Bcufz
// By: ITsLifeOverAll, https://github.com/ITsLifeOverAll

using UglyTrivia;

namespace Trivia;

public class GameRunner
{

    private static bool hasWinner;

    public static void Main(String[] args)
    {
        Game aGame = new Game();

        aGame.add("Chet");
        aGame.add("Pat");
        aGame.add("Sue");

        Random rand = new Random(1234);

        do
        {
            aGame.roll(rand.Next(5) + 1);

            if (rand.Next(9) == 7)
                aGame.wrongAnswer();
            else
                hasWinner = aGame.wasCorrectlyAnswered();
        } while (!hasWinner);
    }
}

