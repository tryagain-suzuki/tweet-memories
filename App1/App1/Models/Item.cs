using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TweetMemories.Models
{
    public class Item : INotifyPropertyChanged
    {

        private int? like;
        private int? retweet;
        private int? reply;
        private bool? isLiked;
        private bool? isRetweeted;
        private string likeColor;
        private string likeBackgroundColor;
        private string retweetColor;
        private string retweetBackgroundColor;



        public long TweetId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public int? Like
        {
            get { return like; }
            set { SetProperty(ref like, value); }
        }
        public int? Retweet
        {
            get { return retweet; }
            set { SetProperty(ref retweet, value); }
        }
        public int? Reply
        {
            get { return reply; }
            set { SetProperty(ref reply, value); }
        }
        public bool? IsLiked
        {
            get { return isLiked; }
            set { SetProperty(ref isLiked, value); }
        }
        public bool? IsRetweeted
        {
            get { return isRetweeted; }
            set { SetProperty(ref isRetweeted, value); }
        }
        public string LikeColor
        {
            get { return likeColor; }
            set { SetProperty(ref likeColor, value); }
        }
        public string LikeBackgroundColor
        {
            get { return likeBackgroundColor; }
            set { SetProperty(ref likeBackgroundColor, value); }
        }
        public string RetweetColor
        {
            get { return retweetColor; }
            set { SetProperty(ref retweetColor, value); }
        }
        public string RetweetBackgroundColor
        {
            get { return retweetBackgroundColor; }
            set { SetProperty(ref retweetBackgroundColor, value); }
        }



        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
