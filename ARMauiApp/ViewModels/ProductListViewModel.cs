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

        // Promotions for the header carousel
        [ObservableProperty]
        private ObservableCollection<PromotionDto> promotions = new();

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
            LoadMockPromotions();
            await LoadProducts();
        }

        private void LoadMockPromotions()
        {
            try
            {
                Promotions.Clear();
                Promotions.Add(new PromotionDto
                {
                    ImageUrl = "https://snapfeet.io/wp-content/uploads/2023/04/Banner_home_ing_cut.png",
                    //DiscountText = "50-40% OFF",
                    //Headline = "Now in (product)",
                    //Subheadline = "All colours",
                    //ButtonText = "Shop Now",
                    TargetCategoryId = string.Empty
                });
                Promotions.Add(new PromotionDto
                {
                    ImageUrl = "https://tri3d.in/wp-content/uploads/2021/10/Virual-Try-on-banner-1-1400x788.png",
                    //DiscountText = "Summer Sale",
                    //Headline = "Best T-Shirts",
                    //Subheadline = "Up to 30%",
                    //ButtonText = "Shop Now",
                    TargetCategoryId = string.Empty
                });
                Promotions.Add(new PromotionDto
                {
                    ImageUrl = "https://gv-brxm.imgix.net/binaries/_ht_1694432584455/content/gallery/gb-visionexpress/content-pages/offers/new_branding/sun_sale_50_off/sun_sale_offer_banner_d.jpg?fit=fillmax&w=750&h=440&auto=format",
                    //DiscountText = "New Arrivals",
                    //Headline = "Trendy Shoes",
                    //Subheadline = "Fresh Styles",
                    //ButtonText = "Explore",
                    TargetCategoryId = string.Empty
                });
            }
            catch
            {
                // ignore mock load errors
            }
        }

        private async Task LoadCategories()
        {
            try
            {
                var categoryList = await _categoryService.GetCategoriesAsync();
                Categories.Clear();

                // Add "All Categories" option
                Categories.Add(new CategoryDto { Id = "", Name = "Tất cả", Description = "Hiển thị tất cả sản phẩm", ImageUrl = "https://t4.ftcdn.net/jpg/03/85/95/63/360_F_385956366_Zih7xDcSLqDxiJRYUfG5ZHNoFCSLMRjm.jpg", IsSelected = true });

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
            if (SelectedCategory != null)
            {
                SelectedCategory.IsSelected = false;
            }
            foreach (var item in Categories)
            {
                item.IsSelected = false;
            }
            SelectedCategory = category;
            SelectedCategory.IsSelected = true;
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
