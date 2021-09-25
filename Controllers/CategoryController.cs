using Docker.Models;
using Docker.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Docker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository categoryRepository = null;
        private ILogger<CategoryController> logger = null;
        public CategoryController(ICategoryRepository _categoryRepository, ILogger<CategoryController> logger)
        {
            categoryRepository = _categoryRepository;
            this.logger = logger;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        public OkObjectResult Get()
        {
            logger.LogInformation("Get Method Called");
            IQueryable<Category> categories = null;
            try
            {
                categories = categoryRepository.FindAll();
            }
            catch (Exception ex)
            {
                logger.LogError("Error Occured", ex.Message);
            }
            return new OkObjectResult(categories.ToList());
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = categoryRepository.FindByCondition(a => a.Id == id).FirstOrDefault();

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<ActionResult<Category>> Post([FromBody] Category category)
        {
            categoryRepository.Create(category);
            categoryRepository.Save();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
