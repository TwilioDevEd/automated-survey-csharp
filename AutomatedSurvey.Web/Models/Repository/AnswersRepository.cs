using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomatedSurvey.Web.Models.Repository
{
    public class AnswersRepository : IRepository<Answer>
    {
        private readonly AutomatedSurveysContext _context = new AutomatedSurveysContext();

        public void Create(Answer answer)
        {
            _context.Answers.Add(answer);
            _context.SaveChanges();
        }

        public Answer FirstOrDefault(Func<Answer, bool> predicate)
        {
            return _context.Answers.FirstOrDefault(predicate);
        }

        public Answer FirstOrDefault()
        {
            throw new NotImplementedException();
        }

        public Answer Find(int id)
        {
            return _context.Answers.Find(id);
        }

        public IEnumerable<Answer> All()
        {
            return _context.Answers.ToList();
        }
    }
}