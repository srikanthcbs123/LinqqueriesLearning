using LinqqueriesLearning.Models;
using LinqqueriesLearning.Northwind_Connect;
using LinqqueriesLearning.Northwind_DB_DBConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace LinqqueriesLearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NormalLinqqueresController : ControllerBase
    {
        private readonly NorthwindContext _northwindContext;
        private readonly NorthwindDbContext _northwind_DBContext;
        //ctor tab tab is the constructor  creation shortcut.
        //prop tab tab is the property creation shortcut.
        public NormalLinqqueresController(NorthwindContext northwindContext, NorthwindDbContext _northwind_DBContext)
        {
            this._northwindContext = northwindContext;
            this._northwind_DBContext = _northwind_DBContext;
        }
        
        [HttpGet]
        [Route("GetEmployeesData")]
        //2ndway of shortcutfor routing .
        //[HttpGet("GetEmployeesData")]
        public async Task<IActionResult> GetEmployeesData()
        {
            //Basic LinQ synatx:
            //var result=from variablename in datasource  (optional clause ) select variablename
            //here (optional clause ) means where clause,order by clause,Group by Clause...

            //here we are fetching all employess  data.

            //synatx://var result=from localvariablename in datasource  (optional clause ) select localvariablename
            //Note:abc means its a localvariablename
            //BELOW IS THE NORMAL LINQ QUERY
            var result = from abc in _northwind_DBContext.Employees select abc;
            //C#COMPILER CONVERT THIS LINQ QUERY TO THIS SQL QUERY FORMAT:select * from employees


            //the below written for json serialization refrence looping purpose written .net 8.0 to fix this refrence looping we are using this one.lower versions you will not get.
            var jsonSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(result, jsonSettings);
            //It converts your object data to jsonformat
            //  var objectTojsonstringConversion = JsonConvert.SerializeObject(result);

            //It converts your jsonstring to object format

            //var jsonstringToObjectConversion = JsonConvert.DeserializeObject(objectTojsonstringConversion);

            return StatusCode(StatusCodes.Status200OK, convertedData);

        }

        [HttpGet]
        [Route("GetEmployeesDatawith_CityWise")]
        public async Task<IActionResult> GetAllEmployeesDataCityWise()
        {
            //synatx://var result=from localvariablename in datasource  (optional clause ) select localvariablename   
            //it will return employee data with it department along with all the columns data
                 var result = from a in _northwind_DBContext.Employees where a.City == "London" select a;//linqquey 
            //SqlQuery:     //select * from  Employees where City='London'


            var jsonSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(result, jsonSettings);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }
        [HttpGet]
        [Route("GetEmployeesDatawith_CityWiseonlyname")]
        public async Task<IActionResult> GetrequiredNamesCityWise()
        {//here we are fetchingall the data and showing only one column only.
            var result = from a in _northwind_DBContext.Employees select new { EmployeeFullName = a.FirstName + a.LastName };
            //SqlQuery Format:select FirstName+LastName as 'EmployeeFullName' from  Employees 

            var jsonSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(result);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }

        [HttpGet]
        [Route("GetOrderDataWithNamestatswiths")]
        public async Task<IActionResult> GetDataByNamesStartswiths()
        {//here we are fetchingall employess  data WITH NAMES STARTS WITH S LETTER.
            var result = from s in _northwind_DBContext.Customers where s.ContactName.StartsWith("A") select s;//here select s means it will fetch the all the columns data.
            //SQLQUERY:select * from Customers where ContactName like 'A%'
            var jsonSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(result);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }

        [HttpGet]
        [Route("GetEmployees&DeptDataByUsingJoins")]
        public async Task<IActionResult> GetDataByUsingJoins()
        {
            //here we are fetching employess&DepartMenent with data by using joins and orderby descending with required columns.
            //sqlquery:select e.FirstName, e.LastName, e.City, d.DeptName from employee e join Departments d on d.Id=e.EmpId order by e.City desc
            var result = from e in _northwindContext.Employees join d in _northwindContext.Departments on e.EmpId equals d.Id orderby e.City descending 
                         select new { e.FirstName, e.LastName, e.City, d.DeptName };//HERE WE ARE SELCTING THE SPECIFIED COLUMNS
           
            var jsonSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(result);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }
        [HttpGet]
        [Route("take(number) Usage")]
        public async Task<IActionResult> TakeUsage()
        {
            //if you want to get the only first 5 records in a table use this take(number) method.
            //select top 10 * from customers
            var result = (from lstcustmer in _northwindContext.Customers select lstcustmer).Take(10);
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(result);
            return StatusCode(StatusCodes.Status200OK, convertedData);
        }

        [HttpGet]
        [Route("Skip(number) Usage")]
        public async Task<IActionResult> SkipUsage()
        {
            //if you want to get the only first 5 records in a table use this take(number) method.
            //after using the take() method you can use skip() method .
            //skip will skip or ignore the given count of records after taking the records.
            //select top 5 * from customers
            var result = (from lstcustmer in _northwindContext.Customers select lstcustmer).Take(5).Skip(4);
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(result);
            return StatusCode(StatusCodes.Status200OK, convertedData);
        }
        [HttpGet]
        [Route("AgeWithFilter")]
        public async Task<IActionResult> AgeWithFilter()
        {
           // List<int> OBJ = new List<int>();//IT WILL STORE INT DATA            
            //example with dummydata
            List<StudentData> lststudentsObj = new List<StudentData>()
            {//List with multiple objects declaring and assigning the data like this way
               new StudentData() { StudentID = 1, StudentName = "John", Age = 13} ,//this is one object
               new StudentData() { StudentID = 2, StudentName = "Moin",  Age = 21 } ,//this is one object
               new StudentData() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,//this is one object
               new StudentData() { StudentID = 4, StudentName = "Ram" , Age = 20} ,//this is one object
               new StudentData() { StudentID = 5, StudentName = "Ron" , Age = 15 }//this is one object
            };
            //synatx://var result=from localvariablename in datasource  (optional clause ) select localvariablename
            
            var filteredResult = from s in lststudentsObj
                                 where s.Age > 15 && s.Age <= 20  //here we are applying filter condition
                               //  select s   =>it will fetch all the columns data
                                 select new { StudentFullName = s.StudentName };//giving the alisaname
                                                                         //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(filteredResult);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }
        [HttpGet]
        [Route("OrderByusage")]
        public async Task<IActionResult> OrderbyUsage()
        {
            //example with dummydata
            List<StudentData> lststudentsObj = new List<StudentData>()
            {
               new StudentData() { StudentID = 1, StudentName = "John", Age = 13} ,
               new StudentData() { StudentID = 2, StudentName = "Moin",  Age = 21 } ,
               new StudentData() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,
               new StudentData() { StudentID = 4, StudentName = "Ram" , Age = 20} ,
               new StudentData() { StudentID = 5, StudentName = "Ron" , Age = 15 }
            };
            //synatx://var result=from localvariablename in datasource  (optional clause ) select localvariablename
            var orderByAscendingResult = from s in lststudentsObj
                                         orderby s.StudentName ascending
                                         select s;

            var orderByDescendingResult = from s in lststudentsObj
                                          orderby s.StudentName descending
                                          select s;
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(orderByDescendingResult);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }

        [HttpGet]
        [Route("GroupByusage")]
        public async Task<IActionResult> GroupByusage()
        {
            //example with dummydata
            List<StudentData> lststudentsObj = new List<StudentData>()
            {
               new StudentData() { StudentID = 1, StudentName = "John", Age = 13} ,
               new StudentData() { StudentID = 2, StudentName = "Moin",  Age = 13 } ,
               new StudentData() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,
               new StudentData() { StudentID = 4, StudentName = "Ram" , Age = 20} ,
               new StudentData() { StudentID = 5, StudentName = "Ron" , Age = 15 }
            };
            var groupedStudents = lststudentsObj.GroupBy(s => s.Age)
                                     .Select(g => new { Age = g.Key, Students = g.ToList() });
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(groupedStudents);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }
    }
}
