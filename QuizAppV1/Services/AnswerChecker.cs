using QuizAppV1.Interfaces;
using QuizAppV1.Models;

namespace QuizAppV1.Services;

/// <summary>
/// Fournit une implémentation standard du mécanisme de vérification
/// d’une réponse utilisateur par rapport à la bonne réponse attendue.
/// </summary>
internal class AnswerChecker : IAnswerChecker
{
    /// <summary>
    /// Détermine si la réponse donnée par l’utilisateur est correcte
    /// en la comparant à la bonne réponse définie dans la question.
    /// </summary>
    /// <param name="userAnswer">Réponse saisie par l’utilisateur.</param>
    /// <param name="question">Question concernée par la réponse.</param>
    /// <returns>True si la réponse est correcte, False sinon.</returns>
    public bool IsCorrect(string userAnswer, Question question)
    {
        return question.IsCorrect(userAnswer);
    }
}
