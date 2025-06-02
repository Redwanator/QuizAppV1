namespace QuizAppV1.Models;

/// <summary>
/// Représente une session de quiz en cours, assurant le suivi de la progression,
/// des réponses correctes, et de l’état général du quiz.
/// </summary>
internal class QuizSession
{
    /// <summary>
    /// Liste interne des questions de la session.
    /// </summary>
    private List<Question> Questions { get; } = new();

    /// <summary>
    /// Index de la question actuellement affichée.
    /// </summary>
    internal int CurrentIndex { get; private set; } = 0;

    /// <summary>
    /// Nombre de réponses correctes données jusqu'à présent.
    /// </summary>
    internal int CorrectAnswers { get; private set; } = 0;

    /// <summary>
    /// Question en cours, ou null si l’index est invalide.
    /// </summary>
    internal Question? CurrentQuestion =>
        (CurrentIndex >= 0 && CurrentIndex < Questions.Count)
            ? Questions[CurrentIndex]
            : null;

    /// <summary>
    /// Nombre total de questions dans la session.
    /// </summary>
    internal int TotalQuestions => Questions.Count;

    /// <summary>
    /// Initialise une nouvelle session de quiz avec une liste de questions.
    /// </summary>
    /// <param name="questions">Les questions à inclure dans la session.</param>
    internal QuizSession(IEnumerable<Question> questions)
    {
        Questions.AddRange(questions);
    }

    /// <summary>
    /// Enregistre une réponse donnée par l’utilisateur, et incrémente le score si elle est correcte.
    /// </summary>
    /// <param name="answer">La réponse fournie par l’utilisateur.</param>
    internal void RegisterAnswer(string answer)
    {
        if (CurrentQuestion is null)
            return;

        if (CurrentQuestion.IsCorrect(answer))
            CorrectAnswers++;
    }

    /// <summary>
    /// Passe à la question suivante.
    /// </summary>
    /// <returns>True s’il reste des questions, False si la session est terminée.</returns>
    internal bool MoveNext()
    {
        CurrentIndex++;
        return CurrentIndex < Questions.Count;
    }

    /// <summary>
    /// Réinitialise la session avec un nouvel ensemble de questions.
    /// </summary>
    /// <param name="questions">Nouvelles questions à charger.</param>
    internal void Reset(IEnumerable<Question> questions)
    {
        Questions.Clear();
        Questions.AddRange(questions);
        CurrentIndex = 0;
        CorrectAnswers = 0;
    }

    /// <summary>
    /// Indique si la session est terminée (toutes les questions ont été parcourues).
    /// </summary>
    internal bool IsFinished => CurrentIndex >= Questions.Count;
}
