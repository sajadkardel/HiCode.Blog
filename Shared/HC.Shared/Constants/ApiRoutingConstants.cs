
namespace HC.Shared.Constants;

public static class ApiRoutingConstants
{
    public static class ApiContentTypeConst
    {
        public const string Json = "application/json";
        public const string UrlEncode = "application/x-www-form-urlencoded";
    }

    public static class Auth
    {
        public const string ControllerName = "auth";
        public const string SignUp = "signUp";
        public const string SignIn = "signIn";
        public const string Get = "get";
        public const string GetById = "getById";
        public const string Create = "create";
        public const string Update = "update";
        public const string Delete = "delete";
    }
}
