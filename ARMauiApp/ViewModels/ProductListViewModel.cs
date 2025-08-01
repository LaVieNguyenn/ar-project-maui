using ARMauiApp.Models;
using ARMauiApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ARMauiApp.ViewModels
{
    public partial class ProductListViewModel : ObservableObject
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;

        [ObservableProperty]
        private ObservableCollection<ProductDto> products = new();

        [ObservableProperty]
        private ObservableCollection<CategoryDto> categories = new();

        [ObservableProperty]
        private CategoryDto? selectedCategory;

        [ObservableProperty]
        private string searchText = string.Empty;

        [ObservableProperty]
        private bool isLoading = false;

        [ObservableProperty]
        private bool isRefreshing = false;

        public ICommand LoadProductsCommand { get; }
        public ICommand RefreshProductsCommand { get; }
        public ICommand SearchProductsCommand { get; }
        public ICommand ViewProductDetailCommand { get; }
        public ICommand LoadCategoriesCommand { get; }
        public ICommand FilterByCategoryCommand { get; }

        public ProductListViewModel(ProductService productService, CategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;

            // Đăng ký commands trong constructor
            LoadProductsCommand = new AsyncRelayCommand(LoadProducts);
            RefreshProductsCommand = new AsyncRelayCommand(RefreshProducts);
            SearchProductsCommand = new AsyncRelayCommand(SearchProducts);
            ViewProductDetailCommand = new AsyncRelayCommand<ProductDto?>(ViewProductDetail);
            LoadCategoriesCommand = new AsyncRelayCommand(LoadCategories);
            FilterByCategoryCommand = new AsyncRelayCommand<CategoryDto?>(FilterByCategory);
        }

        private async Task LoadProducts()
        {
            if (IsLoading) return;

            IsLoading = true;

            try
            {
                var categoryId = SelectedCategory?.Id ?? "";
                var productList = await _productService.GetProductsAsync(categoryId, SearchText);
                Products.Clear();

                foreach (var product in productList)
                {
                    Products.Add(product);
                }
            }
            catch (Exception ex)
            {
                // Handle error
                await Shell.Current.DisplayAlert("Lỗi", $"Không thể tải sản phẩm: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task RefreshProducts()
        {
            IsRefreshing = true;
            await LoadProducts();
            IsRefreshing = false;
        }

        private async Task SearchProducts()
        {
            // Reset category selection when searching
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                await LoadProducts();
                //SelectedCategory = Categories.FirstOrDefault(); // Set to "All Categories"
            }
        }

        private async Task ViewProductDetail(ProductDto? product)
        {
            if (product == null) return;

            var navigationParameter = new Dictionary<string, object>
            {
                ["ProductId"] = product.Id
            };

            await Shell.Current.GoToAsync("productdetail", navigationParameter);
        }

        public async Task Initialize()
        {
            await LoadCategories();
            await LoadProducts();
        }

        private async Task LoadCategories()
        {
            try
            {
                var categoryList = await _categoryService.GetCategoriesAsync();
                Categories.Clear();

                // Add "All Categories" option
                Categories.Add(new CategoryDto { Id = "", Name = "Tất cả", Description = "Hiển thị tất cả sản phẩm" });

                foreach (var category in categoryList)
                {
                    Categories.Add(category);
                }

                // Set default selection to "All Categories"
                SelectedCategory = Categories.FirstOrDefault();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Lỗi", $"Không thể tải danh mục: {ex.Message}", "OK");
            }
        }

        private async Task FilterByCategory(CategoryDto? category)
        {
            if (category == null) return;
            SelectedCategory = category;
            await LoadProducts();
        }

        // This method is called automatically when SearchText changes
        partial void OnSearchTextChanged(string value)
        {
            // Debounce search to avoid too many API calls
            _ = Task.Run(async () =>
            {
                await Task.Delay(500); // Wait 500ms
                if (SearchText == value) // Check if search text hasn't changed
                {
                    await MainThread.InvokeOnMainThreadAsync(async () => await SearchProducts());
                }
            });
        }
    }
}
