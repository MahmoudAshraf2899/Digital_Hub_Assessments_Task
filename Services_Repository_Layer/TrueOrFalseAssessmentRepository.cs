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
    public class TrueOrFalseAssessmentRepository : GenericRepository<edulmsContext, AssessmentTrueFalse>, ITrueOrFalseAssessmentRepository
    {
        private bool CheckIfAssessmentQuestionExist(long id)
        {
            
            var isExist = Context.AssessmentQuestions.AsNoTracking()
                                                                 .Where(c => c.Id == id)
                                                                 .Any();
            return isExist;
        }
        public AssessmentTrueFalse? AddNewTrueFalseAssessment(DtoAddNewTrueFalseQuestion body)
        {
            if (body is not null)
            {
                //Get assessment_questions id
                var assessment_question_Exist = CheckIfAssessmentQuestionExist(body.assessment_questions_Id);
                if (assessment_question_Exist == true)//If Exist
                {
                    AssessmentTrueFalse question = new AssessmentTrueFalse
                    {
                        IsTrue = body.is_true,
                        QuestionId = body.assessment_questions_Id,
                        CreatedAt = DateTime.Now,
                    };
                    Add(question);
                    Save();
                    return question;
                }
                return null;
            }
            return null;
        }
        public AssessmentTrueFalse? EditTrueFalseAssessment(DtoEditTrueFalseAssessment body)
        {
            if (body is not null)
            {
                var trueOrFalseAssessment = FindBy(c => c.Id == body.id).FirstOrDefault();
                if (trueOrFalseAssessment is not null)
                {
                    var assessment_question_Exist = CheckIfAssessmentQuestionExist(body.assessment_questions_Id);
                    if (assessment_question_Exist == true)//If Exist
                    {
                        trueOrFalseAssessment.IsTrue = body.is_true;
                        trueOrFalseAssessment.QuestionId = body.assessment_questions_Id;
                        trueOrFalseAssessment.UpdatedAt = DateTime.Now.Date;

                        Update(trueOrFalseAssessment);
                        Save();
                        return trueOrFalseAssessment;
                    }
                    else
                    {
                        return null;
                    }

                }
                return null;

            }
            return null;
        }
        public List<DtoGetAllTrueFalseAssessmentQuestions> GetAllTrueFalseAssessmentQuestions()
        {
            var result = (from q in Context.AssessmentTrueFalses.AsNoTracking()
                          select new DtoGetAllTrueFalseAssessmentQuestions
                          {
                              id = q.Id,
                              assessment_questions_Id = q.QuestionId,
                              created_at = q.CreatedAt,
                              updated_at = q.UpdatedAt,
                              is_true = q.IsTrue,
                          }).OrderByDescending(c => c.id).ToList();
            return result;
        }
        public DtoGetTrueFalseAssessmentById GetTrueFalseAssessmentQuestionById(long id)
        {
            var result = (from q in Context.AssessmentTrueFalses.AsNoTracking()
                          where q.Id == id
                          select new DtoGetTrueFalseAssessmentById
                          {
                              id = q.Id,
                              assessment_question_id = q.QuestionId,
                              created_at = q.CreatedAt,
                              questionText = q.Question.Question,
                          }).FirstOrDefault();
            return result;
        }
    }
}
