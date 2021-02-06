using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetMemories.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TweetMemories.Views
{
    public partial class ReplyPage : ContentPage
    {
        public ReplyPage()
        {
            InitializeComponent();
            BindingContext = new ReplyViewModel();
        }
    }
}