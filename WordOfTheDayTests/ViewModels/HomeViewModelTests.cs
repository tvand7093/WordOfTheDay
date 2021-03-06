﻿using System;
using NUnit.Framework;
using Moq;
using FluentAssertions;
using WordOfTheDay.ViewModels;
using WordOfTheDay.Interfaces;
using WordOfTheDayTests.Helpers;
using WordOfTheDay.Models;
using Xamarin.Forms;
using WordOfTheDay.Structures;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using WordOfTheDay.Helpers;

namespace WordOfTheDayTests.ViewModels
{
	[TestFixture]
	public class HomeViewModel_Unit_Tests
	{
		[Test]
		public void Structure_IsGenericBaseViewModel() 
		{
			var vm = new HomeViewModel (null);
			Assert.IsInstanceOf (typeof(BaseViewModel<Word>), vm);
		}

		[Test]
		public void Structure_IsISubscriber() 
		{
			var vm = new HomeViewModel (null);
			Assert.IsInstanceOf (typeof(ISubscriber), vm);
		}

		[Test]
		public void ShowLabels_Default() 
		{
			var vm = new HomeViewModel (null);
			vm.ShowLabels.Should ().BeFalse ();
		}

		[Test]
		public void Padding_Default() 
		{
			var vm = new HomeViewModel (null);
			vm.Padding.Should ().Be(default(Thickness));
		}

		[Test]
		public void OpenCommand_Default() 
		{
			var vm = new HomeViewModel (null);
			vm.OpenCommand.Should ().NotBeNull ();
		}

		[Test]
		public void DataSource_Default()
		{
			var vm = new HomeViewModel (null);

			vm.DataSource.Date.Should ().Be (default(DateTime));
			vm.DataSource.EnglishWord.Should ().BeEmpty ();
            vm.DataSource.EnglishExample.Should().BeEmpty();
            vm.DataSource.TodaysWord.Should().BeEmpty();
            vm.DataSource.TodaysExample.Should().BeEmpty();
		}

		[Test]
		public void ShowLabels_Notifies() 
		{
			var vm = new HomeViewModel (null);
			vm.MonitorEvents ();
			vm.ShowLabels = true;
			vm.ShouldRaisePropertyChangeFor (x => x.ShowLabels);
		}

		[Test]
		public void ShowLabels_CanChange() 
		{
			var vm = new HomeViewModel (null);
			vm.MonitorEvents ();
			vm.ShowLabels = true;
			vm.ShowLabels.Should ().BeTrue ();
		}

		[Test]
		public void Padding_Notifies() 
		{
			var vm = new HomeViewModel (null);
			vm.MonitorEvents ();
			vm.Padding = new Thickness(10);
			vm.ShouldRaisePropertyChangeFor (x => x.Padding);
		}

		[Test]
		public void Padding_CanChange() 
		{
			var vm = new HomeViewModel (null);
			vm.MonitorEvents ();
			vm.Padding = new Thickness(10);
			vm.Padding.Should ().Be (new Thickness (10));
		}

		[Test]
		public void Unsubscribe_SetsDefaults() 
		{
			var vm = new HomeViewModel (null);
			vm.ShowLabels = true;
			vm.IsBusy = true;

			vm.Unsubscribe ();

			vm.IsBusy.Should ().BeFalse ();
			vm.ShowLabels.Should ().BeFalse ();
		}
			
	}

	[TestFixture]
	public class HomeViewModel_Integration_Tests
	{
		[SetUp]
		public void Setup()
		{
			Cleanup.CleanCache ();
            Generate.ConfigureFeedService();
		}

		[TearDown]
		public void Teardown()
		{
			Cleanup.CleanCache ();
			FeedService.TestHTML = null;
		}

		[Test]
		public void SelectedLanguage_ShouldRaiseChanged()
		{
			var vm = new HomeViewModel (null);
			vm.MonitorEvents ();
			vm.SelectedLanguage = "Chinese";
			vm.ShouldRaisePropertyChangeFor (m => m.SelectedLanguage);
		}

		[Test]
		public async Task Loading_ShouldRaisePadding()
		{
            Cleanup.CleanCache();

			var app = Generate.GetApp ();
			var vm = new HomeViewModel (app);
			vm.MonitorEvents ();

			await vm.Loading (app.MainPage);

			vm.ShouldRaisePropertyChangeFor(x => x.Padding);
		}

		[Test]
		public async Task Loading_ShouldRaiseDataSource()
		{
            Cleanup.CleanCache();

			var app = Generate.GetApp ();
			var vm = new HomeViewModel (app);
			vm.MonitorEvents ();

			await vm.Loading (app.MainPage);
			vm.ShouldRaisePropertyChangeFor(x => x.DataSource);
		}

		[Test]
		public async Task Loading_DataSourceShouldNotBeDefault()
		{
            Cleanup.CleanCache();

			var app = Generate.GetApp ();
			var vm = new HomeViewModel (app);

			await vm.Loading (app.MainPage);

			vm.DataSource.CompareTo (new Word ()).Should ().NotBe (0);
		}

		[Test]
		public async Task Loading_ShouldSetNewWord()
		{
            Cleanup.CleanCache();

			var app = Generate.GetApp ();
			var vm = new HomeViewModel (app);
			var expected = Generate.HtmlWord ();

			await vm.Loading (app.MainPage);

			vm.DataSource.CompareTo (expected).Should().Be(0);
		}

		[Test]
		public async Task Loading_ShouldResetBools()
		{
            Cleanup.CleanCache();

			var app = Generate.GetApp ();
			var vm = new HomeViewModel (app);

			await vm.Loading (app.MainPage);

			vm.IsBusy.Should ().BeFalse ();
			vm.ShowLabels.Should ().BeTrue ();
		}

		[Test]
		public void Languages_ShouldHaveValues()
		{
			var vm = new HomeViewModel (null);

			var languages = new String[] {
				"Arabic",
				"Balinese",
				"Balinese For Indonesian",
				"Chinese",
				"Dari",
				"Dutch",
				"English For Portuguese",
				"English For Spanish",
				"Esperanto",
				"French",
				"German",
				"Hebrew",
				"Hindi",
				"Indonesian",
				"Irish",
				"Italian",
				"Japanese",
				"Korean",
				"Latin",
				"Norwegian",
				"Pashto",
				"Polish",
				"Portuguese",
				"Russian",
				"Spanish",
				"Swedish",
				"Turkish",
				"Urdu"
			};

			var vmLangs = vm.Languages;

			foreach (var lang in vmLangs) {
				languages.Contains (lang).Should().BeTrue();
			}
		}

		[Test]
		public void Language_SavesAfterChange()
		{
			Cleanup.CleanCache ();
			var vm = new HomeViewModel (null);

			vm.SelectedLanguage = "German";
			vm.LastLanguage.Should ().Be (Language.German);
		}

		[Test]
		public void Language_DefaultIsItalian()
		{
			var vm = new HomeViewModel (null);
			vm.SelectedLanguage.Should ().Be ("Italian");
		}
	}
}

