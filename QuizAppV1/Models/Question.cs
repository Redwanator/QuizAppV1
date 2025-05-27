namespace QuizAppV1.Models;

/// <summary>
/// Représente une question à choix multiple avec ses propositions et la bonne réponse.
/// </summary>
/// <param name="text">Intitulé de la question.</param>
/// <param name="options">Liste des propositions de réponse (au moins 3 attendues).</param>
/// <param name="correctAnswerIndex">Index correspondant à la bonne réponse dans la liste des options (commence à 0).</param>
internal class Question(string text, IEnumerable<string> options, string correctAnswer)
{
    /// <summary>
    /// Intitulé de la question posée.
    /// </summary>
    public string Text { get; init; } = text;

    /// <summary>
    /// Liste des propositions de réponse.
    /// </summary>
    public IEnumerable<string> Options { get; init; } = options;

    /// <summary>
    /// Index de la bonne réponse dans la liste des options (commence à 0).
    /// </summary>
    public string CorrectAnswer { get; init; } = correctAnswer;

    /// <summary>
    /// Vérifie si l'index fourni correspond à la bonne réponse.
    /// </summary>
    /// <param name="answerIndex">Index de la réponse choisie par l’utilisateur (commence à 0).</param>
    /// <returns>True si la réponse est correcte, False sinon.</returns>
    public bool IsCorrect(string userAnswer)
    {
        return string.Equals(userAnswer?.Trim(), CorrectAnswer?.Trim(), StringComparison.OrdinalIgnoreCase);
    }
}
