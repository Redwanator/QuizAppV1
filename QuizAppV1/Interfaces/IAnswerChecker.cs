using QuizAppV1.Models;

namespace QuizAppV1.Interfaces;

internal interface IAnswerChecker
{
    bool IsCorrect(string userAnswer, Question question);
}
