//namespace View.ViewModel
//{
//    public class JwtToken
//    {
//        private readonly RequestDelegate _request;

//        public JwtToken(RequestDelegate request)
//        {
//            _request = request;
//        }

//        public async Task Invoke(HttpContext context)
//        {
//            if (context.Session != null && context.Session.Keys.Contains("AuthToken"))
//            {
//                var token = context.Session.GetString("AuthToken");
//                // Thực hiện logic với token...
//            }
//            else
//            {
//                // Xử lý trường hợp không có session
//                throw new InvalidOperationException("Session has not been configured or no token found.");
//            }
//        }
//    }
//}
