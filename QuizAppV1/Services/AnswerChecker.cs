using QuizAppV1.Interfaces;
using QuizAppV1.Models;

namespace QuizAppV1.Services;

internal class AnswerChecker : IAnswerChecker
{
    public bool IsCorrect(string userAnswer, Question question)
    {
        return question.IsCorrect(userAnswer);
    }
}
