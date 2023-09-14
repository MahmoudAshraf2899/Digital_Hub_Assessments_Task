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
    public class AssessmentQuestionsRelationRepository : GenericRepository<edulmsContext, AssessmentQuestionsRelation>, IAssessmentQuestionsRelationRepository
    {
        private bool CheckIfAssessmentQuestionExist(long id)
        {
            var isExist = Context.AssessmentQuestions.AsNoTracking()
                                                     .Where(c => c.Id == id)
                                                     .Any();
            return isExist;
        }
        private bool CheckIfAssessmentExist(long id)
        {
            var isExist = Context.Assessments.AsNoTracking()
                                             .Where(c => c.Id == id)
                                             .Any();
            return isExist;
        }

        public AssessmentQuestionsRelation? AddNewAssessmentQuestionsRelation(DtoAddNewAssessmentQuestionsRelation body)
        {
            if (body is not null)
            {
                var assessment_question_isExist = CheckIfAssessmentQuestionExist(body.assessment_questions_id);
                var assessment__isExist = CheckIfAssessmentExist(body.assessment_id);
                if (assessment_question_isExist == true && assessment__isExist == true)
                {
                    AssessmentQuestionsRelation assessmentQuestionsRelation = new AssessmentQuestionsRelation
                    {
                        AssessmentId = body.assessment_id,
                        QuestionId = body.assessment_id,
                        CreatedAt = DateTime.Now.Date,
                    };
                    Add(assessmentQuestionsRelation);
                    Save();
                    return assessmentQuestionsRelation;
                }
                return null;
            }
            return null;
        }

        public AssessmentQuestionsRelation? EditAssessmentQuestionsRelation(DtoEditAssessmentQuestionsRelation body)
        {
            if (body is not null)
            {
                var assessmentsQuestionRelation = FindBy(c => c.Id == body.id).FirstOrDefault();
                if (assessmentsQuestionRelation is not null)
                {

                    var assessment_question_isExist = CheckIfAssessmentQuestionExist(body.assessment_questions_id);
                    var assessment__isExist = CheckIfAssessmentExist(body.assessment_id);
                    if (assessment_question_isExist == true && assessment__isExist == true)
                    {
                        assessmentsQuestionRelation.AssessmentId = body.assessment_id;
                        assessmentsQuestionRelation.QuestionId = body.assessment_questions_id;
                        assessmentsQuestionRelation.UpdatedAt = DateTime.Now.Date;

                        Update(assessmentsQuestionRelation);
                        Save();
                        return assessmentsQuestionRelation;
                    }
                    return null;
                }
                return null;

            }
            return null;

        }

        public List<DtoGetAllAssessmentQuestions> GetAllAssessmentQuestions()
        {
            var result = (from q in Context.AssessmentQuestionsRelations.AsNoTracking()
                          where q.Assessment.Published > 0 //To Get Published Assessments Only
                          select new DtoGetAllAssessmentQuestions
                          {
                              assessmentDescription = q.Assessment.Description,
                              assessmentDuration = q.Assessment.Duration,
                              assessmentShortDescription = q.Assessment.ShortDescription,
                              assessmentSlug = q.Assessment.Slug,
                              assessmentTitle = q.Assessment.Title,

                              type = q.Question.Type,
                              level = q.Question.Level,
                              order = q.Question.Order,
                              question = q.Question.Question,

                              created_at = q.CreatedAt,
                              updated_at = q.UpdatedAt,


                          }).OrderByDescending(c => c.id).ToList();
            return result;
        }

        public DtoGetAssessmentQuestionsById GetAssessmentQuestionsById(long id)
        {
            var result = (from q in Context.AssessmentQuestionsRelations.AsNoTracking()
                          where q.Id == id
                          select new DtoGetAssessmentQuestionsById
                          {
                              assessmentDescription = q.Assessment.Description,
                              assessmentDuration = q.Assessment.Duration,
                              assessmentShortDescription = q.Assessment.ShortDescription,
                              assessmentSlug = q.Assessment.Slug,
                              assessmentTitle = q.Assessment.Title,

                              type = q.Question.Type,
                              level = q.Question.Level,
                              order = q.Question.Order,
                              question = q.Question.Question,

                              created_at = q.CreatedAt,
                              updated_at = q.UpdatedAt,


                          }).FirstOrDefault();
            return result;
        }
    }
}
