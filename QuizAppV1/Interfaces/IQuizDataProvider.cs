using QuizAppV1.Models;

namespace QuizAppV1.Interfaces;

/// <summary>
/// Définit le contrat pour un fournisseur de données de quiz.
/// Permet d'accéder aux disciplines disponibles et à certaines disciplines spécifiques prédéfinies.
/// </summary>
internal interface IQuizDataProvider
{
    /// <summary>
    /// Retourne l'ensemble des disciplines disponibles pour le quiz.
    /// </summary>
    /// <returns>Une collection d'objets <see cref="Discipline"/>.</returns>
    IEnumerable<Discipline> GetDisciplines();
}
