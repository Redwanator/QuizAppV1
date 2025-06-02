using QuizAppV1.Extensions;
using QuizAppV1.Interfaces;
using QuizAppV1.Models;
using QuizAppV1.Resources;
using QuizAppV1.Services;
using System.Globalization;

namespace QuizAppV1;

/// <summary>
/// Formulaire principal de l’application QuizApp.
/// Gère l’interface utilisateur, la logique de navigation entre les questions,
/// l’affichage du feedback, les minuteries et les interactions avec le gestionnaire de quiz.
/// </summary>
public partial class QuizForm : Form
{
    private Discipline? _selectedDiscipline;
    private IQuizManager? _manager;
    private int _questionDurationMs = 10000;
    private int _remainingMs = 10000;

    /// <summary>
    /// Initialise le formulaire principal et charge les disciplines disponibles pour le quiz.
    /// </summary>
    public QuizForm()
    {
        InitializeComponent();
        InitializeLanguages();
        ApplyLanguage();
        LoadDisciplines();
        ToggleQuizUI(false);
    }

    /// <summary>
    /// Représente une langue supportée, avec nom affiché et code culture.
    /// </summary>
    private record Language(string DisplayName, string CultureCode);

    /// <summary>
    /// Liste des langues disponibles dans l'application.
    /// </summary>
    private static readonly Language[] SupportedLanguages =
    {
        new("Français", "fr"),
        new("English", "en"),
        new("Español", "es"),
        new("Italiano", "it"),
        new("العربية", "ar")
    };

    /// <summary>
    /// Initialise les langues disponibles dans la ComboBox et sélectionne la langue par défaut (Français).
    /// </summary>
    private void InitializeLanguages()
    {
        comboLangues.Items.Clear();
        comboLangues.Items.AddRange(SupportedLanguages.Select(lang => lang.DisplayName).ToArray());
        comboLangues.SelectedItem = SupportedLanguages[0].DisplayName; // Default: Français
    }

    private void ApplyLanguage()
    {
        btnStart.Text = Strings.StartButton;
        btnRestart.Text = Strings.RestartButton;
        lblFeedback.Text = "";
        lblTimer.Text = string.Format(Strings.TimerRemaining, _questionDurationMs / 1000);
    }

    /// <summary>
    /// Charge les disciplines disponibles à partir du fournisseur de données JSON et les affiche dans la liste déroulante.
    /// </summary>
    private void LoadDisciplines()
    {
        JsonQuizDataProvider provider = new();
        IEnumerable<Discipline> disciplines = provider.GetDisciplines();

        comboDisciplines.DataSource = disciplines;
        comboDisciplines.DisplayMember = nameof(Discipline.Name);
    }

    /// <summary>
    /// Active ou désactive les composants de l'interface en fonction de l’état de la session de quiz.
    /// </summary>
    /// <param name="sessionEnCours">True si le quiz est en cours, False sinon.</param>
    private void ToggleQuizUI(bool sessionEnCours)
    {
        comboDisciplines.Enabled = !sessionEnCours;
        btnStart.Enabled = !sessionEnCours;
        panelQuiz.Visible = sessionEnCours;
        btnRestart.Enabled = sessionEnCours;
    }

    /// <summary>
    /// Met à jour la discipline sélectionnée lorsque l'utilisateur change la sélection dans la liste déroulante.
    /// </summary>
    /// <param name="sender">Contrôle émetteur.</param>
    /// <param name="e">Arguments de l'événement.</param>
    private void comboDisciplines_SelectedIndexChanged(object sender, EventArgs e)
    {
        _selectedDiscipline = comboDisciplines.SelectedItem as Discipline;
    }

    /// <summary>
    /// Démarre une nouvelle session de quiz avec la discipline sélectionnée.
    /// </summary>
    /// <param name="sender">Contrôle émetteur.</param>
    /// <param name="e">Arguments de l'événement.</param>
    private void btnStart_Click(object sender, EventArgs e)
    {
        if (_selectedDiscipline == null)
        {
            MessageBox.Show(Strings.SelectDiscipline);
            return;
        }

        IAnswerChecker checker = new AnswerChecker();
        IScoreCalculator calculator = new ScoreCalculator();
        _manager = new QuizManager(_selectedDiscipline.Questions, checker, calculator);

        ToggleQuizUI(true);

        progressBarQuiz.Maximum = _manager.TotalQuestions;

        ShowQuestion();
    }

    /// <summary>
    /// Affiche la question actuelle ou les résultats si le quiz est terminé.
    /// </summary>
    private void ShowQuestion()
    {
        if (_manager?.IsFinished != false)
        {
            ShowEndOfQuizMessage();
            ToggleQuizUI(false);
            return;
        }

        SwitchAnswerButtonsStates(true);
        DisplayCurrentQuestion();
        DisplayProgress();
        ResetTimerUI();
    }

    /// <summary>
    /// Affiche un message récapitulatif des résultats du quiz (note et pourcentage).
    /// </summary>
    private void ShowEndOfQuizMessage()
    {
        int correct = _manager!.CorrectAnswers;
        int total = _manager.TotalQuestions;
        int percent = _manager.GetScorePercentage();

        MessageBox.Show(
            string.Format(
                Strings.QuizEndMessage,
                correct,
                total,
                percent
                ),
            Strings.QuizEndTitle,
            MessageBoxButtons.OK,
            icon: MessageBoxIcon.Information
        );
    }

    /// <summary>
    /// Affiche le texte et les options de réponse de la question en cours.
    /// </summary>
    private void DisplayCurrentQuestion()
    {
        Question question = _manager!.CurrentQuestion!;
        lblQuestion.Text = question.Text;

        btnOption1.Text = question.Options.ElementAtOrDefault(0) ?? string.Empty;
        btnOption2.Text = question.Options.ElementAtOrDefault(1) ?? string.Empty;
        btnOption3.Text = question.Options.ElementAtOrDefault(2) ?? string.Empty;
    }

    /// <summary>
    /// Met à jour l'affichage de la progression dans le quiz (question X sur Y).
    /// </summary>
    private void DisplayProgress()
    {
        lblProgression.Text = string.Format(
            Strings.ProgressFormat,
            _manager!.CurrentIndex + 1,
            _manager.TotalQuestions
            );
        progressBarQuiz.Value = _manager.CurrentIndex + 1;
        lblFeedback.Text = "";
    }

    /// <summary>
    /// Réinitialise le chronomètre pour la question actuelle et démarre le timer associé.
    /// </summary>
    private void ResetTimerUI()
    {
        _remainingMs = _questionDurationMs;
        progressBarCurrentQuestion.Maximum = _questionDurationMs;
        progressBarCurrentQuestion.SetValueInstantly(_remainingMs);

        lblTimer.Text = string.Format(
            Strings.TimerRemaining,
            _remainingMs / 1000
            );
        timerCurrentQuestion.Interval = 50;
        timerCurrentQuestion.Start();
    }

    /// <summary>
    /// Gère la réponse sélectionnée par l’utilisateur, affiche un retour visuel, et passe à la question suivante.
    /// </summary>
    /// <param name="sender">Bouton cliqué.</param>
    /// <param name="e">Arguments de l’événement.</param>
    private void AnswerClicked(object sender, EventArgs e)
    {
        if (sender is not Button btnClicked) return;

        timerCurrentQuestion.Stop();
        SwitchAnswerButtonsStates(false);

        string selectedAnswer = btnClicked.Text;
        bool isCorrect = _manager!.RegisterAndCheckAnswer(selectedAnswer);

        if (isCorrect)
        {
            lblFeedback.SetText(Strings.GoodAnswer, Color.Green);
            btnClicked.BackColor = Color.LightGreen;
        }
        else
        {
            lblFeedback.SetText(Strings.BadAnswer, Color.Red);
            btnClicked.BackColor = Color.IndianRed;
        }

        _manager.MoveNext();
        timerNextQuestion.Start();
    }

    /// <summary>
    /// Demande confirmation à l'utilisateur, puis réinitialise l'état du quiz avec la discipline actuellement sélectionnée.
    /// L'utilisateur peut ensuite relancer le quiz manuellement en cliquant sur “Commencer”.
    /// </summary>
    /// <param name="sender">Bouton Recommencer.</param>
    /// <param name="e">Arguments de l’événement.</param>
    private void btnRestart_Click(object sender, EventArgs e)
    {
        DialogResult confirm = MessageBox.Show(
            Strings.RestartConfirmation,
            Strings.ConfirmationTitle,
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );

        if (confirm == DialogResult.No)
            return;

        _manager?.Reset(_selectedDiscipline?.Questions ?? []);
        ToggleQuizUI(false);
    }

    /// <summary>
    /// Active ou désactive les boutons de réponse et réinitialise leur couleur de fond.
    /// </summary>
    /// <param name="enabled">True pour activer, False pour désactiver.</param>
    private void SwitchAnswerButtonsStates(bool enabled)
    {
        Color defaultColor = SystemColors.Control;

        btnOption1.Enabled = enabled;
        btnOption1.BackColor = defaultColor;

        btnOption2.Enabled = enabled;
        btnOption2.BackColor = defaultColor;

        btnOption3.Enabled = enabled;
        btnOption3.BackColor = defaultColor;
    }

    /// <summary>
    /// Représente les états de couleur possibles pour la barre de progression de la question :
    /// <list type="bullet">
    /// <item><term>Normal</term> Vert (temps suffisant)</item>
    /// <item><term>Warning</term> Jaune (mi-temps)</item>
    /// <item><term>Error</term> Rouge (fin imminente ou temps écoulé)</item>
    /// </list>
    /// </summary>
    private enum progressBarColor
    {
        Normal = 1,
        Error = 2,
        Warning = 3
    }

    /// <summary>
    /// Gère le décompte de temps pour chaque question et passe à la suivante quand le temps est écoulé.
    /// </summary>
    /// <param name="sender">Timer associé.</param>
    /// <param name="e">Arguments de l’événement.</param>
    private void timerCurrentQuestion_Tick(object sender, EventArgs e)
    {
        _remainingMs = Math.Max(0, _remainingMs - timerCurrentQuestion.Interval);

        progressBarCurrentQuestion.Value = _remainingMs;
        lblTimer.Text = string.Format(
            Strings.TimerRemaining,
            _remainingMs / 1000
            );

        progressBarCurrentQuestion.SetState(_remainingMs switch
        {
            <= 4000 => (int)progressBarColor.Error,
            <= 7000 => (int)progressBarColor.Warning,
            _ => (int)progressBarColor.Normal
        });

        if (_remainingMs == 0)
        {
            timerCurrentQuestion.Stop();
            lblFeedback.SetText(Strings.TimeOut, Color.OrangeRed);
            _manager?.MoveNext();
            timerNextQuestion.Start();
        }
    }

    /// <summary>
    /// Déclenche l'affichage de la question suivante après un court délai.
    /// </summary>
    /// <param name="sender">Timer associé.</param>
    /// <param name="e">Arguments de l’événement.</param>
    private void timerNextQuestion_Tick(object sender, EventArgs e)
    {
        timerNextQuestion.Stop();
        ShowQuestion();
    }

    /// <summary>
    /// Gère le changement de langue sélectionnée dans la ComboBox.
    /// Met à jour la culture du thread et recharge les textes de l'interface.
    /// </summary>
    private void comboLangues_SelectedIndexChanged(object sender, EventArgs e)
    {
        string? selectedDisplayName = comboLangues.SelectedItem?.ToString();

        // Recherche la langue sélectionnée dans la liste disponible
        Language? selectedLanguage = SupportedLanguages.FirstOrDefault(
            lang => lang.DisplayName == selectedDisplayName
        );

        if (selectedLanguage is null)
            return;

        // Appliquer la langue choisie à l'application
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(selectedLanguage.CultureCode);
        ApplyLanguage();

        // Recharger l'affichage de la question si une session est en cours
        if (_manager is not null && !_manager.IsFinished)
            DisplayCurrentQuestion();
    }
}
