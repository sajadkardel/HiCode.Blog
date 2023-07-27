
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

        public static class Blog
        {
            private const string BaseAddress = "blog/";

            public const string GetAllCategory = BaseAddress + "getAllCategory";
            public const string GetCategoryById = BaseAddress + "getCategoryById";
            public const string CreateCategory = BaseAddress + "createCategory";
            public const string UpdateCategory = BaseAddress + "updateCategory";
            public const string DeleteCategory = BaseAddress + "deleteCategory";

            public const string GetAllPost = BaseAddress + "getAllPost";
            public const string GetPostById = BaseAddress + "getPostById";
            public const string CreatePost = BaseAddress + "createPost";
            public const string UpdatePost = BaseAddress + "updatePost";
            public const string DeletePost = BaseAddress + "deletePost";

            public const string GetAllTag = BaseAddress + "getAllTag";
            public const string GetTagById = BaseAddress + "getTagById";
            public const string CreateTag = BaseAddress + "createTag";
            public const string UpdateTag = BaseAddress + "updateTag";
            public const string DeleteTag = BaseAddress + "deleteTag";
        }
    }

    public static class ClientSide
    {
        public static class Page
        {
            public static string Index = "/";
            public static string SignUp = "/sign-up";
            public static string SignIn = "/sign-in";
            public static string Post(int PostId) => $"/blog-post/{PostId}";
            public static string News = "/news";
            public static string Books = "/books";
            public static string Users = "/users";
            public static string Profile = "/profile";
        }
    }
}
