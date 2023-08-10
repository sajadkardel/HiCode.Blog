using HC.Web.Shared;
using Microsoft.AspNetCore.Components;

namespace HC.Web.Components;

public partial class BlogPostPreview : AppBaseComponent
{
    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public string ShortDescription { get; set; } = string.Empty;
    [Parameter] public string ImageSrc { get; set; } = "/images/post-preview.jpeg";
    [Parameter] public string ImageAlt { get; set; } = "HiCode";

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        Title = "نکاتی درمورد Static Constructor در سیشارپ";
        ShortDescription = "یکی از سوالاتی که در مصاحبه ها فنی پرسیده میشه موارد مرتبط به Static Constructor هست که حتی اگر تا به حال از اون استفاده نکردید باید درموردش اطلاعاتی داشتته باشید. اول باید بدونیم اصلا چه نیازی به این قابلیت سیشارپ خواهیم داشت. در شرایطی ممکن است یک ممبر Static درون کلاس داشه باشیم که مقدار این ممبر Static از قبل برای ما یک مقدار ثابت و مشخص نیست. یعنی نمیتوانیم آن را به صورت";
    }
}
