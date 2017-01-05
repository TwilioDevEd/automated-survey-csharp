using System.Web.Mvc;
using AutomatedSurvey.Web.Domain;
using AutomatedSurvey.Web.Models;
using AutomatedSurvey.Web.Models.Repository;
using Twilio.TwiML;

namespace AutomatedSurvey.Web.Controllers
{
    public class AnswersController : Controller
    {
        private readonly IRepository<Question> _questionsRepository;
        private readonly IRepository<Answer> _answersRepository;

        public AnswersController()
            : this(
                new QuestionsRepository(),
                new AnswersRepository()) { }

        public AnswersController(
            IRepository<Question> questionsRepository,
            IRepository<Answer> answersRepository
            )
        {
            _questionsRepository = questionsRepository;
            _answersRepository = answersRepository;
        }

        [HttpPost]
        public ActionResult Create(
            [Bind(Include = "QuestionId,RecordingUrl,Digits,CallSid,From")]
            Answer answer)
        {
            _answersRepository.Create(answer);

            var nextQuestion = new QuestionFinder(_questionsRepository).FindNext(answer.QuestionId);
            var response = (nextQuestion != null ? new Response(nextQuestion).Build() : ExitResponse);

            return Content(response.ToString(), "application/xml");
        }

        private static VoiceResponse ExitResponse
        {
            get
            {
                var response = new VoiceResponse();
                response.Say("Thanks for your time. Good bye");
                response.Hangup();
                return response;
            }
        }
    }
}