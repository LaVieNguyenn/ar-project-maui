using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMauiApp.Models
{
    public class PlanQrDto
    {
        public string AdminAccountNumber { get; set; } = "";
        public string AdminBankCode { get; set; } = "";
        public string AdminAccountName { get; set; } = "";
        public decimal Amount { get; set; }
        public string PaymentContent { get; set; } = "";
        public string QrCode { get; set; } = "";
    }
}
