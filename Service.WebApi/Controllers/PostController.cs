﻿using Microsoft.AspNetCore.Mvc;
using Model = Service.WebApi.Models.Post;

namespace Service.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : GenericController<Model>
    {
    }
}
