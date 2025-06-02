using QuizAppV1.Models;

namespace QuizAppV1.Interfaces;

/// <summary>
/// Définit le contrat d’un gestionnaire de session de quiz, responsable de la progression,
/// de l’évaluation des réponses et du calcul du score.
/// </summary>
internal interface IQuizManager
{
    /// <summary>
    /// Obtient la question actuellement posée à l'utilisateur.
    /// </summary>
    Question? CurrentQuestion { get; }

    /// <summary>
    /// Indice de la question en cours (commence à 0).
    /// </summary>
    int CurrentIndex { get; }

    /// <summary>
    /// Nombre total de questions dans la session de quiz.
    /// </summary>
    int TotalQuestions { get; }

    /// <summary>
    /// Nombre de bonnes réponses données par l'utilisateur jusqu'à présent.
    /// </summary>
    int CorrectAnswers { get; }

    /// <summary>
    /// Indique si toutes les questions ont été parcourues.
    /// </summary>
    bool IsFinished { get; }

    /// <summary>
    /// Enregistre la réponse de l'utilisateur et retourne si elle est correcte.
    /// </summary>
    /// <param name="answer">La réponse sélectionnée par l'utilisateur.</param>
    /// <returns>True si la réponse est correcte, False sinon.</returns>
    bool RegisterAndCheckAnswer(string answer);

    /// <summary>
    /// Passe à la question suivante dans la session.
    /// </summary>
    void MoveNext();

    /// <summary>
    /// Réinitialise la session avec un nouvel ensemble de questions.
    /// </summary>
    /// <param name="questions">Les questions à utiliser pour la nouvelle session.</param>
    void Reset(IEnumerable<Question> questions);

    /// <summary>
    /// Calcule le score actuel de l'utilisateur en pourcentage.
    /// </summary>
    /// <returns>Le score sur 100, arrondi à l'entier le plus proche.</returns>
    int GetScorePercentage();
}
