using System.Data.Entity.Migrations;
using AutomatedSurvey.Web.Models;

namespace AutomatedSurvey.Web.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AutomatedSurveysContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AutomatedSurveysContext context)
        {
            context.Surveys.AddOrUpdate(
                survey => new { survey.Id, survey.Title },
                new Survey { Id = 1, Title = "Twilio" });

            context.SaveChanges();

            context.Questions.AddOrUpdate(
                question => new { question.Body, question.Type, question.SurveyId },
                new Question
                {
                    Body = "Hello. Thanks for taking the Twilio Developer Education survey. On a scale of 0 to 9 how would you rate this tutorial?",
                    Type = QuestionType.Numeric,
                    SurveyId = 1
                },
                new Question
                {
                    Body = "On a scale of 0 to 9 how would you rate the design of this tutorial?",
                    Type = QuestionType.Numeric,
                    SurveyId = 1
                },
                new Question
                {
                    Body = "In your own words please describe your feelings about Twilio right now? Press the pound sign when you are finished.",
                    Type = QuestionType.Voice,
                    SurveyId = 1
                },
                new Question
                {
                    Body = "Do you like my voice? Please be honest, I dislike liars.",
                    Type = QuestionType.YesNo,
                    SurveyId = 1
                });

            context.SaveChanges();
        }
    }
}
