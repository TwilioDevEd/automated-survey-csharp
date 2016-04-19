using System.Xml.XPath;
using AutomatedSurvey.Web.Domain.SMS;
using AutomatedSurvey.Web.Models;
using NUnit.Framework;

#pragma warning disable 219 // Disable null assigment;

namespace AutomatedSurvey.Web.Test.Domain.SMS
{
    public class ResponseCreatorTest
    {
        [Test]
        public void WhenTheQuestionIsNull_ThenItCreatesAClosingMessage()
        {
            Question question = null;

            var responseCreator = new ResponseCreator();
            var response = responseCreator.Create(null);

            var data = response.ToXDocument();
            Assert.That(
                data.XPathSelectElement("Response/Message").Value,
                Is.EqualTo("Thanks for your time. Good bye"));
        }

        [TestCase(QuestionType.Voice, "Free Question")]
        [TestCase(QuestionType.Numeric, "Numeric Question")]
        [TestCase(QuestionType.YesNo, "Yes No Question")]
        public void WhenTheQuestionIsNotNull_ThenItCreatesAResponseWithTheAppropriateQuestion(
            QuestionType questionType, string questionBody  )
        {
            var question = new Question {Type = questionType, Body = questionBody};

            var responseCreator = new ResponseCreator();
            var response = responseCreator.Create(question);

            var data = response.ToXDocument();
            StringAssert.Contains(
                questionBody, data.XPathSelectElement("Response/Message").Value);
        }

        [TestCase(QuestionType.Voice, "Reply to this message with your answer")]
        [TestCase(QuestionType.Numeric, "Reply with a number from \"0\" to \"9\" to this message")]
        [TestCase(QuestionType.YesNo, "Reply with \"1\" for YES and \"0\" for NO to this message")]
        public void WhenTheQuestionIsNotNull_ThenItCreatesAResponseWithTheAppropriateInstruction(
            QuestionType questionType, string expectedInstructions)
        {
            var question = new Question {Type = questionType};

            var responseCreator = new ResponseCreator();
            var response = responseCreator.Create(question);

            var data = response.ToXDocument();
            StringAssert.Contains(
                expectedInstructions, data.XPathSelectElement("Response/Message").Value);
        }
    }
}