using Context_Layer.Models;
using Data_Services_Layer.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices_Repository_Layer
{
    public interface IAssessmentsQuestionsRepository:IGenericRepository<AssessmentQuestion>
    {
        AssessmentQuestion? AddNewAssessmentQuestion(DtoAddNewAssessmentQuestion body);
        AssessmentQuestion? EditAssessmentQuestion(DtoEditAssessmentQuestion body);
        List<DtoGetAllAssessmentsQuestions> GetAllAssessmentsQuestions();
        DtoGetAssessmentQuestionById GetAssessmentQuestionById(long id);
    }
}
