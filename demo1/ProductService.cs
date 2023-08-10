using demo1.model;

namespace demo1
{
    public class ProductService
    {
         void addproduct()
        {
            Console.WriteLine("bat polising ");
        }

        void deleteprduct()
        {

        }

        void updateproduct()
        {

        }

        product GetproductbyProductID(int id)
        {
            return new product();   
        }

        List<product> Getproduct()
        {
            List<product> products = new List<product>();
            return products;
            
        }
    
    }
}
