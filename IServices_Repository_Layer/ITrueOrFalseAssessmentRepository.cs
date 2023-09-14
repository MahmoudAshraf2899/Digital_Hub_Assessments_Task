using Context_Layer.Models;
using Data_Services_Layer.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServices_Repository_Layer
{
    public interface ITrueOrFalseAssessmentRepository : IGenericRepository<AssessmentTrueFalse>
    {
        AssessmentTrueFalse? AddNewTrueFalseAssessment(DtoAddNewTrueFalseQuestion body);
        AssessmentTrueFalse? EditTrueFalseAssessment(DtoEditTrueFalseAssessment body);
        List<DtoGetAllTrueFalseAssessmentQuestions> GetAllTrueFalseAssessmentQuestions();
        DtoGetTrueFalseAssessmentById GetTrueFalseAssessmentQuestionById(long id);
    }
}
