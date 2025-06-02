namespace QuizAppV1.Interfaces;

/// <summary>
/// Définit un contrat pour calculer un score basé sur le nombre de bonnes réponses
/// et le nombre total de questions.
/// </summary>
internal interface IScoreCalculator
{
    /// <summary>
    /// Calcule le score sous forme de pourcentage à partir des réponses correctes
    /// et du nombre total de questions.
    /// </summary>
    /// <param name="correctAnswers">Nombre de bonnes réponses.</param>
    /// <param name="totalQuestions">Nombre total de questions posées.</param>
    /// <returns>Un entier représentant le score en pourcentage.</returns>
    int ComputeScore(int correctAnswers, int totalQuestions);
}
