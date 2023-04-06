using Business;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Service.Entities;
using Service.OtherClass;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Drawing.Imaging;
using Service.Contexts;
using Web.CartServiceSession;
using System.Linq.Expressions;
using Service.UnitOfWorks;
using Service.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Business.GenericService;

namespace Web.Controllers
{
    //[AllowAnonymous]
    [Authorize]
    public class MealController : Controller
    {
        //private readonly IMealCategoryService _iMealCategoryService;
        //private readonly IMealService _iMeaService;
        //private readonly IOrderService _iIOrderService;
        private readonly IGenericService<MealCategory> _iMealCategoryService;
        private readonly IGenericService<Meal> _iMeaService;
        private readonly IGenericService<Order> _iIOrderService;
        private readonly ILogger<MealController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        //private readonly MyDbContext _context;
        public CartService _cartService { get; set; }

        //public MealController(ILogger<MealController> logger, IUnitOfWork unitOfWork,
        //    UserManager<ApplicationUser> userManager, IMealCategoryService iMealCategoryTodoList, IMealService iMealTodoList, IOrderService iIOrderTodoList,
        //    IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment, CartService cartService)
        //{
        public MealController(ILogger<MealController> logger, IUnitOfWork unitOfWork,
           UserManager<ApplicationUser> userManager, IGenericService<MealCategory> iMealCategoryTodoList, IGenericService<Meal> iMealTodoList, IGenericService<Order> iIOrderTodoList,
           IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment, CartService cartService)
        {
            _logger = logger;
            this._unitOfWork = unitOfWork;
            _userManager = userManager;
            _iMealCategoryService = iMealCategoryTodoList;
            _iMeaService = iMealTodoList;
            _iIOrderService = iIOrderTodoList;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
            //_context = context;
            _cartService = cartService;
        }

        /// Thêm sản phẩm vào cart
        [Route("addcart/{mealID}", Name = "addcart")]
        public IActionResult AddToCart([FromRoute] string mealID)
        {

            //var meal = _context.Meals
            //    .Where(p => p.MealID == mealID)
            //    .FirstOrDefault();
            var meal = _iMeaService.getByID(mealID);
            if (meal == null)
                return NotFound("Không có sản phẩm");

            // Xử lý đưa vào Cart ...
            var cart = _cartService.GetCartItems();
            var cartitem = cart.Find(p => p.Meal.MealID == mealID);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity++;
            }
            else
            {
                //  Thêm mới
                cart.Add(new CartItem() { quantity = 1, Meal = meal });
            }

            // Lưu cart vào Session
            _cartService.SaveCartSession(cart);
            // Chuyển đến trang hiện thị Cart
            return RedirectToAction(nameof(Cart));
        }

        // Hiện thị giỏ hàng
        [Route("/cart", Name = "cart")]
        public IActionResult Cart()
        {
            return View(_cartService.GetCartItems());
        }

        [Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] string mealID, [FromForm] int quantity)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = _cartService.GetCartItems();
            var cartitem = cart.Find(p => p.Meal.MealID == mealID);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity = quantity;
            }
            _cartService.SaveCartSession(cart);
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return Ok();
        }

        /// xóa item trong cart
        [Route("/removecart/{mealID}", Name = "removecart")]
        public IActionResult RemoveCart([FromRoute] string mealID)
        {
            var cart = _cartService.GetCartItems();
            var cartitem = cart.Find(p => p.Meal.MealID == mealID);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cart.Remove(cartitem);
            }

            _cartService.SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }


        /// <summary>
        /// Code cux
        /// </summary>
        /// <returns></returns>

        public Expression<Func<Meal, object>>[] ReturnMealParameterArray()
        {
            Expression<Func<Meal, object>> parameter1 = v => v.OrderDetails;
            Expression<Func<Meal, object>>[] parameterArray = new Expression<Func<Meal, object>>[] { parameter1 };
            return parameterArray;
        }

        public Expression<Func<MealCategory, object>>[] ReturnMealCategoryParameterArray()
        {
            Expression<Func<MealCategory, object>> parameter1 = v => v.Meals;
            Expression<Func<MealCategory, object>>[] parameterArray = new Expression<Func<MealCategory, object>>[] { parameter1 };
            return parameterArray;
        }

        public IActionResult Index()
        {     
            var mealList = _iMeaService.GetAll(null, orderBy: o => o.OrderBy(x => x.MealName), naProperties: ReturnMealParameterArray()).ToList();
            return View(mealList);
        }

        //Checkout tuc la submit Order 
        [Route("Meal/CheckOut/{sum}")]
        public async Task<IActionResult> CheckOut(decimal sum)
        {
            var cartOrder = _cartService.GetCartItems().ToList();
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);
            Order order = new()
            {
                OrderID = Guid.NewGuid().ToString(),
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                Note = string.Empty,
                OrderDetails = null,
                OrderProcess = null,
                SumPrice = sum,
                User = user
            };
            OrderProcess orderProcess = new()
            {
                OrderProcessID = Guid.NewGuid().ToString(),
                Order = order,
                OrderID = order.OrderID,
                PriceOrder = order.SumPrice,
                StatusOrder = "Ordered Meals Successfully"
            };
            //suy nghi huong lay sumprice tu ben Cart View Page
            //order.SumPrice = => Thang ni tao bien rien roi cong lai tat ca
            order.OrderProcess = orderProcess;
            _iIOrderService.create(order);
            _unitOfWork.Complete();
            CartService cartService = new CartService(_httpContextAccessor);
            cartService.ClearCart();
            return RedirectToAction("Index", "Order");
        }

        [HttpGet]
        public IActionResult CreateMeal()
        {
            MealViewModel model = new MealViewModel();
            var mealCategories = _iMealCategoryService.GetAll(null, orderBy: o => o.OrderBy(x => x.MealCategoryName), naProperties: ReturnMealCategoryParameterArray()).ToList();
            TypeSearchList typeSearchList = new TypeSearchList();
            typeSearchList.AddTypeSearchList(mealCategories.ToList());
            foreach (var type in typeSearchList.typeSearches)
            {
                model.ListSelectMealCategory.Add(new SelectListItem { Text = type.TypeName, Value = type.TypeValue });
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateMeal(MealViewModel model)
        {
            //TaskViewModel modelNew = new TaskViewModel();
            if (!ModelState.IsValid)
                return View(model);
            var mealCategory = _iMealCategoryService.getByID(model.MealCategoryIDSelected);
            //curent user login
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);
            Meal newMeal = new()
            {
                MealID = Guid.NewGuid().ToString(),
                MealDescription = model.Meal.MealDescription,
                MealName = model.Meal.MealName,
                MealCategory = mealCategory,
                Price = model.Meal.Price,
                Quantity = model.Meal.Quantity, // quantity con lai cua mon an
                User = user, //user create this meal
                MealImage = UploadFile(model),//luu lai chuoi name thoi
            };

            _iMeaService.create(newMeal);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult MealDetail(string id)
        {
            Meal meal = _iMeaService.getByID(id);
            return View(meal);
        }

        [HttpGet]
        public IActionResult EditMeal(string id)
        {
            Meal meal = _iMeaService.getByID(id);

            MealViewModel model = new MealViewModel();
            var mealCategories = _iMealCategoryService.GetAll(null, orderBy: o => o.OrderBy(x => x.MealCategoryName), naProperties: ReturnMealCategoryParameterArray()).ToList();
            TypeSearchList typeSearchList = new TypeSearchList();
            typeSearchList.AddTypeSearchList(mealCategories.ToList());
            foreach (var type in typeSearchList.typeSearches)
            {
                model.ListSelectMealCategory.Add(new SelectListItem { Text = type.TypeName, Value = type.TypeValue });
            }
            model.Meal = meal;

            //HaFormFileCollection iFormFileCollection = new HaFormFileCollection();

            //model.MealImage = iFormFileCollection.GetFile(meal.MealImage);
            return View(model);
        }

        [HttpPost]
        public IActionResult EditMeal(MealViewModel model)
        {
            MealViewModel meal = new MealViewModel();
            var mealCategory = _iMealCategoryService.getByID(model.MealCategoryIDSelected);
            Meal mealToBind = _iMeaService.getByID(model.Meal.MealID);
            mealToBind.MealName = model.Meal.MealName;
            mealToBind.Price = model.Meal.Price;
            mealToBind.MealDescription = model.Meal.MealDescription;
            mealToBind.MealCategory = mealCategory;
            mealToBind.Quantity = model.Meal.Quantity;
            mealToBind.MealImage = UploadFile(model);

            _iMeaService.edit(mealToBind);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }

        //this method to save image to the system, then save name of img to DB
        private string UploadFile(MealViewModel vm)
        {
            string fileName = null;
            if (vm.MealImage != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + vm.MealImage.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    vm.MealImage.CopyTo(fileStream);
                }
            }
            return fileName;
        }
    }
}