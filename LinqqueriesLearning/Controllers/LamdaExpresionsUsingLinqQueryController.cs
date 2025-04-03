using LinqqueriesLearning.Northwind_Connect;
using LinqqueriesLearning.Northwind_DB_DBConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LinqqueriesLearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LamdaExpresionsUsingLinqQueryController : ControllerBase
    {
        NorthwindDbContext _northwind_DBContext;
        NorthwindContext _northwindContext;

        public LamdaExpresionsUsingLinqQueryController(NorthwindDbContext northwind_DBContext, NorthwindContext northwindContext)
        {
            _northwind_DBContext = northwind_DBContext;
            _northwindContext = northwindContext;
        }

        [HttpGet]
        [Route("GetEmployeesData")]
        //2nd of shortcutfor routing 
        //[HttpGet("GetEmployeesData")]
        //Example: Fetching All Records from employee table example
        public async Task<IActionResult> GetEmployeesData()
        {
            //Basic LamdaLinQ synatx is
            //A lambda expression is written using the => lamda operator
            //lamda expressions will reduce the normal linq query synatx.
            //now a days in realtime we are using this lamda expressions with linq.

            //Normal LinqQuery:  var result = from abc in _northwind_DBContext.Employees select abc;
            var result2= from abc in _northwind_DBContext.Employees select abc;//normal linq query
            //Lamda expression Linq queryis below for fetching data from employee
            var result = _northwind_DBContext.Employees.ToList(); //Lamda expression query, Returns all employees data with all columns.

            //sqlqueryconverted by compiler:select * from employees
            //the below written for json serialization refrence looping purpose written .net 8.0 to fix this refrence looping we are using this one.lower versions you will not get.
            var jsonSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(result, jsonSettings);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }
        [HttpGet]
        [Route("GetEmployeesDatawith_CityWise")]
        public async Task<IActionResult> GetAllEmployeesDataCityWise()
        {

            //Normal LINQ QUERY:var result = from a in _northwind_DBContext.Employees where a.City == "London" select a;//linqquey 
            //SqlQuery:     //select * from  Employees where City='London'
            //LAMDA EXPRESSION USING LINQ query:        //ToList() method will fetch the all the data.
            var result = _northwind_DBContext.Employees.Where(ab => ab.City == "London").ToList();//=>we called lamda opertor
                                                                                                //(parameters) => expression
                                                                                                //here expression is a anoymous function.these functions we used in lamda expressions
            var jsonSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(result);
            return StatusCode(StatusCodes.Status200OK, convertedData);
            //linq query         // var highEarners = context.Employees.Where(e => e.Salary > 50000).ToList(); 
            // Filters employees with salary > 50,000
            //sqlquery :select * from Employees where Salary > 50000
            //.ToList() means take/fetch the total records.
        }
        [HttpGet]
        [Route("GetEmployeesDatawith_CityWise_MultipleAnd ConditionsUage")]
        public async Task<IActionResult> GetAllEmployeesDataCityWise_MultipleAndConditionsUage()
        {

            //Normal LINQ QUERY:var result = from a in _northwind_DBContext.Employees where a.City == "London" select a;//linqquey 
            //SqlQuery:     //select * from  Employees where City=='London'and Country == "UK" and Title == "Sales Manager" 
            //LAMDA EXPRESSION USING LINQ query:
            var result = _northwind_DBContext.Employees.Where(a => a.City == "London" && a.Country == "UK" && a.Title == "Sales Manager").ToList();//=>we called lamda opertor
                                                                                                                                                   //(parameters) => expression
                                                                                                                                                   //here expression is a anoymous function.these functions we used in lamda expressions
            var jsonSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(result);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }
        [HttpGet]
        [Route("GetEmployeesDatawith_ReuiredColumnsonlyShowing")]
        public async Task<IActionResult> GetEmployeesDatawith_ReuiredColumnsonlyShowing()
        {//here we are fetchingall the data and showing only one column only.
         //normal linq query:var result = from a in _northwind_DBContext.Employees select new { EmployeeFullName = a.FirstName + a.LastName };
         //SqlQuery Format:select FirstName+LastName as 'EmployeeFullName' from  Employees 
         //Lamda Expression With Linq query:Thebelow LinQ Quer we will get therequired colmns only.
            var result = _northwind_DBContext.Employees.Select(e => new { e.FirstName, e.LastName, e.Address, e.City }).ToList();
            var jsonSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(result);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }
        [HttpGet]
        [Route("GetOrderDataWithNamestatswiths")]
        public async Task<IActionResult> GetDataByNamesStartswiths()
        {//here we are fetchingall employess  data.
         //normal linq query:
         var result2 = from s in _northwind_DBContext.Customers where s.ContactName.StartsWith("A") select s;
         //lamda expression linq query like below.
            var result = _northwind_DBContext.Customers.Where(a => a.ContactName.StartsWith("A")).ToList();
            //SQLQUERY:select * from Customers where ContactName like 'A%'
            var jsonSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(result);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }
        [HttpGet]
        [Route("OrderByusage")]
        public async Task<IActionResult> OrderbyUsage()
        {
            /*
             //Normal linq queries with ascending order and descending order write like this.
               var orderByAscendingResult = from s in lststudentsObj
                                            orderby s.StudentName ascending
                                            select s;//it will Show the  total columns 
                                                     //Select new{StudentId,Age}//you can also select required columns
               var orderByDescendingResult = from s in lststudentsObj
                                             orderby s.StudentName descending
                                             select s;//here we are fetching the all the data.
            //Here  select s   take The total records.
            //In Lamda Expesions same thing can achieve by .ToList() Predfiend mehod.
            */
            //ascending order/descending order  lamda expresion linq query.
            //sqlquery :Select * from Employees order by ContactName
            //sqlquery :Select * from Employees order by ContactName desc
            var orderByAscendingResult = _northwind_DBContext.Customers.OrderBy(e => e.ContactName).ToList();//ascending order
            var orderByDescendingResult = _northwind_DBContext.Customers.OrderByDescending(e => e.ContactName).ToList();//descending order

            //Order by appling on multile columns combinations.
            //sqlquery :Select * from Employees order by FirstName,LastName

            var orderByonMultipleColumns = _northwind_DBContext.Employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName).ToList();

            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(orderByDescendingResult);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }
        [HttpGet]
        [Route("GroupByusageWithOrginalSingleTable")]
        public async Task<IActionResult> GroupByusage()
        {

            //Sql Groupby Query:select   CompanyName as CompanyName,Count(*) as Count     from Customers group by CompanyName
            var groupedCompanyNameData = _northwind_DBContext.Customers.GroupBy(s => s.CompanyName)
                                     .Select(g => new { CompanyName = g.Key, CompanyName1 = g.ToList() });
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(groupedCompanyNameData);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }
        [HttpGet]
        [Route("Include usage ")]
        public async Task<IActionResult> IncludeUsage()
        {//Inprogress this example.you can apply pk,fk to employee and department table.

            //Sql Groupby Query:select   CompanyName as CompanyName,Count(*) as Count     from Customers group by CompanyName
           //  var employees = _northwind_DBContext.Employees.Include(e => e.Department).Where(e => e.Department.Name == "HR").ToList();
            //Here you should use the primary key and Foreign key combination then only include will work

            var employees = _northwind_DBContext.Employees.Where(e => e.City == "London");
            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(employees);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }
        [HttpGet]
        [Route("GetEmployees&DepartmentDataByUsingJoins")]
        public async Task<IActionResult> GetDataByUsingJoins()
        {
            /*    The below Query is the normal linq query applying  to join
           
            //here we are fetching employess&DepartMenent with data by using joins and orderby descending with required columns.
            //sqlquery:select e.FirstName, e.LastName, e.City, d.DeptName from employee e join Departments d on d.Id=e.EmpId order by e.City desc
            var result = from e in _northwindContext.Employees
                         join d in _northwindContext.Departments
                         on e.EmpId equals d.Id orderby e.City descending
                         select new { e.FirstName, e.LastName, e.City, d.DeptName };
            var jsonSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            */

            //LinQ Query with Lamdaexpressions by using joins.it reduces the join synatx also.
            var employeeDepartments = _northwind_DBContext.Employees
                                   .Join(_northwind_DBContext.Departments,
                                    e => e.EmployeeId, // Outer key selector
                                    d => d.Deptid,     // Inner key selector
                                    (e, d) => new { FirstaName = e.FirstName, DepartmentName = d.Deptname })
                                    .ToList();//Tolist() will return all the data.
                                             //sqlquery:select e.FirstName, e.LastName, e.City, d.DeptName from employee e
                                             //join Departments d on d.Id=e.EmpId
                                             //order by e.City desc


            //It converts your data to jsonformat
            var convertedData = JsonConvert.SerializeObject(employeeDepartments);
            return StatusCode(StatusCodes.Status200OK, convertedData);

        }
    }
}
