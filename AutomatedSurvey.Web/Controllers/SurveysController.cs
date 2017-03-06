using System.Linq;
using System.Web.Mvc;
using AutomatedSurvey.Web.Models;
using AutomatedSurvey.Web.Models.Repository;
using Twilio.TwiML;

namespace AutomatedSurvey.Web.Controllers
{
    public class SurveysController : Controller
    {
        private readonly IRepository<Survey> _surveysRepository;
        private readonly IRepository<Answer> _answersRepository;

        public SurveysController() : this(new SurveysRespository(), new AnswersRepository()) { }

        public SurveysController(
            IRepository<Survey> surveysRepository, IRepository<Answer> answersRepository)
        {
            _surveysRepository = surveysRepository;
            _answersRepository = answersRepository;
        }

        // Webhook for Twilio survey number
        // GET: connectcall
        public ActionResult ConnectCall()
        {
            var response = new VoiceResponse();
            var survey = _surveysRepository.FirstOrDefault();
            var welcomeMessage = string.Format("Thank you for taking the {0} survey", survey.Title);

            response.Say(welcomeMessage);
            response.Redirect(Url.Action("find", "questions", new { id = 1 }));

            return Content(response.ToString(), "application/xml");
        }

        // GET: surveys/results
        public ActionResult Results()
        {
            var answers = _answersRepository.All();
            var uniqueAnswers = answers
                .Select(answer => answer.CallSid)
                .Distinct().ToList();

            ViewBag.UniqueAnswers = uniqueAnswers;
            ViewBag.SurveyTitle = _surveysRepository.FirstOrDefault().Title;
            return View(answers);
        }
    }
}