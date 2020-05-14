using Microsoft.AspNetCore.Mvc;
using Model = Service.WebApi.Models.Product;

namespace Service.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : GenericController<Model>
    {
    }
}
