using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomatedSurvey.Web.Models.Repository
{
    public class SurveysRespository : IRepository<Survey>
    {
        private readonly AutomatedSurveysContext _context = new AutomatedSurveysContext();

        public void Create(Survey entity)
        {
            throw new NotImplementedException();
        }

        public Survey FirstOrDefault(Func<Survey, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Survey FirstOrDefault()
        {
            return _context.Surveys.FirstOrDefault();
        }

        public Survey Find(int id)
        {
            return _context.Surveys.Find(id);
        }

        public IEnumerable<Survey> All()
        {
            throw new NotImplementedException();
        }
    }
}