﻿namespace HistoricalGiftsShop.Services.Data
{
    using System;
    using System.Threading.Tasks;

    public interface IBooksService
    {
        Task<int> CreateAsync(string title, string description, string author, string publisher, DateTime yearOfPublisher, int categoryId, int stock, decimal price, int bookCoverTypeId, int? pages, string language, string isbn);

        T GetById<T>(int id);
    }
}