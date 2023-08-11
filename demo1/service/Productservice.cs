using Amazon.Runtime.Internal.Util;
using AutoMapper;
using demo1.Data;
using demo1.model;
using demo1.service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace demo1.service
{
    public class ProductService : IProductservice
    {
        private readonly ApplicationDbContext _dpContext;
        private IMapper _mapper;

        public ProductService(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _dpContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<product> AddProduct(product Product)
        {

            var data = await _dpContext.Product.AddAsync(Product);
            _dpContext.SaveChanges();
            return data.Entity;
        }

        public async Task<JsonResponse> AddCategory(Category categories)
        {
            try
            { 
                  var data = await _dpContext.Category.AddAsync(categories);
                 _dpContext.SaveChanges();
                  return new JsonResponse(200, true, " New Category added success");
            }
            catch (Exception)
            {
                return new JsonResponse(200, false, "Not Found");
            }
        }
        public async Task<JsonResponse> AddImg(ProductImg ProImg)
        {
            try
            {
                var data =  await _dpContext.ProductImg.AddAsync(ProImg);
                _dpContext.SaveChanges();
                return new JsonResponse(200, true, " New Product Image added success");
            }
            catch (Exception)
            {
                return new JsonResponse(200, false, "Not Found");
            }
        }
       

        public List<Result> GetSearch(string? Name, string? CategoryName, string? param, int pageno, int records, string dire)
        {
            var query = (from category in _dpContext.Category
                         join product in _dpContext.Product
                         on category.CategoryId equals product.CategoryId
                         select new Result
                         {
                             ProductId = product.ProductId,
                             Name = product.Name,
                             price = product.price,
                             Description = product.Description,
                             CategoryName = category.CategoryName
                         }).Where(x =>
                         (x.Name.Contains(Name) || Name == null) &&
                         (x.CategoryName.Contains(CategoryName) || CategoryName == null)
                        ).Skip((pageno - 1) * 10)
                        .Take(records);

            if (param == "ProductId")
            {
                query = (dire == "asc") ? query.OrderBy(p => p.ProductId) : query.OrderByDescending(p => p.ProductId);
            }
            else if (param == "Name")
            {
                query = (dire == "asc") ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
            }
            else if (param == "price")
            {
                query = (dire == "asc") ? query.OrderBy(p => p.price) : query.OrderByDescending(p => p.price);
            }
            else if (param == "Description")
            {
                query = (dire == "asc") ? query.OrderBy(p => p.Description) : query.OrderByDescending(p => p.Description);
            }
            else if (param == "CategoryName")
            {
                query = (dire == "asc") ? query.OrderBy(p => p.CategoryName) : query.OrderByDescending(p => p.CategoryName);
            }
            else
            {
                query = (dire == "asc") ? query.OrderBy(p => p.ProductId) : query.OrderByDescending(p => p.ProductId);
            }

            /*if (!String.IsNullOrEmpty(Name))
            {
                query = query.Where(x => x.Name.Contains(Name));
            }
            if (!String.IsNullOrEmpty(CategoryName))
            {
                query = query.Where(x => x.CategoryName.Contains(CategoryName));
            }*/
            return query.ToList();
        }

        public JsonResponse GetProducts()
        {
            try
            {
                var p = _dpContext.Product.ToList();
                return new JsonResponse(200, true, "Displayed",p);
            }
            catch (Exception)
            {
                return new JsonResponse(200, false, "Not Found");
            }
        }

        public JsonResponse GetCategories()
        {
            try
            {
                var c = _dpContext.Category.ToList();
                return new JsonResponse(200, true, "Displayed", c);

            }
            catch (Exception)
            {
                return new JsonResponse(200, false, "Not Found");
            }
        }
        public JsonResponse GetProductImg()
        {
            try
            {
                var prod = _dpContext.ProductImg.ToList();
                return new JsonResponse(200, true, "Displayed", prod);
            }
            catch (Exception)
            {
                return new JsonResponse(200, false, "Not Found");
            }
        }


        public List<Result> GetResult()
        { 
                var data = (from Cate in _dpContext.Category
                            join Prod in _dpContext.Product
                              on Cate.CategoryId equals Prod.CategoryId
                            select new Result
                            {
                                ProductId = Prod.ProductId,
                                Name = Prod.Name,
                                price = Prod.price,
                                Description = Prod.Description,
                                CategoryName = Cate.CategoryName
                            });
                return data.ToList();
        }
        /*
        public List<Result> GetResultleft()
        {
            var dataleft = _dpContext.Result.FromSqlRaw("EXEC GetProductWithCategoryName").ToList();
            return dataleft;
        }*/
        public JsonResponse DeleteProduct(int ProductId)
        {
            try
            {
                var item = _dpContext.Product.Where(x => x.ProductId == ProductId).FirstOrDefault();
                _dpContext.Product.Remove(item);
                _dpContext.SaveChanges();
                return new JsonResponse(200, true, "Deleted Successfull");
            }
            catch (Exception)
            {
                return new JsonResponse(200, false, "Not Found");
            }
        }

        public JsonResponse DeleteProductImg(int ImgId)
        {
            try
            {
                var item = _dpContext.ProductImg.Where(x => x.ImgId == ImgId).FirstOrDefault();
                _dpContext.ProductImg.Remove(item);
                _dpContext.SaveChanges();
                return new JsonResponse(200, true, "Deleted Successfull");
            }
            catch (Exception)
            {
                return new JsonResponse(200, false, "Not Found");
            }
        }

        public JsonResponse UpdateProduct(product newproduct)
        {
            try
            {
                var DataList = _dpContext.Product.Where(x => x.ProductId == newproduct.ProductId).FirstOrDefault();
                DataList.Name = newproduct.Name;
                DataList.price = newproduct.price;
                DataList.Description = newproduct.Description;
                _dpContext.Product.Update(DataList);
                _dpContext.SaveChanges();
                return new JsonResponse(200, true, "Updated ");
            }
            catch (Exception)
            {
                return new JsonResponse(200, false, "Not Found");
            }
        }

        public JsonResponse UpdateProductImg(ProductImg productimg)
        {
            try
            {
                var DataList = _dpContext.ProductImg.Where(x => x.ImgId == productimg.ImgId).FirstOrDefault();
                DataList.ProductId = productimg.ProductId;
                DataList.Image = productimg.Image;
                _dpContext.ProductImg.Update(DataList);
                _dpContext.SaveChanges();
                return new JsonResponse(200, true, "Updated Successfull");

            }
            catch (Exception)
            {
                return new JsonResponse(200, false, "Not Found");
            }
        }

        public JsonResponse AddUpdate(product newproduct)
        {
            try
            {
                if (newproduct.ProductId != 0)
                {
                    var find = _dpContext.Product.Where(x => x.Name == newproduct.Name && x.ProductId != newproduct.ProductId).FirstOrDefault();
                    if (find == null)
                    {
                        var DataList = _dpContext.Product.Where(x => x.ProductId == newproduct.ProductId).FirstOrDefault();
                        DataList.CategoryId = newproduct.CategoryId;
                        DataList.Name = newproduct.Name;
                        DataList.price = newproduct.price;
                        DataList.Description = newproduct.Description;
                        _dpContext.Product.Update(DataList);
                        _dpContext.SaveChanges();
                        return new JsonResponse(200,true,"update success");
                    }
                    else
                    {
                        return new JsonResponse(200, false, "Product Name !!1");
                    }
                }
                else
                {
                    var find = _dpContext.Product.Where(x => x.Name == newproduct.Name).FirstOrDefault();
                    if (find == null)
                    {
                        var data = _dpContext.Product.AddAsync(newproduct);
                        _dpContext.SaveChanges();
                        return new JsonResponse(200, true, " new Product added success");
                    }
                    else
                    {
                        return new JsonResponse(200, false, "Product Name Is already Taken");
                    }
                }                
            }
            catch (Exception)
            {
                return new JsonResponse(200, false, "Not Found");
            }
        }
        public async Task<JsonResponse> AddOrder(OrderRequest ord)
        {
            try
            {
                Order order = new Order()
                {
                    CustomerId = ord.CustomerId,
                    Status = "Success",
                };
                await _dpContext.OrderTB.AddAsync(order);
                _dpContext.SaveChanges();

                List<OrderItemRequest> list = new List<OrderItemRequest>();
                list = ord.OrderItems;

                List<OrderItem> ordlist = new List<OrderItem>();

                decimal totalPrice = 0;

                foreach (var item in list)
                {
                    var DataList = _dpContext.Product.Where(x => x.ProductId == item.ProductId).FirstOrDefault();

                    OrderItem ord1 = new OrderItem();
                    
                    ord1 = _mapper.Map<OrderItem>(item);
                    ord1.OrderId = order.OrderId;
                    ord1.ItemTotal = DataList.price * item.Quantity;

                    /*OrderItem orditm = new OrderItem()
                    {
                        OrderId = order.OrderId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        ItemTotal = DataList.price * item.Quantity
                    };*/
                    totalPrice += ord1.ItemTotal;
                    ordlist.Add(ord1);
                    //await _dpContext.OrderItemTB.AddAsync(orditm);
                    //_dpContext.SaveChanges

                }


                await _dpContext.OrderItemTB.AddRangeAsync(ordlist);
                await _dpContext.SaveChangesAsync();

                /* var orderTotal = _dpContext.OrderItemTB
                                          .Where(x => x.OrderId == order.OrderId)
                                          .Sum(x => x.ItemTotal);
                */
                order.OrderTotal = totalPrice;

                _dpContext.OrderTB.Update(order);
                _dpContext.SaveChanges();

                return new JsonResponse(200, true, " New Order Added Successfull");
            }
            catch (Exception)
            {
                return new JsonResponse(200, false, "Not Found");
            }
        }
        public JsonResponse GetOrder(int OrderId)
        {
            try { 
                   var query = _dpContext.OrderTB.Where(x => x.OrderId == OrderId).FirstOrDefault();

                    GetOrders ord1 = new GetOrders();

                    ord1 = _mapper.Map<GetOrders>(query);

                    var que = _dpContext.OrderItemTB.Where(x => x.OrderId == OrderId).ToList();
            
                    List<GetOrdItem> ordlist = new List<GetOrdItem>();

                    foreach (var item in que)
                    {
                        var prod =  _dpContext.Product.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
                        GetOrdItem itm = new GetOrdItem();
                        itm = _mapper.Map<GetOrdItem>(item);
                        itm.Name = prod.Name;
                        itm.price = prod.price;
                        ordlist.Add(itm);
                    }
                    ord1.OrdItem = ordlist;
                    //return ord1;
                    return new JsonResponse(200, true, "Displayed", ord1);
            }
            catch (Exception)
            {
                return new JsonResponse(200, false, "Not Found");
            }
        }

        public JsonResponse GetOrder2(int OrderId) 
        {
            try
            {
                var query = (from order in _dpContext.OrderTB
                            join orderItem in _dpContext.OrderItemTB on order.OrderId equals orderItem.OrderId
                            join product in _dpContext.Product on orderItem.ProductId equals product.ProductId
                            where order.OrderId == OrderId
                            select new 
                            {
                               OrderId = order.OrderId,
                               CustomerId = order.CustomerId,
                               OrderDate =  order.OrderDate,
                               Status = order.Status,
                               OrderTotal = order.OrderTotal,
                                OrdItem = new GetOrdItem()                  
                                {
                                 ItemId = orderItem.ItemId,
                                 ProductId = orderItem.ProductId,
                                 Name = product.Name,
                                 price = product.price,
                                 Quantity = orderItem.Quantity,
                                ItemTotal = orderItem.ItemTotal
                                }
                            }).ToList();

                var ord = query.GroupBy(od => new { od.OrderId, od.CustomerId, od.OrderDate, od.Status, od.OrderTotal }).Select(g => new GetOrders
                {
                    OrderId = g.Key.OrderId,
                    CustomerId = g.Key.CustomerId,
                    OrderDate = g.Key.OrderDate,
                    Status = g.Key.Status,
                    OrderTotal = g.Key.OrderTotal,
                    OrdItem = g.Select(od => od.OrdItem).ToList()
                }).FirstOrDefault();
                

                return new JsonResponse(200, true, "Displayed",ord);

            }
            catch (Exception)
            {
                return new JsonResponse(200, false, "Not Found");
            }
        }

        public JsonResponse GetOrderFromProcedure()
        {
            try
            {
                var prod = _dpContext.Result.FromSqlRaw("EXEC GetProductWithCategoryName").ToList();
                return new JsonResponse(200, true, "Displayed", prod);

            }
            catch (Exception)
            {
                return new JsonResponse(200, false, "Not Found");
            }
        }

        public async Task<JsonResponse> AddOrderItem(OrderItem orditm)
        {
            try
            {
                var data = await _dpContext.OrderItemTB.AddAsync(orditm);
                _dpContext.SaveChanges();
                return new JsonResponse(200, true, " New OrderItem added success");
            }
            catch (Exception)
            {
                return new JsonResponse(200, false, "Not Found");
            }
        }
    }
}