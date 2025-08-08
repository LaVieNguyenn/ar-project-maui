using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMauiApp.Models
{
    public class PlanDto
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int DurationMonths { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal FinalPrice => Math.Round(Price * (1 - DiscountPercent / 100));

        // UI only:
        public string UiBrushKey { get; set; } = "GradientPurple";
    }

}
