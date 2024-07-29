using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class HomeController:Controller
    {
        private IStoreRepository repository;
        public int pageSize = 4;
        public HomeController(IStoreRepository repo)
        {
            repository = repo;
        }
        //public IActionResult Index() => View(repository.Products);
        public ViewResult Index(string? category,int productPage = 1)
            => View(new ProductsListViewModel
            {
                Products = repository.Products
                .Where(p => category==null || p.Category == category)
                .OrderBy(p => p.ProductID)
                .Skip((productPage - 1) * pageSize)
                .Take(pageSize),
               PagingInfo = new PagingInfo
               {
                   CurrentPage = productPage,
                   ItemsPerPage = pageSize,
                   TotalItems = category == null 
                   ? repository.Products.Count()
                   : repository.Products.Where(e =>  e.Category == category).Count()

               },
               CurrentCategory = category
            });
    }
}
