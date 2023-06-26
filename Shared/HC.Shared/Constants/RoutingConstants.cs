
namespace HC.Shared.Constants;

public static class RoutingConstants
{

    public static class ServerSide
    {
        public static class Auth
        {
            private const string BaseAddress = "auth/";
            public const string SignUp = BaseAddress + "signUp";
            public const string SignIn = BaseAddress + "signIn";
        }

        public static class User
        {
            private const string BaseAddress = "user/";
            public const string GetAll = BaseAddress + "getAll";
            public const string GetById = BaseAddress + "getById";
            public const string Create = BaseAddress + "create";
            public const string Update = BaseAddress + "update";
            public const string Delete = BaseAddress + "delete";
        }
    }

    public static class ClientSide
    {
        public static class Page
        {
            public const string Index = "/";
            public const string SignUp = "/sign-up";
            public const string SignIn = "/sign-in";
            public const string Users = "/users";
        }
    }
}
