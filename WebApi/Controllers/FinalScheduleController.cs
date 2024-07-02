namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using POCO;
    using Repository;
    using Service;

    public class FinalScheduleController : ApiController
    {
        [HttpGet]
        public List<FinalSchedule> GetFinalSchedule()
        {
            var errors = new List<string>();
            var repository = new FinalScheduleRepository();
            var service = new FinalScheduleService(repository);
            return service.GetFinalSchedule(ref errors);
        }

        [HttpGet]
        public FinalSchedule GetCourseFinalSchedule(int scheduleId)
        {
            var errors = new List<string>();
            var repository = new FinalScheduleRepository();
            var service = new FinalScheduleService(repository);
            return service.GetCourseFinalSchedule(scheduleId, ref errors);
        }

        [HttpPost]
        public string InsertFinalSchedule(FinalSchedule finalSchedule)
        {
            var errors = new List<string>();
            var repository = new FinalScheduleRepository();
            var service = new FinalScheduleService(repository);
            service.InsertFinalSchedule(finalSchedule, ref errors);
            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

      [HttpPost]
      public string UpdateFinalSchedule(FinalSchedule finalSchedule)
      {
          var errors = new List<string>();
          var repository = new FinalScheduleRepository();
          var service = new FinalScheduleService(repository);
          service.UpdateFinalSchedule(finalSchedule, ref errors);

          if (errors.Count == 0)
          {
              return "ok";
          }

          return "error";
      }

               [HttpPost]
               public string DeleteFinalSchedule(int scheduleId)
              {
                  var errors = new List<string>();
                  var repository = new FinalScheduleRepository();
                  var service = new FinalScheduleService(repository);
                  service.DeleteSchedule(scheduleId, ref errors);

                  if (errors.Count == 0)
                  {
                      return "ok";
                  }

                  return "error";
              }
    }
}
