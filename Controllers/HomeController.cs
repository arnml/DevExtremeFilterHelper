using DevExtreme.AspNet.Mvc;
using dgFilter.Helper;
using dgFilter.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace dgFilter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetUsers(DataSourceLoadOptions loadOptions)
        {
            try
            {
                var users = CompanyUser.GetUsers();
                if (loadOptions.Filter == null)
                {
                    return Ok(users);
                }                    
                else
                {
                    return Ok(processFilter(loadOptions, users));
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }
        private List<CompanyUser> processFilter(DataSourceLoadOptions loadOptions, List<CompanyUser> users)
        {
            var columns = new string[] { "FirstName", "LastName", "Email", "Password", "Address" };
            var searchDict = DevExtremeFilterHelper.GetSearchTerms(loadOptions.Filter, columns);
            var searchTerms = new Dictionary<string, string>
            {
                { "SearchPanel", "" }
            };
            searchTerms["SearchPanel"] = searchDict["contains"].First().Value;
            foreach (var column in columns)
            {
                searchTerms[column] = searchDict["="][column];
            }
            var _common = searchTerms["SearchPanel"];
            var _firstname = searchTerms["FirstName"];
            var _lastname = searchTerms["LastName"];
            var _email = searchTerms["Email"];
            var _password = searchTerms["Password"];
            var _address = searchTerms["Address"];

            return SearchUsers(users, _common, _firstname, _lastname, _email, _password, _address);
        }
        public List<CompanyUser> SearchUsers(List<CompanyUser> users, string common, string firstname, string lastname, string email, string password, string address)
        {
            var result = users
                .Where(s => (string.IsNullOrWhiteSpace(firstname) || s.FirstName.Trim().ToLower() == firstname.ToLower()) &&
                            (string.IsNullOrWhiteSpace(lastname) || s.LastName.Trim().ToLower() == lastname.ToLower()) &&
                            (string.IsNullOrWhiteSpace(email) || s.Email.Trim().ToLower() == email.ToLower()) &&
                            (string.IsNullOrWhiteSpace(password) || s.Password.Trim().ToLower() == password.ToLower()) &&
                            (string.IsNullOrWhiteSpace(address) || s.Address.Trim().ToLower() == address.ToLower()) &&
                            (string.IsNullOrWhiteSpace(common) ||
                             s.FirstName.ToLower().Contains(common.ToLower()) ||
                             s.LastName.ToLower().Contains(common.ToLower()) ||
                             s.Email.ToLower().Contains(common.ToLower()) ||
                             s.Password.ToLower().Contains(common.ToLower()) ||
                             s.Address.ToLower().Contains(common.ToLower())))
                .ToList();

            return result;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}