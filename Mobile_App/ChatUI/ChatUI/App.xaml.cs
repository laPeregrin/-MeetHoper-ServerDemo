﻿using ChatUI.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatUI
{
    public partial class App : Application
    {
        public static string User = "User";
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
