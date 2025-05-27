using QuizAppV1.Models;
using QuizAppV1.Services;

namespace QuizAppV1;

public partial class QuizForm : Form
{
    private Discipline? _selectedDiscipline;
    private List<Question> _questions = new();
    private int _questionsCount = 0;
    private int _currentQuestionIndex = 0;
    private int _correctAnswers = 0;
    private int _taskDelay = 1500;
    private int _remainingTime = 10;

    public QuizForm()
    {
        InitializeComponent();
        LoadDisciplines();
        ToggleQuizUI(false);
    }

    private void ToggleQuizUI(bool sessionEnCours)
    {
        // Quand une session est en cours
        if (sessionEnCours)
        {
            // Masquer les choix de discipline
            comboDisciplines.Enabled = false;
            btnCommencer.Enabled = false;

            // Afficher le panel du quiz
            panelQuiz.Visible = true;

            // Activer le bouton Recommencer
            btnRecommencer.Enabled = true;
        }
        else
        {
            // Afficher les choix de discipline
            comboDisciplines.Enabled = true;
            btnCommencer.Enabled = true;

            // Masquer le quiz
            panelQuiz.Visible = false;

            // Désactiver Recommencer
            btnRecommencer.Enabled = false;

            // Réinitialisation visuelle
            lblFeedback.Text = "Retour d'information ici";
            lblQuestion.Text = "Question ici";
            lblProgression.Text = "Progression ici";
            lblTimer.Text = "10s par question";
        }
    }


    private void LoadDisciplines()
    {
        JsonQuizDataProvider provider = new();
        IEnumerable<Discipline> disciplines = provider.GetDisciplines();

        comboDisciplines.DataSource = disciplines;
        comboDisciplines.DisplayMember = nameof(Discipline.Name);
    }

    private void comboDisciplines_SelectedIndexChanged(object sender, EventArgs e)
    {
        _selectedDiscipline = comboDisciplines.SelectedItem as Discipline;
    }

    private void btnCommencer_Click(object sender, EventArgs e)
    {
        if (_selectedDiscipline == null)
        {
            MessageBox.Show("Veuillez sélectionner une discipline avant de commencer le quiz.");
            return;
        }

        _questions = _selectedDiscipline.Questions.ToList();

        if (_questions == null)
        {
            MessageBox.Show("Aucune question disponible pour cette discipline.");
            return;
        }

        ToggleQuizUI(true);

        _questionsCount = _questions.Count;
        _currentQuestionIndex = 0;
        _correctAnswers = 0;

        ShowQuestion();
    }

    private void ShowQuestion()
    {
        if (_currentQuestionIndex >= _questionsCount)
        {
            MessageBox.Show($"Quiz terminé ! \r\nScore : {_correctAnswers} / {_questionsCount}",
                "Résultat",
                MessageBoxButtons.OK,
                icon:MessageBoxIcon.Information
            );

            ToggleQuizUI(false);

            return;
        }

        SetAnswerButtonsEnabled(true);

        Question question = _questions[_currentQuestionIndex];
        lblQuestion.Text = question.Text;

        IEnumerable<string> options = question.Options;
        btnOption1.Text = options.ElementAtOrDefault(0) ?? string.Empty;
        btnOption2.Text = options.ElementAtOrDefault(1) ?? string.Empty;
        btnOption3.Text = options.ElementAtOrDefault(2) ?? string.Empty;

        lblProgression.Text = $"Question {_currentQuestionIndex + 1} sur {_questionsCount}";
        lblFeedback.Text = "";

        _remainingTime = 10;
        lblTimer.Text = $"Temps restant : {_remainingTime} s";
        timer1.Start();
    }

    private async void AnswerClicked(object sender, EventArgs e)
    {
        if (sender is not Button btnClicked) return;

        timer1.Stop();

        SetAnswerButtonsEnabled(false);

        string selectedAnswer = btnClicked.Text;
        Question currentQuestion = _questions[_currentQuestionIndex];

        if (currentQuestion.IsCorrect(selectedAnswer))
        {
            lblFeedback.Text = "Bonne réponse !";
            lblFeedback.ForeColor = Color.Green;
            _correctAnswers++;
        }
        else
        {
            //lblFeedback.Text = $"Mauvaise réponse. Réponse attendue : {currentQuestion.CorrectAnswer}";
            lblFeedback.Text = "Mauvaise réponse.";
            lblFeedback.ForeColor = Color.Red;
        }

        _currentQuestionIndex++;
        //Task.Delay(1500).ContinueWith(Invoke(ShowQuestion)); Task.Invoke ne fonctionne pas

        await Task.Delay(_taskDelay);

        ShowQuestion();
    }

    private void SetAnswerButtonsEnabled(bool enabled)
    {
        btnOption1.Enabled = enabled;
        btnOption2.Enabled = enabled;
        btnOption3.Enabled = enabled;
    }

    private void btnRecommencer_Click(object sender, EventArgs e)
    {
        var confirm = MessageBox.Show(
            "Voulez-vous vraiment recommencer le quiz ?",
            "Confirmation",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );

        if (confirm == DialogResult.No)
            return;

        _questions = _selectedDiscipline?.Questions?.ToList() ?? new List<Question>();
        _questionsCount = _questions.Count;
        _currentQuestionIndex = 0;
        _correctAnswers = 0;

        //_selectedDiscipline = null;
        //_questions.Clear();

        ToggleQuizUI(false);
    }

    private async void timer1_Tick(object sender, EventArgs e)
    {
        _remainingTime--;

        lblTimer.Text = $"Temps restant : {_remainingTime} s";

        if ( _remainingTime <= 0)
        {
            timer1.Stop();

            lblFeedback.Text = "Temps écoulé !";
            lblFeedback.ForeColor = Color.OrangeRed;

            _currentQuestionIndex++;

            await Task.Delay(_taskDelay);

            ShowQuestion();
        }
    }
}
