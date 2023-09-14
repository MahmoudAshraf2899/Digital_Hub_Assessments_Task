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
    public class AssessmentAnswerRepository : GenericRepository<edulmsContext, AssessmentAnswer>, IAssessmentAnswerRepository
    {
        public AssessmentAnswer? AddUserAnswers(DtoAddUserAnswers body)
        {
            if (body is not null)
            {
                AssessmentAnswer assessmentAnswer = new AssessmentAnswer()
                {
                    AssessmentId = body.assessmentId,
                    QuestionId = body.questionId,
                    CreatedAt = DateTime.Now.Date,
                    Answer = body.answer,
                    Score = body.score,
                    UserId = body.userId,
                };
                Add(assessmentAnswer);
                Save();
                return assessmentAnswer;
            }
            return null;

        }

        public AssessmentAnswer? EditUserAnswers(DtoEditUserAnswers body)
        {
            if (body is not null)
            {
                var assessmentAnswer = FindBy(c => c.Id == body.id).FirstOrDefault();
                if (assessmentAnswer is not null)
                {
                    assessmentAnswer.Answer = body.answer;
                    assessmentAnswer.Score = body.score;
                    Update(assessmentAnswer);
                    Save();
                    return assessmentAnswer;
                }
                return null;

            }
            return null;
        }

        public List<DtoGetAssessmentAnswersForUser> GetAssessmentAnswersForUser(long userId)
        {
            var result = (from q in Context.AssessmentAnswers.AsNoTracking()
                          where q.UserId == userId
                          select new DtoGetAssessmentAnswersForUser
                          {
                              answer = q.Answer,
                              assessmentDescription = q.Assessment.Description,
                              assessmentShortDescription = q.Assessment.ShortDescription,
                              assessmentTitle = q.Assessment.Title,
                              created_at = q.CreatedAt,
                              questionText = q.Question.Question,
                              score = q.Score,
                              updated_at = q.UpdatedAt,
                              userName = q.User.Username
                          }).OrderByDescending(c => c.score).ToList();
            return result;
        }

        public DtoAssessmentAnswersForUserWithSpecificAssessment GetAssessmentAnswersForUserWithSpecificAssessment(long userId, long assessmentId)
        {
            var result = (from q in Context.AssessmentAnswers.AsNoTracking()
                          where q.UserId == userId && q.AssessmentId == assessmentId
                          select new DtoAssessmentAnswersForUserWithSpecificAssessment
                          {
                              answer = q.Answer,
                              assessmentDescription = q.Assessment.Description,
                              assessmentShortDescription = q.Assessment.ShortDescription,
                              assessmentTitle = q.Assessment.Title,
                              created_at = q.CreatedAt,
                              questionText = q.Question.Question,
                              score = q.Score,
                              updated_at = q.UpdatedAt,
                          }).FirstOrDefault();
            return result;
        }
    }
}
