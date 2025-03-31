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
            var result = from s in _northwind_DBContext.Customers where s.ContactName.StartsWith("A") select s;
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
    }
}
