namespace HistoricalGiftsShop.Common.Enums
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public enum OrderStatusType
    {
        [Display(Name = "Създадена")]
        Created = 1,
        [Display(Name = "Потвърдена")]
        Confirmed = 2,
        [Display(Name = "Изпратена")]
        Sent = 3,
        [Display(Name = "Получена")]
        Delivered = 4,
        [Display(Name = "Приета")]
        Accepted = 5,
        [Display(Name = "Отказана")]
        Refused = 6,
    }
}
