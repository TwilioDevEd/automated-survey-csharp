using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AutomatedSurvey.Web.Models;

namespace AutomatedSurvey.Web.Domain.SMS
{
    public interface ITrackedQuestion
    {
        void StoreOrDestroy(Question question);

        Question Fetch();

        bool IsEmpty();

        bool IsPresent();
    }

    public class TrackedQuestion : ITrackedQuestion
    {
        private const string CookieName = "Question";

        private readonly ControllerContext _controllerContext;

        public TrackedQuestion(ControllerContext controllerContext)
        {
            _controllerContext = controllerContext;
        }

        public void StoreOrDestroy(Question question)
        {
            if (question == null)
            {
                Cookie.Expires = DateTime.Now.AddDays(-1);
                _controllerContext.HttpContext.Response.Cookies.Add(Cookie);
            }
            else
            {
                var cookie = new HttpCookie(CookieName)
                {
                    Value = Serialize(question)
                };

                _controllerContext.HttpContext.Response.Cookies.Add(cookie);
            }
        }

        public Question Fetch()
        {
            var cookie = Cookie;
            return Deserialize(cookie.Value);
        }

        public bool IsEmpty()
        {
            return Cookie == null;
        }

        public bool IsPresent()
        {
            return !IsEmpty();
        }

        private HttpCookie Cookie
        {
            get { return _controllerContext.HttpContext.Request.Cookies[CookieName]; }
        }

        private static string Serialize(Question question)
        {
            return new JavaScriptSerializer().Serialize(question);
        }

        private static Question Deserialize(string serializedQuestion)
        {
            return new JavaScriptSerializer().Deserialize<Question>(serializedQuestion);
        }
    }
}