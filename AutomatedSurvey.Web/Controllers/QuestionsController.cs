using System.Web.Mvc;
using AutomatedSurvey.Web.Domain;
using AutomatedSurvey.Web.Models;
using AutomatedSurvey.Web.Models.Repository;
using Twilio.AspNet.Mvc;

namespace AutomatedSurvey.Web.Controllers
{
    public class QuestionsController : TwilioController
    {
        private readonly IRepository<Question> _questionsRepository;

        public QuestionsController() : this(new QuestionsRepository()) { }

        public QuestionsController(
            IRepository<Question> questionsRepository)
        {
            _questionsRepository = questionsRepository;
        }

        // GET: /questions/find/5
        public ActionResult Find(int id)
        {
            var question = _questionsRepository.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }

            var response = new Response(question).Build();
            return TwiML(response);
        }
    }
}