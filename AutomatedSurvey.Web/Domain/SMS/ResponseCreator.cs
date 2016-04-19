using System.Collections.Generic;
using AutomatedSurvey.Web.Models;
using Twilio.TwiML;

namespace AutomatedSurvey.Web.Domain.SMS
{
    public interface IResponseCreator
    {
        TwilioResponse Create(Question question);
    }

    public class ResponseCreator : IResponseCreator
    {
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

        public TwilioResponse Create(Question question)
        {
            if (question == null)
            {
                return ExitMessage();
            }

            var response = new TwilioResponse();
            response.Message(MessageBody(question));

            return response;
        }

        private static string MessageBody(Question question)
        {
            return string.Format("{0}\n\n{1}", question.Body, Instructions[question.Type]);
        }

        private static TwilioResponse ExitMessage()
        {
            var response = new TwilioResponse();
            response.Message("Thanks for your time. Good bye");

            return response;
        }
    }
}