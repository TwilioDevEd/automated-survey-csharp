using System.Collections.Generic;
using AutomatedSurvey.Web.Models;
using Twilio.TwiML;

namespace AutomatedSurvey.Web.Domain
{
    public class Response
    {
        private readonly Question _question;

        public Response(Question question)
        {
            _question = question;
        }

        public static IDictionary<QuestionType, string> QuestionTypeToMessage
        {
            get
            {
                return new Dictionary<QuestionType, string>
                {
                    {QuestionType.Voice, "Please record your answer after the beep and then hit the pound sign"},
                    {QuestionType.Numeric, "Please press a number between 0 and 9 and then hit the pound sign"},
                    {QuestionType.YesNo, "Please press the 1 for yes and the 0 for no and then hit the pound sign"}
                };
            }
        }

        /// <summary>
        /// Builds an instance.
        /// </summary>
        /// <returns>A new instance of the TwilioResponse</returns>
        public TwilioResponse Build()
        {
            var response = new TwilioResponse();
            response.Say(_question.Body);
            response.Say(QuestionTypeToMessage[_question.Type]);
            AddRecordOrGatherCommands(response);

            return response;
        }

        private void AddRecordOrGatherCommands(TwilioResponse response)
        {
            var questionType = _question.Type;
            switch (questionType)
            {
                case QuestionType.Voice:
                    response.Record(new { action = GenerateUrl(_question) });
                    break;
                case QuestionType.Numeric:
                case QuestionType.YesNo:
                    response.Gather(new { action = GenerateUrl(_question) });
                    break;
            }
        }

        private static string GenerateUrl(Question question)
        {
            return string.Format("/answers/create?questionId={0}", question.Id);
        }
    }
}