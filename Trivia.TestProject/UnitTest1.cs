using System.Text;

namespace Trivia.TestProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public Task Test1()
        {
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            Console.SetOut(sw);

            GameRunner.Main([]);
            var output = sb.ToString();
            return Verify(output);
        }
    }
}