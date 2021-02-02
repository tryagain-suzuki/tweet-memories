using System;

namespace TweetMemories.Models
{
    public class Item
    {
        public long TweetId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public int? Like { get; set; }
        public int? Retweet { get; set; }
        public int? Reply { get; set; }
        public bool? IsLiked { get; set; }
        public bool? IsRetweeted { get; set; }
        public string LikeColor { get; set; }
        public string LikeBackgroundColor { get; set; }
        public string RetweetColor { get; set; }
        public string RetweetBackgroundColor { get; set; }
    }
}