using ARMauiApp.Models;
using ARMauiApp.Services;
#if ANDROID
using Android.Content;
#endif

namespace ARMauiApp.ViewModels
{
    [QueryProperty(nameof(ProductId), "ProductId")]
    public partial class ProductDetailViewModel : ObservableObject
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private readonly CartService _cartService;


        [ObservableProperty]
        private string productId = string.Empty;

        [ObservableProperty]
        private ProductDto? product;

        [ObservableProperty]
        private bool isLoading = false;

        [ObservableProperty]
        private string selectedSize = string.Empty;

        [ObservableProperty]
        private string categoryName = string.Empty;

        [ObservableProperty]
        private bool isAddingToCart = false;

        public ProductDetailViewModel(ProductService productService, CategoryService categoryService, CartService cartService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _cartService = cartService;
        }

        [RelayCommand]
        async Task LoadProduct()
        {
            if (string.IsNullOrEmpty(ProductId) || IsLoading) return;

            IsLoading = true;

            try
            {
                Product = await _productService.GetProductByIdAsync(ProductId);

                if (Product?.Sizes?.Any() == true)
                {
                    SelectedSize = Product.Sizes.First();
                }

                // Load category name
                if (Product != null && !string.IsNullOrEmpty(Product.CategoryId))
                {
                    var category = await _categoryService.GetCategoryByIdAsync(Product.CategoryId);
                    CategoryName = category?.Name ?? Product.CategoryId;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Lỗi", $"Không thể tải sản phẩm: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        async Task AddToCart()
        {
            if (Product == null) return;

            try
            {
                isAddingToCart = true;
                var result = await _cartService.AddToCartAsync(Product.Id, 1); // Số lượng mặc định là 1
                if (result != null)
                {
                    await Shell.Current.DisplayAlert("Thành công", "Đã thêm vào giỏ hàng!", "OK");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Lỗi", "Không thể thêm vào giỏ hàng.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Lỗi", $"Không thể thêm vào giỏ hàng: {ex.Message}", "OK");
            }
            finally
            {
                isAddingToCart = false;
            }
        }

        partial void OnProductIdChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _ = LoadProductAsync();
            }
        }

        private async Task LoadProductAsync()
        {
            await LoadProduct();
        }
    }
}
