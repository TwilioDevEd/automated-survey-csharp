using AutomatedSurvey.Web.Models;
using AutomatedSurvey.Web.Models.Repository;

namespace AutomatedSurvey.Web.Domain
{
    public class QuestionFinder
    {
        private readonly IRepository<Question> _repository;

        public QuestionFinder(IRepository<Question> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Finds the next question.
        /// </summary>
        /// <param name="questionId">The question ID</param>
        /// <returns>The next question if available, otherwise null</returns>
        public Question FindNext(int questionId)
        {
            var currentQuestion = _repository.Find(questionId);
            var nextQuestionId = questionId + 1;

            return _repository.FirstOrDefault(
                q => q.SurveyId == currentQuestion.SurveyId && q.Id == nextQuestionId);
        }
    }
}