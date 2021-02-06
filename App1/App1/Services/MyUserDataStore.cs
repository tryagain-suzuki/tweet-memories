using TweetMemories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetMemories.Services
{
    public class MyUserDataStore
    {
        readonly UserInfo user;

        const string ApiKey = "cL3xwz4hYxBf3NaIugCEmMUzN";
        const string ApiSecret = "EGkca9bfoLTn6S7YExHLMJ2hc6gstIOpdyMVz8zpsW8NZTnFQ1";
        const string AccessToke = "987883569883238400-S2BOzTyRcQckMYiFOQ4jS8Uk02AlRFO";
        const string AccessTokeSecret = "gm1ONHPiaLauNv67UXtUBUbmwW17gW8BFxPO8uZhhVAVD";

        public readonly CoreTweet.Tokens _token;

        public MyUserDataStore()
        {
            _token = CoreTweet.Tokens.Create(ApiKey, ApiSecret, AccessToke, AccessTokeSecret);

            var twitterUser = _token.Account.VerifyCredentials();
            user = new UserInfo 
            {
                ScreenName = $@"@{twitterUser.ScreenName}",
                Name = twitterUser.Name,
                ProfileImageUrl = twitterUser.ProfileImageUrl,
                CreatedAt = twitterUser.CreatedAt,
                Description = twitterUser.Description,
                Url = twitterUser.Url,
                FollowersCount = twitterUser.FollowersCount,
                FriendsCount = twitterUser.FriendsCount,
            };
            
        }

        public async Task<UserInfo> GetUserInfo()
        {
            return await Task.FromResult(user);
        }

    }
}