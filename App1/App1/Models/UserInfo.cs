using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TweetMemories.Models
{
    public class UserInfo 
    {
        public string ScreenName { get; set; }
        public string Name { get; set; }
        public string ProfileImageUrl { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public int FollowersCount { get; set; }
        public int FriendsCount { get; set; }

    }
}
