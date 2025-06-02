using QuizAppV1.Interfaces;

namespace QuizAppV1.Services;

internal class ScoreCalculator : IScoreCalculator
{
    public int ComputeScore(int correctAnswers, int totalQuestions)
    {
        return (int)((double)correctAnswers / totalQuestions * 100);
    }
}
