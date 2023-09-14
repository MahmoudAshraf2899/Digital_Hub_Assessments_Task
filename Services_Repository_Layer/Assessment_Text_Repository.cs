using Context_Layer.Models;
using Data_Services_Layer.DTOS;
using IServices_Repository_Layer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_Repository_Layer
{
    public class Assessment_Text_Repository : GenericRepository<edulmsContext, AssessmentText>, IAssessment_Text_Repository
    {
        private bool CheckIfAssessmentQuestionExist(long id)
        {
            
            var isExist = Context.AssessmentQuestions.AsNoTracking()
                                                                 .Where(c => c.Id == id)
                                                                 .Any();
            return isExist;
        }

        public AssessmentText? AddNewAssessmentText(DtoAddNewAssessmentText body)
        {
            if (body is not null)
            {
                var assessment_question_isExist = CheckIfAssessmentQuestionExist(body.assessment_question_id);
                if (assessment_question_isExist)
                {
                    AssessmentText assessmentText = new AssessmentText()
                    {
                        Order = body.order,
                        Answer = body.answer,
                        CreatedAt = DateTime.Now.Date,
                        QuestionId = body.assessment_question_id,
                    };
                    Add(assessmentText);
                    Save();
                    return assessmentText;
                }
                return null;
            }
            return null;
        }

        public AssessmentText? EditAssessmentText(DtoEditAssessmentText body)
        {
            if (body is not null)
            {
                //#1:Get Assessment Text
                var assessmentText = FindBy(c => c.Id == body.id).FirstOrDefault();

                if (assessmentText is not null)
                {
                    var assessment_question_isExist = CheckIfAssessmentQuestionExist(body.assessment_question_id);
                    if (assessment_question_isExist)
                    {
                        assessmentText.Answer = body.answer;
                        assessmentText.Order = body.order;
                        assessmentText.QuestionId = body.assessment_question_id;
                        assessmentText.UpdatedAt = DateTime.Now.Date;
                        Update(assessmentText);
                        Save();
                        return assessmentText;
                    }
                    return null;
                }
                return null;
            }
            return null;
        }


        public List<DtoGetAllAssessmentText> GetAllAssessmentText()
        {
            var result = (from q in Context.AssessmentTexts.AsNoTracking()
                          select new DtoGetAllAssessmentText
                          {
                              id = q.Id,
                              answer = q.Answer,
                              order = q.Order,
                              updated_at = q.UpdatedAt,
                              created_at = q.CreatedAt,
                              questionText = q.Question.Question,

                          }).OrderByDescending(c => c.id).ToList();
            return result;
        }

        public DtoGetAssessmentTextById GetAssessmentTextById(long id)
        {
            var result = (from q in Context.AssessmentTexts.AsNoTracking()
                          where q.Id == id
                          select new DtoGetAssessmentTextById
                          {
                              answer = q.Answer,
                              order = q.Order,
                              created_at = q.CreatedAt,
                              questionText = q.Question.Question,
                              updated_at = q.UpdatedAt,
                          }).FirstOrDefault();
            return result;
        }
    }
}
