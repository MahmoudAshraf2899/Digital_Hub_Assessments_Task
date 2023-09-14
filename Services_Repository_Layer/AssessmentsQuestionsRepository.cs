using Context_Layer.Models;
using Data_Services_Layer.DTOS;
using IServices_Repository_Layer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_Repository_Layer
{
    public class AssessmentsQuestionsRepository : GenericRepository<edulmsContext, AssessmentQuestion>, IAssessmentsQuestionsRepository
    {
        public AssessmentQuestion? AddNewAssessmentQuestion(DtoAddNewAssessmentQuestion body)
        {
            if (body is not null)
            {
                AssessmentQuestion assessmentQuestion = new AssessmentQuestion()
                {
                    Question = body.question,
                    Type = body.type,
                    Level = body.level,
                    Order = body.order,
                    CreatedAt = DateTime.Now.Date
                };
                Add(assessmentQuestion);
                Save();
                return assessmentQuestion;
            }
            return null;
        }
        public AssessmentQuestion? EditAssessmentQuestion(DtoEditAssessmentQuestion body)
        {
            if (body is not null)
            {
                var assessmentQuestion = FindBy(c => c.Id == body.id).FirstOrDefault();
                if (assessmentQuestion is not null)
                {
                    assessmentQuestion.Order = body.order;
                    assessmentQuestion.Level = body.level;
                    assessmentQuestion.UpdatedAt = DateTime.Now.Date;
                    assessmentQuestion.Question = body.question;
                    assessmentQuestion.Type = body.type;

                    Update(assessmentQuestion);
                    Save();
                    return assessmentQuestion;
                }
                return null;
            }
            return null;
        }

        public List<DtoGetAllAssessmentsQuestions> GetAllAssessmentsQuestions()
        {
            var result = (from q in Context.AssessmentQuestions.AsNoTracking()
                          select new DtoGetAllAssessmentsQuestions
                          {
                              id = q.Id,
                              level = q.Level,
                              order = q.Order,
                              question = q.Question,
                              type = q.Type
                          }).OrderByDescending(c => c.id).ToList();
            return result;
        }

        public DtoGetAssessmentQuestionById GetAssessmentQuestionById(long id)
        {
            var result = (from q in Context.AssessmentQuestions.AsNoTracking()
                          where q.Id == id
                          select new DtoGetAssessmentQuestionById
                          {
                              level = q.Level,
                              order = q.Order,
                              question = q.Question,
                              type = q.Type
                          }).FirstOrDefault();
            return result;
        }
    }
}
