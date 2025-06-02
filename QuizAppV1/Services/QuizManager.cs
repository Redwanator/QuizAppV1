using QuizAppV1.Interfaces;
using QuizAppV1.Models;

namespace QuizAppV1.Services;

internal class QuizManager : IQuizManager
{
    private readonly QuizSession _session;
    private readonly IAnswerChecker _checker;
    private readonly IScoreCalculator _calculator;

    internal QuizManager(
        IEnumerable<Question> questions,
        IAnswerChecker checker,
        IScoreCalculator calculator
        )
    {
        _session = new QuizSession(questions);
        _checker = checker;
        _calculator = calculator;
    }

    public Question? CurrentQuestion => _session.CurrentQuestion;
    public int CurrentIndex => _session.CurrentIndex;
    public int TotalQuestions => _session.TotalQuestions;
    public int CorrectAnswers => _session.CorrectAnswers;
    public bool IsFinished => _session.IsFinished;

    public bool RegisterAndCheckAnswer(string answer)
    {
        if (_session.CurrentQuestion is null) return false;

        bool isCorrect = _checker.IsCorrect(answer, _session.CurrentQuestion);
        _session.RegisterAnswer(answer);
        return isCorrect;
    }

    public int GetScorePercentage()
    {
        return _calculator.ComputeScore(_session.CorrectAnswers, _session.TotalQuestions);
    }

    public void MoveNext() => _session.MoveNext();

    public void Reset(IEnumerable<Question> questions)
    {
        _session.Reset(questions);
    }
}
