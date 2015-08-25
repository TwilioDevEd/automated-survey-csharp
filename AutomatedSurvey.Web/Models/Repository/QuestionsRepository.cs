using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomatedSurvey.Web.Models.Repository
{
    public class QuestionsRepository : IRepository<Question>
    {
        private readonly AutomatedSurveysContext _context = new AutomatedSurveysContext();

        public void Create(Question question)
        {
            _context.Questions.Add(question);
            _context.SaveChanges();
        }

        public Question FirstOrDefault(Func<Question, bool> predicate)
        {
            return _context.Questions.FirstOrDefault(predicate);
        }

        public Question FirstOrDefault()
        {
            throw new NotImplementedException();
        }

        public Question Find(int id)
        {
            return _context.Questions.Find(id);
        }

        public IEnumerable<Question> All()
        {
            return _context.Questions.ToList();
        }
    }
}