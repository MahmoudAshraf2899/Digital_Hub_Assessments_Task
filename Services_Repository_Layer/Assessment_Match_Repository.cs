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
    public class Assessment_Match_Repository : GenericRepository<edulmsContext, AssessmentMatch>, IAssessment_Match_Repository
    {
        private bool CheckIfAssessmentQuestionExist(long id)
        {
            
            var isExist = Context.AssessmentQuestions.AsNoTracking()
                                                                 .Where(c => c.Id == id)
                                                                 .Any();
            return isExist;
        }
        public AssessmentMatch? AddNewAssessmentMatch(DtoAddNewAssessmentMatch body)
        {
            if (body is not null)
            {
                var assessment_question_isExist = CheckIfAssessmentQuestionExist(body.assessment_question_id);
                if (assessment_question_isExist)
                {
                    if (body.AnswerIdKey.Length == 0 || body.AnswerIdKey.Length > 36)
                    {
                        return null;
                    }
                    else if (body.QuestionIdKey.Length == 0 || body.QuestionIdKey.Length > 36)
                    {
                        return null;
                    }
                    else
                    {
                        AssessmentMatch assessmentMatch = new AssessmentMatch()
                        {
                            QuestionIdKey = body.QuestionIdKey,
                            AnswerIdKey = body.AnswerIdKey,
                            Answer = body.answer,
                            Option = body.option,
                            QuestionId = body.assessment_question_id,
                            Order = body.order,
                            CreatedAt = DateTime.Now.Date,
                        };
                        Add(assessmentMatch);
                        Save();
                        return assessmentMatch;
                    }
                }
                return null;
            }
            return null;

        }
        public AssessmentMatch? EditAssessmentMatch(DtoEditAssessmentMatch body)
        {
            if (body is not null)
            {
                //#1:Get Assessment Match
                var assessmentMatch = FindBy(c => c.Id == body.id).FirstOrDefault();

                if (assessmentMatch is not null)
                {
                    var assessment_question_isExist = CheckIfAssessmentQuestionExist(body.assessment_question_id);
                    if (assessment_question_isExist)
                    {
                        assessmentMatch.Answer = body.answer;
                        assessmentMatch.Order = body.order;
                        assessmentMatch.AnswerIdKey = body.answer_id_key;
                        assessmentMatch.QuestionIdKey = body.question_id_key;
                        assessmentMatch.QuestionId = body.assessment_question_id;
                        assessmentMatch.UpdatedAt = DateTime.Now.Date;
                        Update(assessmentMatch);
                        Save();
                        return assessmentMatch;

                    }
                    return null;
                }
                return null;

            }
            return null;

        }
        public List<DtoGetAllAssessementsMatch> GetAllAssessmentsMatch()
        {
            var result = (from q in Context.AssessmentMatches.AsNoTracking()
                          select new DtoGetAllAssessementsMatch
                          {
                              id = q.Id,
                              answer = q.Answer,
                              option = q.Option,
                              order = q.Order,
                              answer_id_key = q.AnswerIdKey,
                              question_id_key = q.QuestionIdKey,
                              created_at = q.CreatedAt,
                              questionText = q.Question.Question,
                              updated_at = q.UpdatedAt,

                          }).OrderByDescending(c => c.id).ToList();
            return result;

        }

        public DtoGetAssessmentMatchById GetAssessmentMatchById(long id)
        {
            var result = (from q in Context.AssessmentMatches.AsNoTracking()
                          where q.Id == id
                          select new DtoGetAssessmentMatchById
                          {                               
                              answer = q.Answer,
                              option = q.Option,
                              order = q.Order,
                              answer_id_key = q.AnswerIdKey,
                              question_id_key = q.QuestionIdKey,
                              created_at = q.CreatedAt,
                              questionText = q.Question.Question,
                              updated_at = q.UpdatedAt,
                              

                          }).FirstOrDefault();
            return result;
        }
    }
}
