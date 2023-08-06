
namespace HC.Web.Shared;

public partial class Header : AppBaseComponent
{
    private bool isExpandedMenu = true;

    private void OnExpanderClick()
    {
        isExpandedMenu = !isExpandedMenu;

        // Notify to NavMenu to width 0px or 300px
    }
}
