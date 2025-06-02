using System.Runtime.InteropServices;

namespace QuizAppV1.Extensions;

/// <summary>
/// Fournit des méthodes d'extension utiles pour personnaliser des contrôles WinForms,
/// comme les barres de progression et les étiquettes.
/// </summary>
internal static class WinFormsExtensions
{
    /// <summary>
    /// Met à jour instantanément la valeur d'une <see cref="ProgressBar"/> sans provoquer d'animation visuelle.
    /// </summary>
    /// <param name="progressBar">La barre de progression à modifier.</param>
    /// <param name="value">La valeur cible à appliquer.</param>
    public static void SetValueInstantly(this ProgressBar progressBar, int value)
    {
        ++progressBar.Maximum;
        progressBar.Value = value + 1;
        progressBar.Value = value;
        --progressBar.Maximum;
    }

    /// <summary>
    /// Envoie un message à une fenêtre ou un contrôle spécifique.
    /// Utilisé ici pour changer l'état visuel d'une <see cref="ProgressBar"/> via l'API Win32.
    /// </summary>
    /// <param name="hWnd">Handle (pointeur) vers la fenêtre ou le contrôle ciblé.</param>
    /// <param name="Msg">Code du message à envoyer.</param>
    /// <param name="w">Paramètre WPARAM du message (interprété selon le message).</param>
    /// <param name="l">Paramètre LPARAM du message (interprété selon le message).</param>
    /// <returns>Un pointeur vers le résultat du message (souvent ignoré ici).</returns>
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
    static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);

    /// <summary>
    /// Définit l'état visuel (couleur) d'une <see cref="ProgressBar"/> via une API native Windows.
    /// </summary>
    /// <param name="pBar">La barre de progression ciblée.</param>
    /// <param name="state">
    /// L'état à appliquer :
    /// 1 = vert (normal), 2 = rouge (erreur), 3 = jaune (avertissement).
    /// </param>
    public static void SetState(this ProgressBar pBar, int state)
    {
        SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
    }

    /// <summary>
    /// Modifie le texte et la couleur d'une <see cref="Label"/> de façon combinée.
    /// </summary>
    /// <param name="label">L'étiquette à mettre à jour.</param>
    /// <param name="text">Le nouveau texte à afficher.</param>
    /// <param name="color">La couleur à appliquer au texte.</param>
    public static void SetText(this Label label, string text, Color color)
    {
        label.Text = text;
        label.ForeColor = color;
    }
}
