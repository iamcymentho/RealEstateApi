using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateApi.Data;
using RealEstateApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RealEstateApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]


    public class CategoriesController : ControllerBase
        
    {
        ApiDbContext _dbContext = new ApiDbContext(); //_dbcontext is the database connector (dbconn)



        // GET: api/<CategoriesController>
        [HttpGet]
        public IActionResult Get()  
        {

            //return _dbContext.Categories; 

            return Ok(_dbContext.Categories);

            // returning status code (ok)
            // categories is the table name in the db
            // IActionResult so the STATUS CODE can be returned 
        }




        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
           var Category = _dbContext.Categories.FirstOrDefault(x=>x.Id == id); // finding using LAMDA expression

            if(Category == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(Category);
            }
           


            // returning status code (ok)
            // categories is the table name in the db
            // IActionResult so the STATUS CODE can be returned 
        }


        [HttpGet("[action]")]    // "ACTION" stands for attribute routing 
        public IActionResult SortCategories()
        {
            var sortCategories = _dbContext.Categories.OrderByDescending(X => X.Name);
            return Ok(sortCategories);


        }



        // POST api/<CategoriesController>
        [HttpPost]
        public IActionResult Post([FromBody] Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();

            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Category categoryObj)
        {
           var category =  _dbContext.Categories.Find(id);

            if(category == null)
            {
                return NotFound("No record found against this Id " + id);

            }
            else
            {
                category.Name = categoryObj.Name;
                category.ImageUrl = categoryObj.ImageUrl;
                _dbContext.SaveChanges();

                return Ok("Record Updated Successfully");
            }


           
;        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _dbContext.Categories.Find(id); // finding the particular Item with ID to delete 

            if(category == null)
            {
                return NotFound("No record  with id " + id + " can be found");
            }
            else
            {
                _dbContext.Categories.Remove(category); // delete item with the ID 
                _dbContext.SaveChanges();

                return Ok("Record Deleted Succesffully");

            }
           
        }
    }
}
