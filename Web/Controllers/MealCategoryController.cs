using Business;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Service.Contexts;
using Service.Entities;
using Service.OtherClass;
using Service.UnitOfWorks;
using System.Security.Claims;
using System.Linq;
using Business.GenericService;
using System.Linq.Expressions;
using Service.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Web.Controllers
{
    //[AllowAnonymous]
    [Authorize]
    public class MealCategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IGenericService<MealCategory> _mealCategoryService;

        //==> dung thang 
        public readonly IGenericService<MealCategory> _mealCategoryService;
        private readonly ILogger<MealCategoryController> _logger;
        private UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;

        //public MealCategoryController(ILogger<MealCategoryController> logger,
        //   UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IMealCategoryService mealCategoryService,
        //   IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
        //{
        public MealCategoryController(ILogger<MealCategoryController> logger,
           UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IGenericService<MealCategory> mealCategoryService,
           IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mealCategoryService = mealCategoryService;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {

            List<MealCategory> mealCategoryList = new List<MealCategory>();
            using (var httpClient = new HttpClient())
            {
                //Get the current claims principal
                //var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

                //// Get the claims values
                //var jwtToken = identity.Claims.Where(c => c.Type == "jwtToken")
                //                   .Select(c => c.Value).SingleOrDefault();


                //var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                var identity = User.Identity as ClaimsIdentity;

                var jwtToken = identity?.FindFirst(c => c.Type == "jwtToken").Value;  //.Claims.Where(x => x.Type == "uid").FirstOrDefault().Value;
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken);
                using (var response = await httpClient.GetAsync("https://localhost:7181/api/MealCategories"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    mealCategoryList = JsonConvert.DeserializeObject<List<MealCategory>>(apiResponse);
                }
            }

            //Advanced of Expression: co the get nhieu object trong 1 class, order theo trinh tu, them dieu kien , combinde dieu kien 
            //get all for Index 
            //Expression<Func<MealCategory, object>> parameter1 = v => v.Meals;
            //Expression<Func<MealCategory, object>>[] parameterArray = new Expression<Func<MealCategory, object>>[] { parameter1 };
            
            //var mealCategoryList = _mealCategoryService.GetAll(null,orderBy: o => o.OrderBy(x => x.MealCategoryName),naProperties: parameterArray);
            return View(mealCategoryList);
        }
        [HttpGet]
        public IActionResult CreateMealCategory()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateMealCategory(MealCategoryViewModel model)
        {
            //TaskViewModel modelNew = new TaskViewModel();
            if (!ModelState.IsValid)
                return View(model);

            MealCategory newMeal = model.MealCategory;
            newMeal.MealCategoryID = Guid.NewGuid().ToString();

            newMeal.MealCategoryImage = UploadFile(model);//luu lai chuoi name thoi

            //_iMealCategoryTodoList.createMealCategory(newMeal);
            _mealCategoryService.create(newMeal);
            var isSuccess = _unitOfWork.Complete();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DetailMealCategory(string id)
        {
            //MealCategory mealCategory = _context.MealCategorys.Where(o => o.MealCategoryID == id).Include(c => c.Meals).ToList().FirstOrDefault();// as IEnumerable<Meal>;
            //IEnumerable<Meal> mealInThisCategory = listMealInThisCategory as IEnumerable<Meal>;
            Expression<Func<MealCategory, bool>> filterExpression = u => u.MealCategoryID.Equals(id);
            Expression<Func<MealCategory, object>> objectExpression = u => u.Meals;
            Expression<Func<MealCategory, object>>[] parrameter1 = new Expression<Func<MealCategory, object>>[] { objectExpression };

            MealCategory mealCategory = _mealCategoryService.GetAll(filter: filterExpression, orderBy: o => o.OrderBy(o => o.MealCategoryName) ,naProperties: parrameter1).ToList().FirstOrDefault();
            return View(mealCategory);

        }

        private string UploadFile(MealCategoryViewModel vm)
        {
            string fileName = null;
            if (vm.MealCategoryImage != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + vm.MealCategoryImage.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    vm.MealCategoryImage.CopyTo(fileStream);
                }
            }
            return fileName;
        }
    }
}
