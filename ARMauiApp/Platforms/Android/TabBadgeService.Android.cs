#if ANDROID
using Android.Util;
using ARMauiApp.Services;
using Google.Android.Material.Badge;
using Google.Android.Material.BottomNavigation;
using System;
using AView = Android.Views.View;
using AViewGroup = Android.Views.ViewGroup;

namespace ARMauiApp
{
    public class TabBadgeService : ITabBadgeService
    {
        const string TAG = "CartBadge";

        public void SetBadge(Shell shell, int tabIndex, int? count)
        {
            if (shell?.Handler?.PlatformView is not AViewGroup root) { Log.Warn(TAG, "PlatformView null"); return; }

            var bnv = FindBNV(root);
            if (bnv is null) { Log.Warn(TAG, "BottomNavigationView not found"); return; }

            var menu = bnv.Menu;
            Log.Info(TAG, $"Menu size={menu.Size()} tabIndex={tabIndex}");
            if (tabIndex < 0 || tabIndex >= menu.Size()) { Log.Warn(TAG, "tabIndex out of range"); return; }

            var item = menu.GetItem(tabIndex);
            var badge = bnv.GetOrCreateBadge(item.ItemId);

            if (!count.HasValue || count.Value <= 0)
            {
                badge.ClearNumber();
                badge.SetVisible(false);
                Log.Info(TAG, "Hide badge");
            }
            else
            {
                badge.Number = Math.Min(count.Value, 99);
                badge.SetVisible(true);

                // làm nổi bật để bạn dễ thấy
                badge.BackgroundColor = Android.Graphics.Color.ParseColor("#FF3B30").ToArgb(); // đỏ
                badge.BadgeTextColor = Android.Graphics.Color.White.ToArgb();

                Log.Info(TAG, $"Show badge = {badge.Number}");
            }
        }

        static BottomNavigationView? FindBNV(AView v)
        {
            if (v is BottomNavigationView b) return b;
            if (v is AViewGroup g)
            {
                for (int i = 0; i < g.ChildCount; i++)
                {
                    var f = FindBNV(g.GetChildAt(i));
                    if (f != null) return f;
                }
            }
            return null;
        }
    }
}
#endif
