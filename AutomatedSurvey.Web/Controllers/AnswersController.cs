using System.Web.Mvc;
using AutomatedSurvey.Web.Domain;
using AutomatedSurvey.Web.Models;
using AutomatedSurvey.Web.Models.Repository;
using Twilio.TwiML;
using Twilio.TwiML.Mvc;

namespace AutomatedSurvey.Web.Controllers
{
    public class AnswersController : TwilioController
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
        public TwiMLResult Create(
            [Bind(Include = "QuestionId,RecordingUrl,Digits,CallSid,From")]
            Answer answer)
        {
            _answersRepository.Create(answer);

            var nextQuestion = new QuestionFinder(_questionsRepository).FindNext(answer.QuestionId);
            return TwiML(nextQuestion != null ? new Response(nextQuestion).Build() : ExitResponse);
        }

        private static TwilioResponse ExitResponse
        {
            get
            {
                var response = new TwilioResponse();
                response.Say("Thanks for your time. Good bye");
                response.Hangup();
                return response;
            }
        }
    }
}