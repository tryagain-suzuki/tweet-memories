using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TweetMemories.Models;
using TweetMemories.Services;
using Xamarin.Forms;

namespace TweetMemories.ViewModels
{
    [QueryProperty(nameof(TweetId), nameof(TweetId))]
    class ReplyViewModel : BaseViewModel
    {
        // 返信先ツイート
        private long tweetId;
        private string text;
        private string description;
        private string iconUrl;
        private string name;
        private string id;


        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string IconUrl
        {
            get => iconUrl;
            set => SetProperty(ref iconUrl, value);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        public long TweetId
        {
            get
            {
                return tweetId;
            }
            set
            {
                tweetId = value;
                LoadTweetId(value);
            }
        }

        // 自分の情報
        public MyUserDataStore MyDataStore => DependencyService.Get<MyUserDataStore>();
        public UserInfo MyInfo { get; private set; }

        private string tweet;
        public string Tweet
        {
            get => tweet;
            set => SetProperty(ref tweet, value);
        }
        public Command ReplyCommand { get; }

        const string ApiKey = "cL3xwz4hYxBf3NaIugCEmMUzN";
        const string ApiSecret = "EGkca9bfoLTn6S7YExHLMJ2hc6gstIOpdyMVz8zpsW8NZTnFQ1";
        const string AccessToke = "987883569883238400-S2BOzTyRcQckMYiFOQ4jS8Uk02AlRFO";
        const string AccessTokeSecret = "gm1ONHPiaLauNv67UXtUBUbmwW17gW8BFxPO8uZhhVAVD";

        public readonly CoreTweet.Tokens _token;

        public ReplyViewModel()
        {
            Title = "Reply";
            ReplyCommand = new Command(OnReply);
            _token = CoreTweet.Tokens.Create(ApiKey, ApiSecret, AccessToke, AccessTokeSecret);
        }

        private bool ValidateReply()
        {
            return !String.IsNullOrWhiteSpace(tweet);
        }

        public async void LoadTweetId(long tweetId)
        {
            try
            {
                MyInfo = await MyDataStore.GetUserInfo();
                var item = await DataStore.GetItemAsync(tweetId);
                Id = item.Id;
                IconUrl = item.IconUrl;
                Description = item.Description;
                Name = item.Name;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private async void OnReply()
        {
            _token.Statuses.Update(
                status: Tweet,
                in_reply_to_status_id: TweetId
            );

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
