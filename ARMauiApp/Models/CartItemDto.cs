using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMauiApp.Models
{
    public partial class CartItemDto : ObservableObject
    {
        [ObservableProperty] private string productId = "";
        [ObservableProperty] private string productName = "";
        [ObservableProperty] private int quantity;
        [ObservableProperty] private decimal price;
        [ObservableProperty] private string imageUrl = "";
        [ObservableProperty] private string model3DUrl = "";
    }
}
