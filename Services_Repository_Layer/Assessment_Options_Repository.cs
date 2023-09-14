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
    public class Assessment_Options_Repository : GenericRepository<edulmsContext, AssessmentOption>, IAssessment_Options_Repository
    {
        private bool CheckIfAssessmentQuestionExist(long id)
        {
            
            var isExist = Context.AssessmentQuestions.AsNoTracking()
                                                                 .Where(c => c.Id == id)
                                                                 .Any();
            return isExist;
        }
        public AssessmentOption? AddNewAssessmentOption(DtoAddNewAssessmentOption body)
        {
            if (body is not null)
            {
                var assessment_question_isExist = CheckIfAssessmentQuestionExist(body.assessment_question_id);
                if (assessment_question_isExist)
                {
                    AssessmentOption assessmentOption = new AssessmentOption()
                    {
                        Correct = body.correct,
                        CreatedAt = DateTime.Now.Date,
                        Option = body.option,
                        Order = body.order,
                        QuestionId = body.assessment_question_id,
                    };
                    Add(assessmentOption);
                    Save();
                    return assessmentOption;
                }
                return null;
            }
            return null;
        }

        public AssessmentOption? EditAssessmentOption(DtoEditAssessmentOption body)
        {
            if (body is not null)
            {
                //#1:Get Assessment Option
                var assessmentOption = FindBy(c => c.Id == body.id).FirstOrDefault();

                if (assessmentOption is not null)
                {
                    var assessment_question_isExist = CheckIfAssessmentQuestionExist(body.assessment_question_id);
                    if (assessment_question_isExist)
                    {
                        assessmentOption.Option = body.option;
                        assessmentOption.UpdatedAt = DateTime.Now.Date;
                        assessmentOption.QuestionId = body.assessment_question_id;
                        assessmentOption.Correct = body.correct;
                        assessmentOption.Order = body.order;

                        Update(assessmentOption);
                        Save();
                        return assessmentOption;
                    }
                    return null;
                }
                return null;

            }
            return null;

        }

        public List<DtoGetAllAssessmentOptions> GetAllAssessmentOptions()
        {
            var result = (from q in Context.AssessmentOptions.AsNoTracking()
                          select new DtoGetAllAssessmentOptions
                          {
                              id = q.Id,
                              correct = q.Correct,
                              option = q.Option,
                              order = q.Order,
                              questionText = q.Question.Question
                          }).OrderByDescending(c => c.id).ToList();
            return result;

        }

        public DtoGetAssessmentOptionById GetAssessmentOptionsById(long id)
        {
            var result = (from q in Context.AssessmentOptions.AsNoTracking()
                          where q.Id == id
                          select new DtoGetAssessmentOptionById
                          {
                              correct = q.Correct,
                              option = q.Option,
                              order = q.Order,
                              questionText = q.Question.Question
                          }).FirstOrDefault();
            return result;
        }
    }
}
