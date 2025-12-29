using E_Commerce.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Controllers
{
    [Route("api/[controller]")] //baseUrl/api/Product
    [ApiController]
    public class ProductController : ControllerBase
    {
        //Get: baseUrl/api/Product/id 
         [HttpGet] 
        public ActionResult<Product> GetById(int id)
        {
            return new Product()
            {
                Id = id
            };
        }
        ////Get: baseUrl/api/Product/
        //[HttpGet]
        //public ActionResult<Product> GetAll()
        //{
        //    return new Product()
        //    {
        //        Id = 100
        //    };
        //}
        //Post: baseUrl/api/Product
        [HttpPost]
        public ActionResult<Product> Add(Product product)
        {
            return product;
        }

        //Put: baseUrl/api/Product
        [HttpPut]
        public ActionResult<Product> Update(Product product)
        {
            return product;
        }
        //Delete: baseUrl/api/Product
        [HttpDelete]
        public ActionResult<Product> Delete(Product product)
        {
            return product;
        }
    }
}
