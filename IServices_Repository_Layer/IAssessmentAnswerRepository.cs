using Context_Layer.Models;
using Data_Services_Layer.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices_Repository_Layer
{
    public interface IAssessmentAnswerRepository : IGenericRepository<AssessmentAnswer>
    {
        AssessmentAnswer? AddUserAnswers(DtoAddUserAnswers body);
        AssessmentAnswer? EditUserAnswers(DtoEditUserAnswers body);
        List<DtoGetAssessmentAnswersForUser> GetAssessmentAnswersForUser(long userId);
        DtoAssessmentAnswersForUserWithSpecificAssessment GetAssessmentAnswersForUserWithSpecificAssessment(long userId,long assessmentId);
    }
}
