using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApi.Data;
using RealEstateApi.Models;
using System.Security.Claims;

namespace RealEstateApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {

        ApiDbContext _dbContext = new ApiDbContext();


        [HttpGet("GetAssestsByCategoryId")]   // just put in double quote to overide name 
        [Authorize]
        public IActionResult GetAssets(int categoryId) //fetching all assets based on CategoryId from the database
        {
            var assetResult = _dbContext.Assets.Where(c=>c.CategoryId == categoryId);

            if (assetResult == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(assetResult);
            }

        }



        [HttpGet("GetAssetDetails")]
        [Authorize]

        public IActionResult GetAssetDetails(int id) //fetching  asset details based on Id from the database
        {
            var assetDetailsResult = _dbContext.Assets.FirstOrDefault(a => a.Id == id);

            if (assetDetailsResult == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(assetDetailsResult);
            }

        }

        [HttpGet("TrendingAssets")]
        [Authorize]
        public IActionResult GetTrendingAssets() //fetching all assets based on IsTrending from the database
        {
            var trendingResult = _dbContext.Assets.Where(c => c.IsTrending == true);

            if (trendingResult == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(trendingResult);
            }

        }



        [HttpGet("SearchAssets")]
        [Authorize]
        public IActionResult GetSearchAssets(string address) //Searching / sorting  through  all assets based on address from the database
        {
            var SearchResult = _dbContext.Assets.Where(a => a.Address.Contains(address));

            if (SearchResult == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(SearchResult);
            }

        }



        [HttpPost("[Action]")]
        [Authorize]
        public IActionResult AddAsset([FromBody] Asset asset)
        {
            //var property = _dbContext.Properties.FirstOrDefault(p => p.properties == )

            if (asset == null)
            {
                //return BadRequest("Record matching this property exists.");

                return NoContent();
            }
            else
            {
                var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value; // finding email using cliams (payload sent to JWT)
                var user = _dbContext.Users.FirstOrDefault(u => u.EmailAddress == userEmail);

                if (user == null)
                {
                    return NotFound("No record found");
                }
                else
                {
                    asset.IsTrending = false; // set to false because only Admin can get to change the IsTrending value
                    asset.UserId = user.Id;   // Auto-incrementing UserId (The user adding assets) . A Foriegn key in Assets table

                    _dbContext.Assets.Add(asset);
                    _dbContext.SaveChanges();

                    return StatusCode(StatusCodes.Status201Created);

                }
            }

        }


        [HttpPut("[Action]/{id}")]
        [Authorize]
        public IActionResult UpdateAssetById(int id, [FromBody] Asset asset)
        {

            var assetResult = _dbContext.Assets.FirstOrDefault(a => a.Id == id);

            if (assetResult == null)
            {
                //return BadRequest("Record matching this property exists.");

                return NotFound();
            }
            else
            {
                var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value; // finding email using cliams (payload sent to JWT)
                var user = _dbContext.Users.FirstOrDefault(u => u.EmailAddress == userEmail);

                if (user == null)
                {
                    return NotFound("No record found");

                }else if (assetResult.UserId == user.Id)  // checking to see if the ID of the user tallies with the ID in the database

                {
                    //updating assets table in the database
                    assetResult.Name = asset.Name;
                    assetResult.Details = asset.Details;
                    assetResult.Address = asset.Address;
                    assetResult.ImageUrl = asset.ImageUrl;
                    assetResult.Price = asset.Price;


                    asset.IsTrending = false; // set to false because only Admin can get to change the IsTrending value
                    asset.UserId = user.Id;   // Auto-incrementing UserId (The user adding assets) . A Foriegn key in Assets table


                    _dbContext.SaveChanges();

                    return Ok("Record Successfully Updated");
                }
                else
                {
                    return BadRequest();
                }
                
            }

        }

        [HttpDelete("[Action]/{id}")]
        [Authorize]
        public IActionResult DeleteAssetById(int id)
        {

            var assetResult = _dbContext.Assets.FirstOrDefault(a => a.Id == id);

            if (assetResult == null)
            {
                //return BadRequest("Record matching this property exists.");

                return NotFound();
            }
            else
            {
                var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value; // finding email using cliams (payload sent to JWT)
                var user = _dbContext.Users.FirstOrDefault(u => u.EmailAddress == userEmail);

                if (user == null)
                {
                    return NotFound("No record found");

                }
                else if (assetResult.UserId == user.Id)  // checking to see if the ID of the user tallies with the ID in the database

                {
                    //deleting assets from table in the database

                    _dbContext.Assets.Remove(assetResult);


                    _dbContext.SaveChanges();

                    return Ok("Record deleted Successfully ");
                }
                else
                {
                    return BadRequest();
                }

            }

        }


            //[HttpGet]
            //    public IActionResult GetProducts(string address, string price)
            //    {
            //        var products = _dbContext.Assets.AsQueryable();     if (!string.IsNullOrEmpty(address) && !string.IsNullOrEmpty(price))
            //        {
            //            switch (address)
            //            {
            //                case "name":
            //                    products = price == "asc" ? products.OrderBy(p => p.Name) : products.OrderByDescending(p => p.Name);
            //                    break;
            //                case "price":
            //                    products = order == "asc" ? products.OrderBy(p => p.Price) : products.OrderByDescending(p => p.Price);
            //                    break;
            //                default:
            //                    break;
            //            }
            //        }     return Ok(products);
            //    }



        //public IActionResult GetProducts(string sort, string order)
        //{
        //    var products = _dbContext.Products.AsQueryable(); if (!string.IsNullOrEmpty(sort) && !string.IsNullOrEmpty(order))
        //    {
        //        switch (sort)
        //        {
        //            case "name":
        //                products = order == "asc" ? products.OrderBy(p => p.Name) : products.OrderByDescending(p => p.Name);
        //                break;
        //            case "price":
        //                products = order == "asc" ? products.OrderBy(p => p.Price) : products.OrderByDescending(p => p.Price);
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    return Ok(products);
        //}


    }
}
