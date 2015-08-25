using System;
using System.Collections.Generic;
using System.Linq;
using AutomatedSurvey.Web.Models;
using AutomatedSurvey.Web.Models.Repository;

namespace AutomatedSurvey.Web.Test.Repositories
{
    public class InMemoryQuestionsRepository : IRepository<Question>
    {
        private readonly IList<Question> _db = new List<Question>();

        public void Create(Question question)
        {
            _db.Add(question);
        }

        public Question FirstOrDefault(Func<Question, bool> predicate)
        {
            return _db.FirstOrDefault(predicate);
        }

        public Question FirstOrDefault()
        {
            throw new NotImplementedException();
        }

        public Question Find(int id)
        {
            return _db.FirstOrDefault(question => question.Id == id);
        }

        public IEnumerable<Question> All()
        {
            return _db;
        }
    }
}
