using Context_Layer.Models;
using Data_Services_Layer.DTOS;
using IServices_Repository_Layer;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_Repository_Layer
{
    public class AssessmentsRepository : GenericRepository<edulmsContext, Assessment>, IAssessmentsRepository
    {

        public Assessment? AddNewAssessment(DtoAddNewAssessment body, long userId)
        {
            if (body is not null)
            {
                Assessment NewAssessment = new Assessment()
                {
                    CreatedAt = DateTime.Now.Date,
                    //CreatedBy = userId,//Created By Is not long here ??
                    Description = body.description,
                    ShortDescription = body.description,
                    CategoryId = body.category_id,
                    Duration = body.duration,
                    Published = body.published,
                    Slug = "Added",
                    Title = body.title,

                };
                Add(NewAssessment);
                Save();
                return NewAssessment;
            }
            return null;
        }

        public Assessment? EditAssessment(DtoEditAssessments body)
        {
            if (body is not null)
            {
                var assessment = FindBy(c => c.Id == body.assessmentId).FirstOrDefault();
                if (assessment is not null)
                {
                    assessment.Slug = body.slug;
                    assessment.Title = body.title;
                    assessment.Description = body.description;
                    assessment.ShortDescription = body.description;
                    assessment.Published = body.published;
                    assessment.Duration = body.duration;
                    assessment.UpdatedAt = DateTime.Now.Date;

                    Update(assessment);
                    Save();
                    return assessment;

                }
                return null;
            }
            return null;
        }
        public List<DtoGetAllAssessments> GetAllPublishedAssessments()
        {
            var result = (from q in Context.Assessments.AsNoTracking()
                          where q.Published > 0

                          select new DtoGetAllAssessments
                          {
                              Id = q.Id,
                              created_at = q.CreatedAt,
                              description = q.Description,
                              short_description = q.Description,
                              slug = q.Slug,
                              updated_at = q.UpdatedAt,
                              duration = q.Duration
                          }).OrderByDescending(c => c.Id).ToList();
            return result;
        }
        public DtoGetAssessmentById GetAssessmentById(long id)
        {
            var result = (from q in Context.Assessments.AsNoTracking()
                          where q.Id == id
                          select new DtoGetAssessmentById
                          {
                              Id = q.Id,
                              title = q.Title,
                              createdAt = q.CreatedAt,
                              description = q.Description,
                              short_description = q.Description,
                              slug = q.Slug,
                              updatedAt = q.UpdatedAt,
                              duration = q.Duration,
                              isPublishedStatus = q.Published == 0 ? "Published" : "Not Published"
                          }).FirstOrDefault();
            return result;
        }
    }
}
