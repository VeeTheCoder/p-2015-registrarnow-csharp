namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using POCO;
    using Repository;
    using Service;

    public class GradeDistributionController : ApiController
    {
        [HttpGet]
        public List<GradeDistribution> GetGradeDistribution()
        {
            var errors = new List<string>();
            var repository = new GradeDistributionRepository();
            var service = new GradeDistributionService(repository);
            return service.GetGradeDistribution(ref errors);
        }

        [HttpGet]
        public GradeDistribution GetCourseGradeDistribution(int scheduleId)
        {
            var errors = new List<string>();
            var repository = new GradeDistributionRepository();
            var service = new GradeDistributionService(repository);
            return service.GetCourseGradeDistribution(scheduleId, ref errors);
        }

      [HttpPost]
        public string InsertGradeDistribution(GradeDistribution gradeDistribution)
        {
            var errors = new List<string>();
            var repository = new GradeDistributionRepository();
            var service = new GradeDistributionService(repository);
            service.InsertGradeDistribution(gradeDistribution, ref errors);
            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

      [HttpPost]
      public string UpdateGradeDistribution(GradeDistribution gradeDistribution)
      {
          var errors = new List<string>();
          var repository = new GradeDistributionRepository();
          var service = new GradeDistributionService(repository);
          service.UpdateGradeDistribution(gradeDistribution, ref errors);

          if (errors.Count == 0)
          {
              return "ok";
          }

          return "error";
      }

               [HttpPost]
               public string DeleteGradeDistribution(int scheduleId)
              {
                  var errors = new List<string>();
                  var repository = new GradeDistributionRepository();
                  var service = new GradeDistributionService(repository);
                  service.DeleteSchedule(scheduleId, ref errors);

                  if (errors.Count == 0)
                  {
                      return "ok";
                  }

                  return "error";
              }
    }
}
