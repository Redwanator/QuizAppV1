using QuizAppV1.Interfaces;
using QuizAppV1.Models;

namespace QuizAppV1.Services;

/// <summary>
/// Gère le déroulement d’un quiz en cours, en orchestrant la session, 
/// la validation des réponses et le calcul du score final.
/// </summary>
internal class QuizManager : IQuizManager
{
    private readonly QuizSession _session;
    private readonly IAnswerChecker _checker;
    private readonly IScoreCalculator _calculator;

    /// <summary>
    /// Initialise un nouveau gestionnaire de quiz avec les questions,
    /// un validateur de réponse et un calculateur de score.
    /// </summary>
    /// <param name="questions">Questions à utiliser pour la session de quiz.</param>
    /// <param name="checker">Service de validation de réponse utilisateur.</param>
    /// <param name="calculator">Service de calcul du score final.</param>
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

    /// <summary>
    /// Question actuellement posée à l’utilisateur.
    /// </summary>
    public Question? CurrentQuestion => _session.CurrentQuestion;

    /// <summary>
    /// Index de la question en cours.
    /// </summary>
    public int CurrentIndex => _session.CurrentIndex;

    /// <summary>
    /// Nombre total de questions dans le quiz.
    /// </summary>
    public int TotalQuestions => _session.TotalQuestions;

    /// <summary>
    /// Nombre de bonnes réponses enregistrées jusqu’à présent.
    /// </summary>
    public int CorrectAnswers => _session.CorrectAnswers;

    /// <summary>
    /// Indique si toutes les questions ont été posées.
    /// </summary>
    public bool IsFinished => _session.IsFinished;

    /// <summary>
    /// Enregistre la réponse donnée par l’utilisateur et retourne si elle est correcte.
    /// </summary>
    /// <param name="answer">Réponse sélectionnée.</param>
    /// <returns>True si la réponse est correcte, False sinon.</returns>
    public bool RegisterAndCheckAnswer(string answer)
    {
        if (_session.CurrentQuestion is null) return false;

        bool isCorrect = _checker.IsCorrect(answer, _session.CurrentQuestion);
        _session.RegisterAnswer(answer);
        return isCorrect;
    }

    /// <summary>
    /// Calcule le score actuel en pourcentage à partir des réponses correctes.
    /// </summary>
    /// <returns>Un entier représentant le score en pourcentage.</returns>
    public int GetScorePercentage()
    {
        return _calculator.ComputeScore(_session.CorrectAnswers, _session.TotalQuestions);
    }

    /// <summary>
    /// Passe à la question suivante du quiz.
    /// </summary>
    public void MoveNext() => _session.MoveNext();

    /// <summary>
    /// Réinitialise le quiz avec une nouvelle série de questions.
    /// </summary>
    /// <param name="questions">Questions à utiliser pour une nouvelle session.</param>
    public void Reset(IEnumerable<Question> questions)
    {
        _session.Reset(questions);
    }
}
