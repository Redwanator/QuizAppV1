namespace QuizAppV1;

/// <summary>
/// Partie générée automatiquement du formulaire principal du quiz. 
/// Contient la définition et l’initialisation des composants graphiques.
/// </summary>
partial class QuizForm
{
    /// <summary>
    /// Conteneur pour les composants graphiques du formulaire, utilisé pour la gestion des ressources.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Libère les ressources utilisées par le formulaire, y compris les composants graphiques.
    /// </summary>
    /// <param name="disposing">
    /// True pour libérer les ressources managées et non managées ; False pour ne libérer que les ressources non managées.
    /// </param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Initialise et configure les composants de l’interface graphique du formulaire.
    /// Ne pas modifier manuellement ce code.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        comboDisciplines = new ComboBox();
        btnStart = new Button();
        btnRestart = new Button();
        timerCurrentQuestion = new System.Windows.Forms.Timer(components);
        panelQuiz = new Panel();
        progressBarCurrentQuestion = new ProgressBar();
        progressBarQuiz = new ProgressBar();
        lblQuestion = new Label();
        btnOption3 = new Button();
        btnOption2 = new Button();
        btnOption1 = new Button();
        lblProgression = new Label();
        lblFeedback = new Label();
        lblTimer = new Label();
        timerNextQuestion = new System.Windows.Forms.Timer(components);
        comboLangues = new ComboBox();
        panelQuiz.SuspendLayout();
        SuspendLayout();
        // 
        // comboDisciplines
        // 
        comboDisciplines.DropDownStyle = ComboBoxStyle.DropDownList;
        comboDisciplines.FormattingEnabled = true;
        comboDisciplines.Location = new Point(12, 12);
        comboDisciplines.Name = "comboDisciplines";
        comboDisciplines.Size = new Size(270, 28);
        comboDisciplines.TabIndex = 0;
        comboDisciplines.SelectedIndexChanged += comboDisciplines_SelectedIndexChanged;
        // 
        // btnStart
        // 
        btnStart.Location = new Point(288, 10);
        btnStart.Name = "btnStart";
        btnStart.Size = new Size(200, 30);
        btnStart.TabIndex = 1;
        btnStart.Text = "Commencer";
        btnStart.UseVisualStyleBackColor = true;
        btnStart.Click += btnStart_Click;
        // 
        // btnRestart
        // 
        btnRestart.Location = new Point(494, 10);
        btnRestart.Name = "btnRestart";
        btnRestart.Size = new Size(200, 30);
        btnRestart.TabIndex = 2;
        btnRestart.Text = "Recommencer";
        btnRestart.UseVisualStyleBackColor = true;
        btnRestart.Click += btnRestart_Click;
        // 
        // timerCurrentQuestion
        // 
        timerCurrentQuestion.Interval = 1000;
        timerCurrentQuestion.Tick += timerCurrentQuestion_Tick;
        // 
        // panelQuiz
        // 
        panelQuiz.Controls.Add(progressBarCurrentQuestion);
        panelQuiz.Controls.Add(progressBarQuiz);
        panelQuiz.Controls.Add(lblQuestion);
        panelQuiz.Controls.Add(btnOption3);
        panelQuiz.Controls.Add(btnOption2);
        panelQuiz.Controls.Add(btnOption1);
        panelQuiz.Controls.Add(lblProgression);
        panelQuiz.Controls.Add(lblFeedback);
        panelQuiz.Controls.Add(lblTimer);
        panelQuiz.Location = new Point(12, 46);
        panelQuiz.Name = "panelQuiz";
        panelQuiz.Size = new Size(776, 392);
        panelQuiz.TabIndex = 10;
        // 
        // progressBarCurrentQuestion
        // 
        progressBarCurrentQuestion.Location = new Point(591, 343);
        progressBarCurrentQuestion.Name = "progressBarCurrentQuestion";
        progressBarCurrentQuestion.Size = new Size(182, 18);
        progressBarCurrentQuestion.TabIndex = 18;
        // 
        // progressBarQuiz
        // 
        progressBarQuiz.Location = new Point(3, 343);
        progressBarQuiz.Name = "progressBarQuiz";
        progressBarQuiz.Size = new Size(143, 18);
        progressBarQuiz.TabIndex = 17;
        // 
        // lblQuestion
        // 
        lblQuestion.AutoSize = true;
        lblQuestion.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
        lblQuestion.Location = new Point(3, 23);
        lblQuestion.Name = "lblQuestion";
        lblQuestion.Size = new Size(115, 28);
        lblQuestion.TabIndex = 10;
        lblQuestion.Text = "Question ici";
        // 
        // btnOption3
        // 
        btnOption3.Location = new Point(114, 215);
        btnOption3.Name = "btnOption3";
        btnOption3.Size = new Size(550, 50);
        btnOption3.TabIndex = 13;
        btnOption3.Text = "Option 3";
        btnOption3.UseVisualStyleBackColor = true;
        btnOption3.Click += AnswerClicked;
        // 
        // btnOption2
        // 
        btnOption2.Location = new Point(114, 147);
        btnOption2.Name = "btnOption2";
        btnOption2.Size = new Size(550, 50);
        btnOption2.TabIndex = 12;
        btnOption2.Text = "Option 2";
        btnOption2.UseVisualStyleBackColor = true;
        btnOption2.Click += AnswerClicked;
        // 
        // btnOption1
        // 
        btnOption1.Location = new Point(114, 80);
        btnOption1.Name = "btnOption1";
        btnOption1.Size = new Size(550, 50);
        btnOption1.TabIndex = 11;
        btnOption1.Text = "Option 1";
        btnOption1.UseVisualStyleBackColor = true;
        btnOption1.Click += AnswerClicked;
        // 
        // lblProgression
        // 
        lblProgression.AutoSize = true;
        lblProgression.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblProgression.Location = new Point(3, 369);
        lblProgression.Name = "lblProgression";
        lblProgression.Size = new Size(126, 23);
        lblProgression.TabIndex = 16;
        lblProgression.Text = "Progression ici";
        // 
        // lblFeedback
        // 
        lblFeedback.AutoSize = true;
        lblFeedback.Font = new Font("Segoe UI", 10.2F, FontStyle.Italic, GraphicsUnit.Point, 0);
        lblFeedback.Location = new Point(3, 309);
        lblFeedback.Name = "lblFeedback";
        lblFeedback.Size = new Size(183, 23);
        lblFeedback.TabIndex = 14;
        lblFeedback.Text = "Retour d'information ici";
        // 
        // lblTimer
        // 
        lblTimer.AutoSize = true;
        lblTimer.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblTimer.ForeColor = Color.Maroon;
        lblTimer.Location = new Point(591, 309);
        lblTimer.Name = "lblTimer";
        lblTimer.Size = new Size(143, 23);
        lblTimer.TabIndex = 15;
        lblTimer.Text = "10s par question";
        // 
        // timerNextQuestion
        // 
        timerNextQuestion.Interval = 1500;
        timerNextQuestion.Tick += timerNextQuestion_Tick;
        // 
        // comboLangues
        // 
        comboLangues.DropDownStyle = ComboBoxStyle.DropDownList;
        comboLangues.FormattingEnabled = true;
        comboLangues.Location = new Point(698, 12);
        comboLangues.Name = "comboLangues";
        comboLangues.Size = new Size(90, 28);
        comboLangues.TabIndex = 11;
        comboLangues.SelectedIndexChanged += comboLangues_SelectedIndexChanged;
        // 
        // QuizForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(comboLangues);
        Controls.Add(panelQuiz);
        Controls.Add(btnRestart);
        Controls.Add(btnStart);
        Controls.Add(comboDisciplines);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "QuizForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Quiz App";
        panelQuiz.ResumeLayout(false);
        panelQuiz.PerformLayout();
        ResumeLayout(false);
    }

    #endregion
    private ComboBox comboDisciplines;
    private Button btnStart;
    private Button btnRestart;
    private System.Windows.Forms.Timer timerCurrentQuestion;
    private Panel panelQuiz;
    private Label lblQuestion;
    private Button btnOption3;
    private Button btnOption2;
    private Button btnOption1;
    private Label lblProgression;
    private Label lblFeedback;
    private Label lblTimer;
    private ProgressBar progressBarQuiz;
    private System.Windows.Forms.Timer timerNextQuestion;
    private ProgressBar progressBarCurrentQuestion;
    private ComboBox comboLangues;
}
