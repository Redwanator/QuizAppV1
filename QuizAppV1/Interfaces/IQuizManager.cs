using QuizAppV1.Models;

namespace QuizAppV1.Interfaces;

internal interface IQuizManager
{
    Question? CurrentQuestion { get; }
    int CurrentIndex { get; }
    int TotalQuestions { get; }
    int CorrectAnswers { get; }
    bool IsFinished { get; }

    bool RegisterAndCheckAnswer(string answer);
    void MoveNext();
    void Reset(IEnumerable<Question> questions);
    int GetScorePercentage();
}
