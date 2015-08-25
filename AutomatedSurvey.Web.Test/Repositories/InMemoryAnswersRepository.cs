using System;
using System.Collections.Generic;
using System.Linq;
using AutomatedSurvey.Web.Models;
using AutomatedSurvey.Web.Models.Repository;

namespace AutomatedSurvey.Web.Test.Repositories
{

    public class InMemoryAnswersRepository : IRepository<Answer>
    {
        private readonly IList<Answer> _db = new List<Answer>();

        public void Create(Answer answer)
        {
            _db.Add(answer);
        }

        public Answer FirstOrDefault(Func<Answer, bool> predicate)
        {
            return _db.FirstOrDefault(predicate);
        }

        public Answer FirstOrDefault()
        {
            throw new NotImplementedException();
        }

        public Answer Find(int id)
        {
            return _db.FirstOrDefault(answer => answer.Id == id);
        }

        public IEnumerable<Answer> All()
        {
            return _db;
        }
    }
}
