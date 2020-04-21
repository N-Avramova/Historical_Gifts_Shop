namespace HistoricalGiftsShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public enum PaymentType
    {
        [Display(Name = "Кредитна или Дебитна карта")]
        eBorica,
        [Display(Name = "PayPal")]
        PayPal,
        [Display(Name = "Наложен платеж")]
        CashOnDelivery,
    }
}
