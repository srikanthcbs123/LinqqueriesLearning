using LinqqueriesLearning.Models;
using Microsoft.AspNetCore.Mvc;

namespace LinqqueriesLearning.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("AllAnyContainssum_usage")]
        public async Task<IActionResult> FirstOrDefault_First_Usage_All()
        {
            IList<StudentData> studentList = new List<StudentData>() {
            new StudentData() { StudentID = 1, StudentName = "John", Age = 13 } ,
            new StudentData() { StudentID = 2, StudentName = "Moin",  Age = 21 } ,
            new StudentData() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,
            new StudentData() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
            new StudentData() { StudentID = 5, StudentName = "Ron" , Age = 15 }
            };

            // checks whether all the students are teenagers    
            var areAllStudentsTeenAger = studentList.All(s => s.Age > 12 && s.Age < 20);
            // checks whether any of the students is teenager   
            bool isAnyStudentTeenAger = studentList.Any(s => s.Age > 12 && s.Age < 20);

            IList<int> intList = new List<int>() { 1, 2, 3, 4, 5 };
            bool result = intList.Contains(10);

            //
            IList<int> intList1 = new List<int>() { 10, 21, 30, 45, 50, 87 };

            var total = intList1.Sum();




            return Ok();
        }

        [HttpGet]
        [Route("Intersect&Union")]
        public async Task<IActionResult> IntersectUnionUsage()
        {
            IList<string> strList1 = new List<string>() { "One", "Two", "Three", "Four", "Five" };
            IList<string> strList2 = new List<string>() { "Four", "Five", "Six", "Seven", "Eight" };

            var Intersectresult = strList1.Intersect(strList2);//It will return matching items from the both the lists

            var Unionresult = strList1.Union(strList2);


            return Ok();
        }
    }
}
