using QuizAppV1.Models;

namespace QuizAppV1.Interfaces;

/// <summary>
/// Définit un contrat pour vérifier si une réponse utilisateur est correcte
/// par rapport à une question donnée.
/// </summary>
internal interface IAnswerChecker
{
    /// <summary>
    /// Évalue si la réponse de l'utilisateur correspond à la bonne réponse de la question.
    /// </summary>
    /// <param name="userAnswer">La réponse fournie par l'utilisateur.</param>
    /// <param name="question">La question à évaluer.</param>
    /// <returns>True si la réponse est correcte, sinon False.</returns>
    bool IsCorrect(string userAnswer, Question question);
}
