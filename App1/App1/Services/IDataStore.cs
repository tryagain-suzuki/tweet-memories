﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TweetMemories.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<T> GetItemAsync(long tweetId);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
