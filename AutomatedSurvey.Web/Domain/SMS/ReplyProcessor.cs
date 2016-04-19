using System.Linq;
using System.Web.Mvc;
using AutomatedSurvey.Web.Models;
using AutomatedSurvey.Web.Models.Repository;
using Twilio.TwiML;

namespace AutomatedSurvey.Web.Domain.SMS
{
    public interface IReplyProcessor
    {
        TwilioResponse Process();
    }

    public class ReplyProcessor : IReplyProcessor
    {
        // private readonly string _message;
        // private readonly string _from;
        private readonly TrackedQuestion _trackedQuestion;
        private readonly IResponseCreator _responseCreator;
        private readonly IRepository<Survey> _surveyRepository;
        private readonly IRepository<Question> _questionRepository;

        public ReplyProcessor(
            // string message,
            // string from,
            ControllerContext controllerContext,
            IResponseCreator responseCreator,
            IRepository<Survey> surveyRepository,
            IRepository<Question> questionRepository)
        {
            // _message = message;
            // _from = from;
            _trackedQuestion = new TrackedQuestion(controllerContext);
            _responseCreator = responseCreator;
            _surveyRepository = surveyRepository;
            _questionRepository = questionRepository;
        }


        public TwilioResponse Process()
        {
            return _trackedQuestion.IsEmpty() ? ProcessInitialRequest() : ProcessSuccRequest();
        }

        private TwilioResponse ProcessInitialRequest()
        {
            var survey = _surveyRepository.FirstOrDefault();
            var firstQuestion = survey.Questions.FirstOrDefault();
            _trackedQuestion.StoreOrDestroy(firstQuestion);
            return _responseCreator.Create(firstQuestion);
        }

        private TwilioResponse ProcessSuccRequest()
        {
            var previousQuestion = _trackedQuestion.Fetch();
            // TODO: Create the answer.
            var nextQuestion = new QuestionFinder(_questionRepository).FindNext(previousQuestion.Id);
            _trackedQuestion.StoreOrDestroy(nextQuestion);
            return _responseCreator.Create(nextQuestion);
        }
    }
}