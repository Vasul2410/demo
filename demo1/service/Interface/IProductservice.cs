using demo1.model;
using Microsoft.AspNetCore.Mvc;

namespace demo1.service.Interface
{
    public interface IProductservice
    {
        public  Task<product> AddProduct(product Product);

        public JsonResponse AddUpdate(product newproduct);

        public Task<JsonResponse> AddCategory(Category categories);

        public Task<JsonResponse> AddImg(ProductImg ProImg);

        public JsonResponse GetProducts();

        public JsonResponse GetCategories();

        public JsonResponse GetProductImg();

        public List<Result> GetResult();

        /*public List<Result> GetResultleft();*/

        public List<Result> GetSearch(string? Name, string? CategoryName, string? param, int pageno, int records , string dire);

        public JsonResponse DeleteProduct(int ProductId);

        public JsonResponse DeleteProductImg(int ImgId);

        public JsonResponse UpdateProduct(product newproduct);

        public JsonResponse UpdateProductImg(ProductImg productimg);

        public Task<JsonResponse> AddOrder(OrderRequest ord);

        public Task<JsonResponse> AddOrderItem(OrderItem ord);

        public JsonResponse GetOrder(int OrderId);
        public JsonResponse GetOrder2(int OrderId);




    }
}
