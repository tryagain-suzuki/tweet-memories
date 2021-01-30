using TweetMemories.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace TweetMemories.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}