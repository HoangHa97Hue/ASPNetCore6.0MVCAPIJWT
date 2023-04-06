using Newtonsoft.Json;
using Service.Entities;
using System.Globalization;

namespace Web.CartServiceSession
{
    public class CartService
    {
        public const string CARTKEY = "cart";
        public IHttpContextAccessor _context { get; set; }
        public HttpContext HttpContext { get; set; }

        public CartService(IHttpContextAccessor context)
        {
            _context= context;
            HttpContext = context.HttpContext;
        }

        // Lấy cart từ Session (danh sách CartItem)
        public List<CartItem> GetCartItems()
        {

            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
            }
            return new List<CartItem>();
        }

        // Xóa cart khỏi session
        public void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }

        // Lưu Cart (Danh sách CartItem) vào session
        public void SaveCartSession(List<CartItem> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }
    }
}
