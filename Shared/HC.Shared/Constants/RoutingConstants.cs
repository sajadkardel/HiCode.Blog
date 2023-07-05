
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
            public static string Index = "/";
            public static string SignUp = "/sign-up";
            public static string SignIn = "/sign-in";
            public static string Post(int PostId) => $"/post/{PostId}";
            public static string Archive = "/archive";
            public static string News = "/news";
            public static string Books = "/books";
            public static string Users = "/users";
        }
    }
}
