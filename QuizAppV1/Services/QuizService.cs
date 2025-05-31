using QuizAppV1.Models;

namespace QuizAppV1.Services;

internal class QuizService
{
    private readonly QuizSession _session;

    internal QuizService(IEnumerable<Question> questions)
    {
        _session = new QuizSession(questions);
    }

    internal Question? CurrentQuestion => _session.CurrentQuestion;
    internal int CurrentIndex => _session.CurrentIndex;
    internal int TotalQuestions => _session.TotalQuestions;
    internal int CorrectAnswers => _session.CorrectAnswers;
    internal bool IsFinished => _session.IsFinished;

    internal bool RegisterAndCheckAnswer(string answer)
    {
        _session.RegisterAnswer(answer);
        return _session.CurrentQuestion?.IsCorrect(answer) ?? false;
    }

    internal void MoveNext() => _session.MoveNext();

    internal void Reset(IEnumerable<Question> questions)
    {
        _session.Reset(questions);
    }
}
