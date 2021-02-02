using TweetMemories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetMemories.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        const string ApiKey = "cL3xwz4hYxBf3NaIugCEmMUzN";
        const string ApiSecret = "EGkca9bfoLTn6S7YExHLMJ2hc6gstIOpdyMVz8zpsW8NZTnFQ1";
        const string AccessToke = "987883569883238400-S2BOzTyRcQckMYiFOQ4jS8Uk02AlRFO";
        const string AccessTokeSecret = "gm1ONHPiaLauNv67UXtUBUbmwW17gW8BFxPO8uZhhVAVD";

        public readonly CoreTweet.Tokens _token;

        public MockDataStore()
        {
            _token = CoreTweet.Tokens.Create(ApiKey, ApiSecret, AccessToke, AccessTokeSecret);
            // var status = _token.Statuses.HomeTimelineAsync(count => 50).Result;
            var status = _token.Statuses.UserTimeline(count => 50);
            items = new List<Item>();
            foreach (var tweet in status)
            {
                items.Add(new Item
                {
                    TweetId = tweet.Id,
                    Id = "@" + tweet.User.ScreenName,
                    Name = tweet.User.Name,
                    Description = tweet.Text,
                    IconUrl = tweet.User.ProfileImageUrl,
                    Like = tweet.FavoriteCount,
                    Retweet = tweet.RetweetCount,
                    Reply = _token.Search.Tweets(q => "to:" + tweet.User.ScreenName, since_id => tweet.Id).Count,
                    IsLiked = tweet.IsFavorited,
                    IsRetweeted = tweet.IsRetweeted,
                    LikeBackgroundColor = (bool)tweet.IsFavorited ? "White" : "White",
                    LikeColor = (bool)tweet.IsFavorited ? "#e0245e" : "Gray",
                    RetweetBackgroundColor = (bool)tweet.IsRetweeted ? "White" : "White",
                    RetweetColor = (bool)tweet.IsRetweeted ? "#17BF63" : "Gray",
                });
            }

            //items = new List<Item>()
            //{
            //    new Item { Id = "@" + Guid.NewGuid().ToString(), Name = "TryAgain鈴木", Text = "First item", Description="This is an item description." },
            //    new Item { Id = "@" + Guid.NewGuid().ToString(), Name = "TryAgain鈴木@やる気スイッチ押したい、だけどきっと押せないと思う", Text = "Second item", Description="This is an item description." },
            //    new Item { Id = "@" + Guid.NewGuid().ToString(), Name = "TryAgain鈴木@やる気スイッチ押したい", Text = "Third item", Description="This is an item description." },
            //    new Item { Id = "@" + Guid.NewGuid().ToString(), Name = "TryAgain鈴木@やる気スイッチ押したい", Text = "Fourth item", Description="This is an item description." },
            //    new Item { Id = "@" + Guid.NewGuid().ToString(), Name = "TryAgain鈴木@やる気スイッチ押したい", Text = "Fifth item", Description="This is an item description." },
            //    new Item { Id = "@" + Guid.NewGuid().ToString(), Name = "TryAgain鈴木@やる気スイッチ押したい", Text = "Sixth item", Description="This is an item description." }
            //};
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