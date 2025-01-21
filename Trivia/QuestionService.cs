namespace Trivia
{
    public class QuestionService
    {
        private readonly Dictionary<QuestionType, LinkedList<string>> _questions;

        public QuestionService(int cnt, IEnumerable<QuestionType> questionTypes)
        {
            _questions = [];
            foreach (var questionType in questionTypes)
            {
                var questionList = new LinkedList<string>();
                foreach (int i in Enumerable.Range(0, cnt))
                    questionList.AddLast($"{questionType} Question {i}");
                _questions.Add(questionType, questionList);
            }
        }

        public string TakeQuestion(QuestionType questionType)
        {
            var questionList = _questions[questionType];
            var result = questionList.First();
            questionList.RemoveFirst();
            return result;
        }
    }

    public enum QuestionType
    {
        Pop,
        Science,
        Sports,
        Rock
    }
}
