using System.Text;

namespace GildedRose.TestProject
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

            Program.Main([]);
            var output = sb.ToString();
            return Verify(output);
        }
    }
}