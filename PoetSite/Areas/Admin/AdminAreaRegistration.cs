using Microsoft.AspNetCore.Mvc;

namespace PoetSite.Areas.Admin;

public class AdminAreaRegistration : AreaAttribute
{
    public AdminAreaRegistration() : base("Admin") { }
}