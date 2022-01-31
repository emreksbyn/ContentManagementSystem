using CMS.Domain.Entities.Concrete;

namespace CMS.Presentation.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public string Image { get; set; }
        // İlk önce sepetin boş constr. ihtiyacımız var
        public CartItem() { }
        // Sepete ürün eklendiğince çalışacak olan ctor.
        public CartItem(Product product)
        {
            ProductId = product.Id;
            ProductName = product.Name;
            Price = product.Price;
            Quantity = 1;
            Image = product.ImagePath;
        }
    }
}
