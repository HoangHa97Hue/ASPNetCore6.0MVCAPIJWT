using Business;
using Business.GenericService;
using Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using Service.Contexts;
using Service.Entities;
using Service.Entities.Identity;
using Service.OtherClass;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Web.CartServiceSession;
using Web.TempdataExtension;
using Web.ViewModel;
using static System.Net.Mime.MediaTypeNames;

namespace Web.Controllers
{
    //[AllowAnonymous]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IGenericService<Order> _iOrderService;
        private readonly ILogger<OrderController> _logger;
        private UserManager<ApplicationUser> _userManager;
        private readonly MyDbContext _context;
        //public OrderController(ILogger<OrderController> logger,
        //    UserManager<ApplicationUser> userManager, IOrderService iOrderService,
        //     MyDbContext context)
        //{
        public OrderController(ILogger<OrderController> logger,
            UserManager<ApplicationUser> userManager, IGenericService<Order> iOrderService,
             MyDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            this._iOrderService = iOrderService;
            _context = context;
        }


        [HttpGet]
        //[Authorize(Roles = "Admin")]
        //[Authorize(Roles = "Member")]
        public async Task<IActionResult> Index()
        {
            var orderViewModel = new OrderViewModel();

            //viet lai ham cho no add OrderProcessStatus
            string orderProcessStatus_SelectOptionSelected = TempData.GetString<String>("OrderProcessStatus_SelectOptionSelected");
            string UserID_SelectOptionSelected = TempData.GetString<String>("UserID_SelectOptionSelected");

            //Setting to display correct select option after called this method, add thang dau tien la thang selected, sau do add thang con lai
            List<String> listOrderProcessStatus = new List<String>();

            listOrderProcessStatus.Add("All");
            listOrderProcessStatus.Add(Common.OrderProcess.Success);
            listOrderProcessStatus.Add(Common.OrderProcess.Fail);
            listOrderProcessStatus.Add(Common.OrderProcess.Processing);
            foreach (string status in listOrderProcessStatus)
            {
                if (status.Equals(orderProcessStatus_SelectOptionSelected))
                {
                    orderViewModel.OrderProcessStatus.Add(new SelectListItem { Text = status, Value = status });
                }
            }
            //if  orderViewModel.OrderProcessStatus has added status above then skip
            foreach (string status in listOrderProcessStatus)
            {
                bool checkHasAdded = false;
                foreach (SelectListItem statusHasAdded in orderViewModel.OrderProcessStatus)
                {
                    if (statusHasAdded.Value.Equals(status))
                    {
                        checkHasAdded = true;
                    }
                }
                if (checkHasAdded == false)
                {
                    orderViewModel.OrderProcessStatus.Add(new SelectListItem { Text = status, Value = status });
                }
            }



            var currentUserID = _userManager.GetUserId(User);
            var currentUser = await _userManager.FindByIdAsync(currentUserID);
            orderViewModel.identityUser = currentUser;

            var orderList = new List<Order>();

            List<String> listRoleOfCurrentUser = await _userManager.GetRolesAsync(currentUser) as List<String>;
            //If role Admin need to add to selectListItem , if not => cannot bind to prop in OrderViewModel
            if (listRoleOfCurrentUser.Any(t => t.Equals("Admin")))
            {
                //Same for UserIDSelected
                var usersInRoleMember = await _userManager.GetUsersInRoleAsync("Member");

                List<SelectListItem> listUserForDisplay = new List<SelectListItem>();
                listUserForDisplay.Add(new SelectListItem { Text = "All", Value = "All" });

                foreach (var user in usersInRoleMember)
                {
                    listUserForDisplay.Add(new SelectListItem { Text = user.UserName, Value = user.Id });
                }
                //check if match for UserID has selected then must add First
                foreach (SelectListItem user in listUserForDisplay)
                {
                    if (user.Value.Equals(UserID_SelectOptionSelected))
                    {
                        orderViewModel.UserSelectOptions.Add(new SelectListItem { Text = user.Text, Value = user.Value });// text == name , value == id
                    }
                }
                //then add the rest 
                foreach (SelectListItem user in listUserForDisplay)
                {
                    bool checkHasAdded = false;
                    foreach (SelectListItem userHasAdded in orderViewModel.UserSelectOptions)
                    {
                        if (userHasAdded.Value.Equals(user.Value))
                        {
                            checkHasAdded = true;
                        }
                    }
                    if (checkHasAdded == false)
                    {
                        orderViewModel.UserSelectOptions.Add(new SelectListItem { Text = user.Text, Value = user.Value });
                    }
                }


            }
            //Check if Search by Order Status Processing
            var orderListByStatusProcess = TempData.GetListOrder<List<Order>>("OrderListByStatusProcess");
            if (orderListByStatusProcess != null)
            {
                if (orderListByStatusProcess.Count() > 0)
                {
                    orderList = orderListByStatusProcess;
                }
            }
            else
            {
                Expression<Func<Order, object>> parameter1 = v => v.OrderProcess;
                Expression<Func<Order, object>> parameter2 = v => v.User;
                Expression<Func<Order, object>>[] parameterArray = new Expression<Func<Order, object>>[] { parameter1, parameter2 };
                //check if Member or admin

                if (listRoleOfCurrentUser.Any(t => t.Equals("Admin")))
                {
                   
                    orderList = _iOrderService.GetAll(null, orderBy: o => o.OrderBy(x => x.DateUpdated), naProperties: parameterArray).ToList();
                }
                else
                {
                    Expression<Func<Order, bool>> filterExpression = u => u.User.Id.Equals(currentUser.Id);
                    orderList = _iOrderService.GetAll(filter: filterExpression, orderBy: o => o.OrderBy(x => x.DateCreated), naProperties: parameterArray).ToList();
                }

            }

            orderViewModel.Orders = orderList;
            return View(orderViewModel);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        //[Authorize(Roles = "Member")]
        public async Task<IActionResult> IndexForOrderStatus(OrderViewModel orderViewModel)
        {
            //Search by User and Order Process
            var currentUserID = _userManager.GetUserId(User);
            var currentUser = await _userManager.FindByIdAsync(currentUserID);
            var orderList = new List<Order>();

            //Config to return correct selected options after search
            TempData.PutString("UserID_SelectOptionSelected", orderViewModel.UserIDSelected);
            TempData.PutString("OrderProcessStatus_SelectOptionSelected", orderViewModel.OrderProcessSearchSelected);

            Expression<Func<Order, object>> parameter1 = v => v.OrderProcess;
            Expression<Func<Order, object>> parameter2 = v => v.User;
            Expression<Func<Order, object>>[] parameterArray = new Expression<Func<Order, object>>[] { parameter1, parameter2 };

            //if admin
            if (orderViewModel.UserIDSelected != null)
            {
                var currentUserSelected = await _userManager.FindByIdAsync((orderViewModel.UserIDSelected));
                //IF Admin click all for display all member user
                if (orderViewModel.UserIDSelected.Equals("All"))
                {
                    //check for each orderProcessSearch
                    switch (orderViewModel.OrderProcessSearchSelected)
                    {
                        case "All":
                            orderList = _iOrderService.GetAll(null, orderBy: o => o.OrderBy(x => x.DateUpdated), naProperties: parameterArray).ToList();
                            break;
                        default:
                            Expression<Func<Order, bool>> filterExpression = u => u.OrderProcess.StatusOrder.Equals(orderViewModel.OrderProcessSearchSelected);
                            orderList = _iOrderService.GetAll(filter: filterExpression, orderBy: o => o.OrderBy(x => x.DateCreated), naProperties: parameterArray).ToList();
                            //orderList = _iOrderService.GetByOrderProcess(orderViewModel.OrderProcessSearchSelected).ToList();
                            break;
                    }
                }
                else
                {
                    if (orderViewModel.OrderProcessSearchSelected.Equals("All"))
                    {
                        Expression<Func<Order, bool>> filterExpression = u => u.User.Id.Equals(orderViewModel.UserIDSelected);
                        orderList = _iOrderService.GetAll(filter: filterExpression, orderBy: o => o.OrderBy(x => x.DateCreated), naProperties: parameterArray).ToList();
                        //orderList = _iOrderService.GetByUser(currentUserSelected).ToList();
                    }
                    else
                    {
                        Expression<Func<Order, bool>> filterExpression = u => u.User.Id.Equals(orderViewModel.UserIDSelected) && u.OrderProcess.StatusOrder.Equals(orderViewModel.OrderProcessSearchSelected);
                        orderList = _iOrderService.GetAll(filter: filterExpression, orderBy: o => o.OrderBy(x => x.DateCreated), naProperties: parameterArray).ToList();
                        //orderList = _iOrderService.get(currentUserSelected, orderViewModel.OrderProcessSearchSelected).ToList();
                    }
                }
                TempData.PutListOrder("OrderListByStatusProcess", orderList);//orderList.ToList()
                return RedirectToAction("Index");
            }
            //Seach with Status Process = All, for Customer
            if (orderViewModel.OrderProcessSearchSelected.Equals("All"))
            {
                return RedirectToAction("Index");
            }
            Expression<Func<Order, bool>> lastFilterExpression = u => u.User.Id.Equals(currentUser.Id) && u.OrderProcess.StatusOrder.Equals(orderViewModel.OrderProcessSearchSelected);
            orderList = _iOrderService.GetAll(filter: lastFilterExpression, orderBy: o => o.OrderBy(x => x.DateCreated), naProperties: parameterArray).ToList();
            //orderList = _iOrderService.GetByUserAndOrderProcess(currentUser, orderViewModel.OrderProcessSearchSelected).ToList();
            TempData.PutListOrder("OrderListByStatusProcess", orderList);
            return RedirectToAction("Index");
        }



    }
}
