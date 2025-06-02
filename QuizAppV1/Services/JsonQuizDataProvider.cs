using QuizAppV1.Interfaces;
using QuizAppV1.Models;
using System.Text.Json;

namespace QuizAppV1.Services;

/// <summary>
/// Fournit les questions du quiz en lisant un fichier JSON externe.
/// Elle implémente l'interface IQuizDataProvider, qui garantit qu'elle expose la méthode GetDisciplines().
/// </summary>
internal class JsonQuizDataProvider : IQuizDataProvider
{
    // Chemin relatif vers le fichier JSON contenant les disciplines.
    private readonly string _jsonPath = "Data/quizzes.json";

    /// <summary>
    /// Charge toutes les disciplines disponibles à partir du fichier JSON.
    /// Si le fichier est introuvable ou vide, une liste vide est retournée.
    /// </summary>
    /// <returns>Liste des disciplines disponibles pour le quiz.</returns>
    public IEnumerable<Discipline> GetDisciplines()
    {
        // Vérifie si le fichier JSON existe à l'emplacement prévu, sinon une exception explicite est levée.
        if (!File.Exists(_jsonPath))
            throw new FileNotFoundException($"Fichier non trouvé : {_jsonPath}");

        // Lit tout le contenu du fichier JSON en UTF-8 pour gérer les caractères spéciaux.
        string json = File.ReadAllText(_jsonPath, System.Text.Encoding.UTF8);

        // Configure l’option de désérialisation : les noms de propriétés ne sont pas sensibles à la casse.
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        // Désérialise le contenu JSON en une liste de Discipline, sinon retourne une nouvelle liste vide.
        List<Discipline> disciplines = JsonSerializer.Deserialize<List<Discipline>>(json, options)
                          ?? new List<Discipline>();

        // Retourne la liste des disciplines chargées.
        return disciplines;
    }
}
