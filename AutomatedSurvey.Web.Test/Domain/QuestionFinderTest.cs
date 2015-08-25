using AutomatedSurvey.Web.Domain;
using AutomatedSurvey.Web.Models;
using AutomatedSurvey.Web.Test.Repositories;
using NUnit.Framework;

namespace AutomatedSurvey.Web.Test.Domain
{
    public class QuestionFinderTest
    {
        [Test]
        public void QuestionFind_returns_next_question_if_is_available()
        {
            var firstQuestion = new Question { Id = 1, SurveyId = 1 };
            var secondQuestion = new Question { Id = 2, SurveyId = 1 };

            var questionsRepository = new InMemoryQuestionsRepository();
            questionsRepository.Create(firstQuestion);
            questionsRepository.Create(secondQuestion);

            var nextQuestion = new QuestionFinder(questionsRepository).FindNext(firstQuestion.Id);

            Assert.That(nextQuestion, Is.EqualTo(secondQuestion));
        }

        [Test]
        public void QuestionFind_returns_null_question_if_is_unavailable()
        {
            var firstQuestion = new Question { Id = 1, SurveyId = 1 };

            var questionsRepository = new InMemoryQuestionsRepository();
            questionsRepository.Create(firstQuestion);

            var nextQuestion = new QuestionFinder(questionsRepository).FindNext(firstQuestion.Id);

            Assert.That(nextQuestion, Is.Null);
        }
    }
}
