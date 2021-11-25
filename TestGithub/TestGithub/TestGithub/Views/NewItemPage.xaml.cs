using System;
using System.Collections.Generic;
using System.ComponentModel;
using TestGithub.Models;
using TestGithub.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestGithub.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}