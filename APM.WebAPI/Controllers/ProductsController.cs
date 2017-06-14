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
            try
            {
                return Ok(productRepository.Retrieve().AsQueryable());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        // GET: api/Products/5
        public IHttpActionResult Get(int id)
        {
            try
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
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Products
        public IHttpActionResult Post([FromBody]Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("PRODUCT CANNOT BE NULL");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);

                }
                var newProduct = productRepository.Save(product);
                return Created<Product>(Request.RequestUri + newProduct.ProductId.ToString(), newProduct);
            }
            catch (Exception ex)
            {
               return InternalServerError(ex);
            }

        }

        // PUT: api/Products/5
        public IHttpActionResult Put(int id, [FromBody]Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("PRODUCT CANNOT BE NULL");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);

                }
                var updateProduct = productRepository.Save(id, product);
                if (updateProduct == null)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
        }
    }
}
