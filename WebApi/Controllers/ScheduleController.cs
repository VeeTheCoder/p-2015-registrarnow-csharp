namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using IRepository;
    using POCO;
    using Repository;
    using Service;

    public class ScheduleController : ApiController
    {
        [HttpGet]
        public List<Schedule> GetScheduleList(string year, string quarter)
        {
            var service = new ScheduleService(new ScheduleRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetScheduleList(year, quarter, ref errors);
        }

        [HttpGet]
        public List<Schedule> GetScheduleList()
        {
            var service = new ScheduleService(new ScheduleRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetScheduleList(string.Empty, string.Empty, ref errors);
        }

        [HttpPost]
        public string InsertSchedule(Schedule schedule)
        {
            var errors = new List<string>();
            var repository = new ScheduleRepository();
            var service = new ScheduleService(repository);
            service.InsertSchedule(schedule, ref errors);
            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }
    }
}