using QuizAppV1.Interfaces;

namespace QuizAppV1.Services;

/// <summary>
/// Fournit une implémentation standard pour calculer le score d’un quiz
/// sous forme de pourcentage.
/// </summary>
internal class ScoreCalculator : IScoreCalculator
{
    /// <summary>
    /// Calcule le score en pourcentage en fonction du nombre de bonnes réponses
    /// et du nombre total de questions.
    /// </summary>
    /// <param name="correctAnswers">Nombre de réponses correctes.</param>
    /// <param name="totalQuestions">Nombre total de questions posées.</param>
    /// <returns>Le score sous forme de pourcentage entier (ex. 80 pour 4/5).</returns>
    public int ComputeScore(int correctAnswers, int totalQuestions)
    {
        return (int)((double)correctAnswers / totalQuestions * 100);
    }
}
