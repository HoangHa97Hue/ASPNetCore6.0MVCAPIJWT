//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json;
//using Service.Entities;

//namespace Web.Controllers
//{
//    public class CartController : Controller
//    {
//        public IActionResult Index()
//        {
//            return View();
//        }
//        public const string CARTKEY = "cart";

//        // Lấy cart từ Session (danh sách CartItem)
//        List<CartItem> GetCartItems()
//        {

//            var session = HttpContext.Session;
//            string jsoncart = session.GetString(CARTKEY);
//            if (jsoncart != null)
//            {
//                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
//            }
//            return new List<CartItem>();
//        }

//        // Xóa cart khỏi session
//        void ClearCart()
//        {
//            var session = HttpContext.Session;
//            session.Remove(CARTKEY);
//        }

//        // Lưu Cart (Danh sách CartItem) vào session
//        void SaveCartSession(List<CartItem> ls)
//        {
//            var session = HttpContext.Session;
//            string jsoncart = JsonConvert.SerializeObject(ls);
//            session.SetString(CARTKEY, jsoncart);
//        }

//        /// Thêm sản phẩm vào cart
//        [Route("addcart/{productid:int}", Name = "addcart")]
//        public IActionResult AddToCart([FromRoute] int productid)
//        {

//            var product = _context.Products
//                .Where(p => p.ProductId == productid)
//                .FirstOrDefault();
//            if (product == null)
//                return NotFound("Không có sản phẩm");

//            // Xử lý đưa vào Cart ...
//            var cart = GetCartItems();
//            var cartitem = cart.Find(p => p.product.ProductId == productid);
//            if (cartitem != null)
//            {
//                // Đã tồn tại, tăng thêm 1
//                cartitem.quantity++;
//            }
//            else
//            {
//                //  Thêm mới
//                cart.Add(new CartItem() { quantity = 1, product = product });
//            }

//            // Lưu cart vào Session
//            SaveCartSession(cart);
//            // Chuyển đến trang hiện thị Cart
//            return RedirectToAction(nameof(Cart));
//        }
//    }
//}
