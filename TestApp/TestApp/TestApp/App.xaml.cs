﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace TestApp
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			//MainPage = new TestApp.Page1();
            //Made this use NavigationPage so we could go back and forth between pages
            MainPage = new NavigationPage(new TestApp.MainViewPage());
		}


		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
