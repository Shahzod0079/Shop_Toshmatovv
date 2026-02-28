using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    public RedirectResult Index()
    {
        return Redirect("/Items/List");
    }
}