using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Model = Service.WebApi.Models.Post;

namespace Service.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : GenericController<Model>
    {
        [HttpGet("metadata")]
        public string GetMeta()
        {
            return GetModelMetaData();
        }

        // GET: api/Post
        [HttpGet]
        public IEnumerable<Model> Get()
        {
            return LoadData();
        }

        // GET: api/Post/5
        [HttpGet("{id}", Name = "Get")]
        public Model Get(int id)
        {
            var models = LoadData();

            return models.SingleOrDefault(m => m.Id == id);
        }

        // POST: api/Post
        [HttpPost]
        public Model Post([FromBody]Model model)
        {
            return InsertModel(model);
        }

        // PUT: api/Post/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Model model)
        {
            UpdateModel(id, model);
        }

        // PATCH: api/Post/5
        [HttpPatch("{id}")]
        public void Patch(int id, [FromBody] Model model)
        {
            UpdateModel(id, model);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            DeleteModel(id);
        }
    }
}
