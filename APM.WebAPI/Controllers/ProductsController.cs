using APM.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.OData;

namespace APM.WebAPI.Controllers
{
    [EnableCorsAttribute("http://localhost:52436", "*", "*")]
    public class ProductsController : ApiController
    {
        // GET: api/Products
        ProductRepository productRepository = new ProductRepository();
        [EnableQuery()]
        public IHttpActionResult Get()
        {
            return Ok(productRepository.Retrieve().AsQueryable());
        }


        // GET: api/Products/5
        public IHttpActionResult Get(int id)
        {
            Product product;

            if (id > 0)
            {
                var products = productRepository.Retrieve();
                product = products.FirstOrDefault(p => p.ProductId == id);
                if (product == null)
                {
                    return NotFound();
                }
            }
            else
            {
                product = productRepository.Create();
            }
            return Ok(product);
        }

        // POST: api/Products
        public IHttpActionResult Post([FromBody]Product product)
        {
            if (product == null)
            {
                return BadRequest("PRODUCT CANNOT BE NULL");
            }
            var newProduct = productRepository.Save(product);
            return Created<Product>(Request.RequestUri + newProduct.ProductId.ToString(), newProduct);

        }

        // PUT: api/Products/5
        public IHttpActionResult Put(int id, [FromBody]Product product)
        {

            if (product == null)
            {
                return BadRequest("PRODUCT CANNOT BE NULL");
            }
            var updateProduct = productRepository.Save(id, product);
            if (updateProduct == null)
            {
                return NotFound();
            }
            return Ok();

        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
        }
    }
}
