using System.Collections.Generic;

namespace CMS.Presentation.Models
{
    public class CartVM
    {
        // Sepete eklenen ürün bizim için artık bir CartItem tipinde olacaktır.
        public List<CartItem> CartItems { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
