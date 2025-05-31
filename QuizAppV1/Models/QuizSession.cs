namespace QuizAppV1.Models;

public class QuizSession
{
    internal List<Question> Questions { get; }
    public int CurrentIndex { get; private set; } = 0;
    public int CorrectAnswers { get; private set; } = 0;

    internal Question? CurrentQuestion =>
        (CurrentIndex >= 0 && CurrentIndex < Questions.Count)
            ? Questions[CurrentIndex]
            : null;

    public int TotalQuestions => Questions.Count;

    internal QuizSession(IEnumerable<Question> questions)
    {
        Questions = questions.ToList();
    }

    public void RegisterAnswer(string answer)
    {
        if (CurrentQuestion is null)
            return;

        if (CurrentQuestion.IsCorrect(answer))
            CorrectAnswers++;
    }

    public bool MoveNext()
    {
        CurrentIndex++;
        return CurrentIndex < Questions.Count;
    }

    public bool IsFinished => CurrentIndex >= Questions.Count;
}
