namespace QuizAppV1.Interfaces;

internal interface IScoreCalculator
{
    int ComputeScore(int correctAnswers, int totalQuestions);
}
