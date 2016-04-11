using System.Collections.Generic;
using AutomatedSurvey.Web.Models;
using Twilio.TwiML;

namespace AutomatedSurvey.Web.Domain.SMS
{
    public interface IResponseCreator
    {
        TwilioResponse Create();
    }

    public class ResponseCreator : IResponseCreator
    {
        private readonly Question _question;

        public ResponseCreator(Question question)
        {
            _question = question;
        }

        public static IDictionary<QuestionType, string> Instructions
        {
            get
            {
                return new Dictionary<QuestionType, string>
                {
                    {QuestionType.Voice, "Reply to this message with your answer"},
                    {QuestionType.Numeric, "Reply with a number from \"0\" to \"9\" to this message"},
                    {QuestionType.YesNo, "Reply with \"1\" for YES and \"0\" for NO to this message"}
                };
            }
        }

        public TwilioResponse Create()
        {
            if (_question == null)
            {
                return ExitMessage();
            }

            var response = new TwilioResponse();
            response.Message(MessageBody());

            return response;
        }

        private string MessageBody()
        {
            return string.Format("{0}\n\n{1}", _question.Body, Instructions[_question.Type]);
        }

        private static TwilioResponse ExitMessage()
        {
            var response = new TwilioResponse();
            response.Message("Thanks for your time. Good bye");

            return response;
        }
    }
}