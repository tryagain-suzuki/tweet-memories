using TweetMemories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetMemories.Services
{
    public class TweetDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        const string ApiKey = "VaO4D9t9htBbJUs8WsjbEzqrN";
        const string ApiSecret = "eFU4iRV3MqiucXThi43CnB9xs2tB6I2pAaIBy3vQVhcqVuqP0j";
        const string AccessToke = "987883569883238400-2k2los20vWRCHUn03yIJVydOYGcSPm5";
        const string AccessTokeSecret = "1rxUah1rdRRzNkMTD0cKDuFaCA3vC9M2D5mC2CC2J1hWs";

        readonly CoreTweet.Tokens _token;

        public TweetDataStore()
        {
            _token = CoreTweet.Tokens.Create(ApiKey, ApiSecret, AccessToke, AccessTokeSecret);
            var status = _token.Statuses.HomeTimelineAsync(count => 50).Result;
            items = new List<Item>();
            foreach (var tweet in status)
            {
                items.Add(new Item
                {
                    Id = tweet.User.Name,
                    Name = tweet.User.ScreenName,
                    Description = tweet.Text,
                });
            }

            items = new List<Item>()
            {
                new Item { Id = "@" + Guid.NewGuid().ToString(), Name = "TryAgain鈴木", Text = "First item", Description="This is an item description." },
                new Item { Id = "@" + Guid.NewGuid().ToString(), Name = "TryAgain鈴木@やる気スイッチ押したい、だけどきっと押せないと思う", Text = "Second item", Description="This is an item description." },
                new Item { Id = "@" + Guid.NewGuid().ToString(), Name = "TryAgain鈴木@やる気スイッチ押したい", Text = "Third item", Description="This is an item description." },
                new Item { Id = "@" + Guid.NewGuid().ToString(), Name = "TryAgain鈴木@やる気スイッチ押したい", Text = "Fourth item", Description="This is an item description." },
                new Item { Id = "@" + Guid.NewGuid().ToString(), Name = "TryAgain鈴木@やる気スイッチ押したい", Text = "Fifth item", Description="This is an item description." },
                new Item { Id = "@" + Guid.NewGuid().ToString(), Name = "TryAgain鈴木@やる気スイッチ押したい", Text = "Sixth item", Description="This is an item description." }
            };
        }


        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}