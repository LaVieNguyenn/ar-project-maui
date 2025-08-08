using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMauiApp.Services
{
    public interface ITabBadgeService
    {
        /// <param name="shell">App.Current.MainPage as Shell</param>
        /// <param name="tabIndex">Vị trí tab trong TabBar (Giỏ hàng = 1 theo XAML của bạn)</param>
        /// <param name="count">Số hiển thị; null hoặc <= 0 sẽ ẩn badge</param>
        void SetBadge(Shell shell, int tabIndex, int? count);
    }

}
