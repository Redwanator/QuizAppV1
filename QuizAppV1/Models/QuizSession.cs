namespace QuizAppV1.Models;

internal class QuizSession
{
    private List<Question> Questions { get; } = new();
    internal int CurrentIndex { get; private set; } = 0;
    internal int CorrectAnswers { get; private set; } = 0;

    internal Question? CurrentQuestion =>
        (CurrentIndex >= 0 && CurrentIndex < Questions.Count)
            ? Questions[CurrentIndex]
            : null;

    internal int TotalQuestions => Questions.Count;

    internal QuizSession(IEnumerable<Question> questions)
    {
        Questions.AddRange(questions);
    }

    internal void RegisterAnswer(string answer)
    {
        if (CurrentQuestion is null)
            return;

        if (CurrentQuestion.IsCorrect(answer))
            CorrectAnswers++;
    }

    internal bool MoveNext()
    {
        CurrentIndex++;
        return CurrentIndex < Questions.Count;
    }

    internal void Reset(IEnumerable<Question> questions)
    {
        Questions.Clear();
        Questions.AddRange(questions);
        CurrentIndex = 0;
        CorrectAnswers = 0;
    }

    internal bool IsFinished => CurrentIndex >= Questions.Count;
}
