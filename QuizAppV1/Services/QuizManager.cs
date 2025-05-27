using QuizAppV1.Interfaces;

namespace QuizAppV1.Services;

/// <summary>
/// Gère le déroulement complet du quiz : sélection de la discipline, déroulement des questions,
/// calcul du score, affichage du résultat et relance éventuelle.
/// </summary>
/// <param name="dataProvider">Fournisseur des données du quiz (disciplines et questions).</param>
internal class QuizManager(IQuizDataProvider dataProvider)
{
    private readonly IQuizDataProvider _dataProvider = dataProvider;
}
