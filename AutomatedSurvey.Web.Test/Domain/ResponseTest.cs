using AutomatedSurvey.Web.Domain;
using AutomatedSurvey.Web.Models;
using NUnit.Framework;

namespace AutomatedSurvey.Web.Test.Domain
{
    public class ResponseTest
    {
        [Test]
        public void Build_a_response_using_Record_if_the_question_type_is_Voice()
        {
            var question = new Question
            {
                Id = 1,
                Body = "How's the weather?",
                Type = QuestionType.Voice
            };
            var response = new Response(question).Build();
            var expectedResponse = string.Format(
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
                "<Response>\r\n" +
                "  <Say>{0}</Say>\r\n" +
                "  <Say>{1}</Say>\r\n" +
                "  <Record action=\"/answers/create?questionId={2}\"></Record>\r\n" +
                "</Response>", question.Body, Response.QuestionTypeToMessage[question.Type], question.Id);

            Assert.That(response.ToString(), Is.EqualTo(expectedResponse));
        }

        [TestCase(1, "On a scale of 0 to 9 how much do you like ice cream?", QuestionType.Numeric)]
        [TestCase(2, "Do you like my classical voice? 1 for yes and 0 for no", QuestionType.YesNo)]
        public void Build_a_response_using_Gather_if_the_question_type_is_Numeric_or_YesNo(
            int id, string body, QuestionType type)
        {
            var question = new Question { Id = id, Body = body, Type = type };
            var response = new Response(question).Build();
            var expectedResponse = string.Format(
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
                "<Response>\r\n" +
                "  <Say>{0}</Say>\r\n" +
                "  <Say>{1}</Say>\r\n" +
                "  <Gather action=\"/answers/create?questionId={2}\"></Gather>\r\n" +
                "</Response>", question.Body, Response.QuestionTypeToMessage[question.Type], question.Id);

            Assert.That(response.ToString(), Is.EqualTo(expectedResponse));
        }
    }
}
