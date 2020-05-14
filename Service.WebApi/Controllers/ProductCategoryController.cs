using Microsoft.AspNetCore.Mvc;
using Model = Service.WebApi.Models.ProductCategory;

namespace Service.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : GenericController<Model>
    {
    }
}
