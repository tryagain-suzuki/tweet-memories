﻿using TweetMemories.Models;
using TweetMemories.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using TweetMemories.Services;
using System.Collections.Generic;

namespace TweetMemories.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;

        public ObservableCollection<Item> Items { get; }

        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);

            LikeCommand = new Command<Item>(OnLike);
            RetweetCommand = new Command<Item>(OnRetweet);
            ReplyCommand = new Command<Item>(OnReply);

            _token = CoreTweet.Tokens.Create(ApiKey, ApiSecret, AccessToke, AccessTokeSecret);

        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }

        private IMessageBoxService _messageBoxService => DependencyService.Get<IMessageBoxService>();



        const string ApiKey = "cL3xwz4hYxBf3NaIugCEmMUzN";
        const string ApiSecret = "EGkca9bfoLTn6S7YExHLMJ2hc6gstIOpdyMVz8zpsW8NZTnFQ1";
        const string AccessToke = "987883569883238400-S2BOzTyRcQckMYiFOQ4jS8Uk02AlRFO";
        const string AccessTokeSecret = "gm1ONHPiaLauNv67UXtUBUbmwW17gW8BFxPO8uZhhVAVD";

        public readonly CoreTweet.Tokens _token;

        public Command<Item> LikeCommand { get; }
        public Command<Item> RetweetCommand { get; }
        public Command<Item> ReplyCommand { get; }


        private async void OnLike(Item item)
        {
            try
            {
                if (!(bool)Items[Items.IndexOf(item)].IsLiked)
                {
                    Items[Items.IndexOf(item)].IsLiked = true;
                    Items[Items.IndexOf(item)].LikeColor = "#e0245e";
                    Items[Items.IndexOf(item)].Like += 1;
                    await _token.Favorites.CreateAsync(item.TweetId);
                }
                else
                {
                    Items[Items.IndexOf(item)].IsLiked = false;
                    Items[Items.IndexOf(item)].LikeColor = "Gray";
                    Items[Items.IndexOf(item)].Like -= 1;
                    await _token.Favorites.DestroyAsync(item.TweetId);
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }


        private async void OnRetweet(Item item)
        {
            try
            {
                List<string> retweetText = new List<string> { "リツイート", "リツイートを取り消す" };
                string retweetOnOff = null;
                if (!(bool)Items[Items.IndexOf(item)].IsRetweeted)
                {
                    retweetOnOff = "リツイート";
                }
                else
                {
                    retweetOnOff = "リツイートを取り消す";
                }
                var action = await _messageBoxService.ShowActionSheet(null, null, null, new string[] { retweetOnOff, "引用ツイート" });

                if (retweetText.Contains(action)) { 
                    if (!(bool)Items[Items.IndexOf(item)].IsRetweeted)
                    {
                        Items[Items.IndexOf(item)].IsRetweeted = true;
                        Items[Items.IndexOf(item)].RetweetColor = "#17BF63";
                        Items[Items.IndexOf(item)].Retweet += 1;
                        await _token.Statuses.RetweetAsync(item.TweetId);
                    }
                    else
                    {
                        Items[Items.IndexOf(item)].IsRetweeted = false;
                        Items[Items.IndexOf(item)].RetweetColor = "Gray";
                        Items[Items.IndexOf(item)].Retweet -= 1;
                        await _token.Statuses.UnretweetAsync(item.TweetId);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        async void OnReply(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ReplyPage)}?{nameof(ReplyViewModel.TweetId)}={item.TweetId}");
        }

    }
}