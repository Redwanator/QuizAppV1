using QuizAppV1.Common;
using QuizAppV1.Extensions;
using QuizAppV1.Models;
using QuizAppV1.Services;

namespace QuizAppV1;

public partial class QuizForm : Form
{
    private Discipline? _selectedDiscipline;
    private QuizService? _controller;
    private int _questionDurationMs = 10000;
    private int _remainingMs = 10000;

    public QuizForm()
    {
        InitializeComponent();
        LoadDisciplines();
        ToggleQuizUI(false);
    }

    private void ToggleQuizUI(bool sessionEnCours)
    {
        comboDisciplines.Enabled = !sessionEnCours;
        btnCommencer.Enabled = !sessionEnCours;
        panelQuiz.Visible = sessionEnCours;
        btnRecommencer.Enabled = sessionEnCours;
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
            MessageBox.Show(Messages.SelectDiscipline);
            return;
        }

        _controller = new QuizService(_selectedDiscipline.Questions);

        ToggleQuizUI(true);

        progressBarQuiz.Maximum = _controller.TotalQuestions;

        ShowQuestion();
    }

    private void ShowQuestion()
    {
        if (_controller?.IsFinished != false)
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

    private void ShowEndOfQuizMessage()
    {
        MessageBox.Show(
            string.Format(Messages.QuizEndMessage, _controller!.CorrectAnswers, _controller.TotalQuestions),
            Messages.QuizEndTitle,
            MessageBoxButtons.OK,
            icon: MessageBoxIcon.Information
        );
    }

    private void DisplayCurrentQuestion()
    {
        Question question = _controller!.CurrentQuestion!;
        lblQuestion.Text = question.Text;

        btnOption1.Text = question.Options.ElementAtOrDefault(0) ?? string.Empty;
        btnOption2.Text = question.Options.ElementAtOrDefault(1) ?? string.Empty;
        btnOption3.Text = question.Options.ElementAtOrDefault(2) ?? string.Empty;
    }

    private void DisplayProgress()
    {
        lblProgression.Text = $"Question {_controller!.CurrentIndex + 1} sur {_controller.TotalQuestions}";
        progressBarQuiz.Value = _controller.CurrentIndex + 1;
        lblFeedback.Text = "";
    }

    private void ResetTimerUI()
    {
        _remainingMs = _questionDurationMs;
        progressBarCurrentQuestion.Maximum = _questionDurationMs;
        progressBarCurrentQuestion.SetValueInstantly(_remainingMs);

        lblTimer.Text = $"Temps restant : {_remainingMs / 1000} s";
        timerCurrentQuestion.Interval = 50;
        timerCurrentQuestion.Start();
    }

    private void AnswerClicked(object sender, EventArgs e)
    {
        if (sender is not Button btnClicked) return;

        timerCurrentQuestion.Stop();
        SwitchAnswerButtonsStates(false);

        string selectedAnswer = btnClicked.Text;
        bool isCorrect = _controller!.RegisterAndCheckAnswer(selectedAnswer);

        if (isCorrect)
            lblFeedback.SetText(Messages.GoodAnswer, Color.Green);
        else
            lblFeedback.SetText(Messages.BadAnswer, Color.Red);

        _controller.MoveNext();
        timerNextQuestion.Start();
    }

    private void btnRecommencer_Click(object sender, EventArgs e)
    {
        DialogResult confirm = MessageBox.Show(
            Messages.RestartConfirmation,
            "Confirmation",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );

        if (confirm == DialogResult.No)
            return;

        _controller?.Reset(_selectedDiscipline?.Questions ?? []);
        ToggleQuizUI(false);
    }

    private void SwitchAnswerButtonsStates(bool enabled)
    {
        btnOption1.Enabled = enabled;
        btnOption2.Enabled = enabled;
        btnOption3.Enabled = enabled;
    }

    private enum progressBarColor
    {
        Normal = 1, // green
        Error = 2, // red
        Warning = 3 // yellow
    }

    private void timerCurrentQuestion_Tick(object sender, EventArgs e)
    {
        _remainingMs = Math.Max(0, _remainingMs - timerCurrentQuestion.Interval);

        progressBarCurrentQuestion.Value = _remainingMs;
        lblTimer.Text = $"Temps restant : {_remainingMs / 1000} s";

        progressBarCurrentQuestion.SetState(_remainingMs switch
        {
            <= 4000 => (int)progressBarColor.Error,
            <= 7000 => (int)progressBarColor.Warning,
            _ => (int)progressBarColor.Normal
        });

        if (_remainingMs == 0)
        {
            timerCurrentQuestion.Stop();
            lblFeedback.SetText(Messages.TimeOut, Color.OrangeRed);
            _controller?.MoveNext();
            timerNextQuestion.Start();
        }
    }

    private void timerNextQuestion_Tick(object sender, EventArgs e)
    {
        timerNextQuestion.Stop();
        ShowQuestion();
    }
}
