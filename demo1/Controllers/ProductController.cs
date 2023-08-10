using demo1.Migrations;
using demo1.model;
using demo1.service;
using demo1.service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Routing.Constraints;
using Newtonsoft.Json;

namespace demo1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductservice _product;

        public ProductController(IProductservice ProductService)
        {
            _product = ProductService;

        }


        
        [HttpPost("AddProduct")]
        public async Task<Object> AddProduct([FromBody] product Product)

        {
            try
            {
                product p = await _product.AddProduct(Product);
                return p;
            }
            catch (Exception)
            {

                return false;
            }
        }

        [HttpPost("AddCategory")]
        public async Task<JsonResponse> AddCategory([FromBody] Category categories)

        {
                JsonResponse p = await _product.AddCategory(categories);
                return p;
            
        }

        [HttpPost("AddProductIMg")]
        public async Task<JsonResponse> AddImg(IFormCollection formdata,int productid)

        {
                ProductImg p = new ProductImg();
                IFormFile file = formdata.Files[0];

                using(var memorySrem = new MemoryStream())
                {
                    file.CopyTo(memorySrem);
                    p.Image = memorySrem.ToArray();
                }
                p.ProductId = productid;
                p.FileName = file.FileName;
                p.FileContentType = file.ContentType;
                string ext = Path.GetExtension(file.FileName);
                p.FileExtension = ext;
                var newproductImage = await _product.AddImg(p);
                return newproductImage;
            
        }
        [AllowAnonymous]
        [HttpGet("GetSearch")]
        public IActionResult GetSearch(string? Name, string? CategoryName, string? param, int pageno, int records, string dire)
        {
           
            var res = _product.GetSearch(Name, CategoryName, param, pageno, records, dire);
            var json = JsonConvert.SerializeObject(res, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
            );
            return Ok(json);
        }
        [AllowAnonymous]
        [HttpGet("GetAllProduct")]
        public IActionResult GetProducts()
        {
            var data = _product.GetProducts();
            var json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
            );
            return Ok(json);
        }

        [HttpGet("GetAllCategory")]
        public IActionResult GetCategories()
        {
            var Cat = _product.GetCategories();
            var json = JsonConvert.SerializeObject(Cat, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
            );
            return Ok(json);

        }

        [HttpGet("GetProductImg")]
        public IActionResult GetProductImg()
        {
            var Cat = _product.GetProductImg();
            var json = JsonConvert.SerializeObject(Cat, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
            );
            return Ok(json);

        }


        [HttpGet("GetResult")]
        public JsonResponse GetResult()
        {
            var res = _product.GetResult();
            
            return new JsonResponse(200,true,"success",res);
        }
        [AllowAnonymous]
        [HttpGet("GetOrder")]
        public JsonResponse  GetOrder(int OrderId)
        {
            var res = _product.GetOrder(OrderId);
            var json = JsonConvert.SerializeObject(res, Formatting.Indented,new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
            //return json;
            return new JsonResponse(200, true, "success", res);
        }

        [AllowAnonymous]
        [HttpGet("GetOrderById")]
        public JsonResponse GetOrder2(int OrderId)
        {
            var res = _product.GetOrder2(OrderId);
            //return json;
            return new JsonResponse(200, true, "success", res);
        }


        /*
        [HttpGet("GetResultleft")]
        public IActionResult GetResultleft()
        {
            var res = _product.GetResultleft();
            var json = JsonConvert.SerializeObject(res, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
            );
            return Ok(json);
        }
        */

        [HttpDelete("DeleteProduct")]
        public JsonResponse DeleteProduct(int ProductId)
        {
             var del = _product.DeleteProduct(ProductId);
                return del;   
        }

        [HttpDelete("DeleteProductImg")]
        public JsonResponse DeleteProductImg(int ImgId)
        {
               var jso = _product.DeleteProductImg(ImgId);
                return jso;   
        }

        [HttpPut("UpdateProduct")]
        public JsonResponse UpdateProduct(product newproduct)
        {
               var json = _product.UpdateProduct(newproduct);
                return json;            
        }

        [HttpPut("UpdateProductImg")]
        public JsonResponse UpdateProductImg(ProductImg productimg)
        {
              var res =  _product.UpdateProductImg(productimg);
                return res;
          
        }
        [HttpPost("AddUpdate")]
        public JsonResponse AddUpdate(product newproduct)
        {
               var repone = _product.AddUpdate(newproduct);
                return repone;
        }

        [AllowAnonymous]
        [HttpPost("AddOrder")]
        public async Task<JsonResponse> AddOrder([FromBody] OrderRequest ord)

        {
            JsonResponse p = await _product.AddOrder(ord);
            return p;

        }
        [AllowAnonymous]
        [HttpPost("AddOrderItem")]
        public async Task<JsonResponse> AddOrderItem([FromBody] OrderItem orditm)

        {
            JsonResponse p = await _product.AddOrderItem(orditm);
            return p;

        }
    }
}
