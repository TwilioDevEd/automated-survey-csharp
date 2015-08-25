using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutomatedSurvey.Web.Controllers;
using AutomatedSurvey.Web.Models;
using AutomatedSurvey.Web.Models.Repository;
using AutomatedSurvey.Web.Test.Repositories;
using NUnit.Framework;

namespace AutomatedSurvey.Web.Test.Controllers
{
    public class AnswersControllerTest
    {
        [Test]
        public void Create_Answers_stores_an_answer_in_the_database()
        {
            var questionsRepositoy = new InMemoryQuestionsRepository();
            var answersRepository = new InMemoryAnswersRepository();

            questionsRepositoy.Create(new Question { Id = 1, Body = "Question" });
            questionsRepositoy.Create(new Question { Id = 2, Body = "Question" });

            var controller = GetAnswersController(
                questionsRepositoy, answersRepository);

            var answer = new Answer
            {
                QuestionId = 1,
                RecordingUrl = "http://recording.info/n17sugA",
                Digits = "#",
                CallSid = "9s883999dis0039",
                From = "+29999999"
            };

            controller.Create(answer);

            var answers = answersRepository.All();
            Assert.That(answers, Contains.Item(answer));
        }

        private static AnswersController GetAnswersController(
            IRepository<Question> questionsRepository, IRepository<Answer> answersRepository)
        {
            var controller = new AnswersController(questionsRepository, answersRepository);

            controller.ControllerContext = new ControllerContext
            {
                Controller = controller,
                RequestContext = new RequestContext(new MockHttpContext(), new RouteData())
            };

            return controller;
        }

        private class MockHttpContext : HttpContextBase
        {
            private readonly IPrincipal _user = new GenericPrincipal(
                     new GenericIdentity("someUser"), null /* roles */);

            public override IPrincipal User
            {
                get { return _user; }
                set { base.User = value; }
            }
        }
    }
}
