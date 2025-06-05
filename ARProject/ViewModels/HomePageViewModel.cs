using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using ARProject.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ARProject.ViewModels
{
    public partial class HomePageViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        [ObservableProperty] 
        string title = "Trang chủ";

        // Properties để binding với UI
        [ObservableProperty]
        ObservableCollection<ProductItem> flashSaleProducts;
        

        [ObservableProperty]
        ObservableCollection<ProductItem> popularProducts;

        [ObservableProperty]
        ObservableCollection<ProductItem> justForYouProducts;

        // tạo danh sách nổi bật
        [ObservableProperty]
        ObservableCollection<ProductItem> featuredProducts;

        [ObservableProperty]
        ObservableCollection<Category> categories;

        public HomePageViewModel(IApiService apiService = null)
        {
            _apiService = apiService ?? DependencyService.Get<IApiService>();

            // Initialize collections
            FlashSaleProducts = new ObservableCollection<ProductItem>();
            PopularProducts = new ObservableCollection<ProductItem>();
            JustForYouProducts = new ObservableCollection<ProductItem>();
            FeaturedProducts = new ObservableCollection<ProductItem>();
            Categories = new ObservableCollection<Category>
            {
                new Category { Id = "1", Name = "Áo thun", ImageUrl = "category_tshirt.jpg" },
                new Category { Id = "2", Name = "Quần jeans", ImageUrl = "category_jeans.jpg" },
                new Category { Id = "3", Name = "Giày thể thao", ImageUrl = "category_sneakers.jpg" },
                new Category { Id = "4", Name = "Phụ kiện", ImageUrl = "category_accessories.jpg" }
            };

            // Tải dữ liệu ban đầu
            LoadDummyData();
        }

        [RelayCommand]
        private async Task LoadDataAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                // Trong thực tế, bạn sẽ gọi API để lấy dữ liệu từ đây
                // Ví dụ: var products = await _apiService.GetProductsAsync();
                
                // Hiện tại sẽ dùng dữ liệu giả lập
                LoadDummyData();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi ở đây
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void LoadDummyData()
        {
            // Tạo dữ liệu Flash Sale
            FlashSaleProducts.Clear();
            FlashSaleProducts.Add(new ProductItem { Id = "1", Name = "Basic T-shirt", Price = 17.00, ImageUrl = "product1.jpg", Discount = 20, IsNew = true });
            FlashSaleProducts.Add(new ProductItem { Id = "2", Name = "Simple Orange Shirt", Price = 17.00, ImageUrl = "product2.jpg", Discount = 20, IsSale = true });
            FlashSaleProducts.Add(new ProductItem { Id = "3", Name = "Elegant Dress", Price = 17.00, ImageUrl = "product3.jpg", Discount = 20, IsHot = true });
            FlashSaleProducts.Add(new ProductItem { Id = "4", Name = "Summer Top", Price = 17.00, ImageUrl = "product4.jpg", Discount = 20 });
            FlashSaleProducts.Add(new ProductItem { Id = "5", Name = "Denim Jacket", Price = 17.00, ImageUrl = "product5.jpg", Discount = 20 });
            FlashSaleProducts.Add(new ProductItem { Id = "6", Name = "Casual Leggings", Price = 17.00, ImageUrl = "product6.jpg", Discount = 20 });

            // Tạo dữ liệu Popular
            PopularProducts.Clear();
            PopularProducts.Add(new ProductItem { Id = "7", Name = "Women's Dress", Price = 17.00, ImageUrl = "product4.jpg", Rating = 4.5, IsNew = true });
            PopularProducts.Add(new ProductItem { Id = "8", Name = "Men's Shirt", Price = 17.00, ImageUrl = "product5.jpg", Rating = 4.2, IsSale = true });
            PopularProducts.Add(new ProductItem { Id = "9", Name = "Sport Shoes", Price = 17.00, ImageUrl = "product6.jpg", Rating = 4.8, IsHot = true });
            PopularProducts.Add(new ProductItem { Id = "10", Name = "Casual Jacket", Price = 17.00, ImageUrl = "product1.jpg", Rating = 4.0 });

            // Tạo dữ liệu Just For You
            JustForYouProducts.Clear();
            JustForYouProducts.Add(new ProductItem { Id = "11", Name = "Basic T-shirt", Price = 17.00, ImageUrl = "product_basic_tshirt.jpg" });
            JustForYouProducts.Add(new ProductItem { Id = "12", Name = "Simple Orange Shirt", Price = 17.00, ImageUrl = "product_orange_shirt.jpg" });
            JustForYouProducts.Add(new ProductItem { Id = "13", Name = "Elegant Dress", Price = 17.00, ImageUrl = "product_elegant_dress.jpg" });
            JustForYouProducts.Add(new ProductItem { Id = "14", Name = "Summer Casual Tops Crew Neck Short Sleeve Shirts", Price = 17.00, ImageUrl = "product_casual_top.jpg" });
            JustForYouProducts.Add(new ProductItem { Id = "15", Name = "Classic Denim Jacket", Price = 17.00, ImageUrl = "product_denim_jacket.jpg" });
            JustForYouProducts.Add(new ProductItem { Id = "16", Name = "EvoShield Women's Standard Leggings", Price = 17.00, ImageUrl = "product_leggings.jpg" });

            // Tạo dữ liệu Featured
            FeaturedProducts.Clear();
            FeaturedProducts.Add(new ProductItem { Id = "17", Name = "Stylish Hoodie", Price = 25.00, ImageUrl = "featured_hoodie.jpg", IsNew = true });
            FeaturedProducts.Add(new ProductItem { Id = "18", Name = "Trendy Sneakers", Price = 50.00, ImageUrl = "featured_sneakers.jpg", IsSale = true });
            FeaturedProducts.Add(new ProductItem { Id = "19", Name = "Classic Watch", Price = 100.00, ImageUrl = "featured_watch.jpg", IsHot = true });
            FeaturedProducts.Add(new ProductItem { Id = "20", Name = "Leather Backpack", Price = 75.00, ImageUrl = "featured_backpack.jpg" });
            FeaturedProducts.Add(new ProductItem { Id = "21", Name = "Vintage Sunglasses", Price = 30.00, ImageUrl = "featured_sunglasses.jpg" });
            FeaturedProducts.Add(new ProductItem { Id = "22", Name = "Wool Scarf", Price = 20.00, ImageUrl = "featured_scarf.jpg" });
        }

        [RelayCommand]
        private void ItemTapped(ProductItem item)
        {
            // Navigate to product details
            if (item != null)
            {
                // Điều hướng đến trang chi tiết sản phẩm
                // Shell.Current.GoToAsync($"{nameof(ProductDetailPage)}?id={item.Id}");
            }
        }

        [RelayCommand]
        private void AddToWishlist(ProductItem item)
        {
            // Add product to wishlist
            if (item != null)
            {
                // Thêm vào danh sách yêu thích
                item.IsWishlisted = !item.IsWishlisted;
            }
        }

        [RelayCommand]
        private void NavigateToAR(ProductItem item)
        {
            // Navigate to AR page
            if (item != null)
            {
                // Shell.Current.GoToAsync($"{nameof(ARPage)}?id={item.Id}");
            }
        }
    }

    // Lớp đơn giản để lưu trữ thông tin sản phẩm
    public class ProductItem : ObservableObject
    {
        private string _id;
        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private double _price;
        public double Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        private string _imageUrl;
        public string ImageUrl
        {
            get => _imageUrl;
            set => SetProperty(ref _imageUrl, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private double _rating;
        public double Rating
        {
            get => _rating;
            set => SetProperty(ref _rating, value);
        }

        private int _discount;
        public int Discount
        {
            get => _discount;
            set => SetProperty(ref _discount, value);
        }

        private bool _isWishlisted;
        public bool IsWishlisted
        {
            get => _isWishlisted;
            set => SetProperty(ref _isWishlisted, value);
        }

        private bool _isNew;
        public bool IsNew
        {
            get => _isNew;
            set => SetProperty(ref _isNew, value);
        }

        private bool _isSale;
        public bool IsSale
        {
            get => _isSale;
            set => SetProperty(ref _isSale, value);
        }

        private bool _isHot;
        public bool IsHot
        {
            get => _isHot;
            set => SetProperty(ref _isHot, value);
        }
    }

    public class Category : ObservableObject
    {
        private string _id;
        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        private string _imageUrl;
        public string ImageUrl
        {
            get => _imageUrl;
            set => SetProperty(ref _imageUrl, value);
        }
    }
}
