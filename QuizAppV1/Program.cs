namespace QuizAppV1;

/// <summary>
/// Point d'entrée principal de l'application QuizApp.
/// Initialise la configuration WinForms et lance le formulaire principal.
/// </summary>
internal static class Program
{
    /// <summary>
    /// Méthode principale exécutée au démarrage de l’application.
    /// Configure l’environnement graphique et affiche le formulaire Quiz.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(new QuizForm());
    }
}