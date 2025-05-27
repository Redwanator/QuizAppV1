namespace QuizAppV1.Models;

/// <summary>
/// Représente un domaine de quiz regroupant plusieurs questions à choix multiple.
/// </summary>
/// <param name="name">Nom affiché de la discipline.</param>
/// <param name="questions">Liste des questions associées à cette discipline.</param>
internal class Discipline(string name, IEnumerable<Question> questions)
{
    /// <summary>
    /// Nom lisible de la discipline (affiché à l’utilisateur).
    /// </summary>
    public string Name { get; init; } = name;

    /// <summary>
    /// Ensemble des questions liées à cette discipline.
    /// </summary>
    public IEnumerable<Question> Questions { get; init; } = questions;
}
