using System;
using NUnit.Framework;
using FluentAssertions;
using WordOfTheDay.ViewModels;
using Moq;
using Xamarin.Forms;
using WordOfTheDay.Interfaces;
using System.ComponentModel;
using WordOfTheDayTests.Helpers;

namespace WordOfTheDayTests.ViewModels
{
	class TestBase : BaseViewModel {
		public TestBase (IApplication app) : base(app)
		{

		}
	}
	class GenericTestBase : BaseViewModel<string>
	{
		public GenericTestBase (IApplication app) : base(app)
		{
		}
	}

	[TestFixture]
	public class BaseViewModel_Integration_Tests
	{
		[Test]
		public void ShouldHaveSaneDefaultsForPage() 
		{
			var vm = new TestBase (Generate.GetApp());

			vm.IsBusy.Should ().BeFalse ();
			vm.Navigation.Should ().NotBeNull ();
		}
	}

	[TestFixture]
	public class BaseViewModel_Unit_Tests
	{
		[Test]
		public void Structure_IsINotifyPropertyChanged ()
		{
			var vm = new TestBase (null);
			Assert.IsInstanceOf (typeof(INotifyPropertyChanged), vm);
		}
			
		[Test]
		public void IsBusy_CanChange() 
		{
			var vm = new TestBase (null);

			vm.IsBusy = true;
			vm.IsBusy.Should ().BeTrue ();
		}

		[Test]
		public void IsBusy_Notified() 
		{
			var vm = new TestBase (null);
			vm.MonitorEvents ();
			vm.IsBusy = true;
			vm.ShouldRaisePropertyChangeFor (x => x.IsBusy);
		}
	}

	[TestFixture]
	public class GenericBaseViewModel_Unit_Tests
	{
		[Test]
		public void Structure_IsBaseViewModel()
		{
			var vm = new GenericTestBase (null);
			Assert.IsInstanceOf (typeof(BaseViewModel), vm);
		}

		[Test]
		public void Structure_IsGenericBaseType() 
		{
			var vm = new GenericTestBase (null);
			Assert.IsInstanceOf (typeof(BaseViewModel<String>), vm);
		}

		[Test]
		public void DataSource_CanChange() 
		{
			var vm = new GenericTestBase (null);

			vm.MonitorEvents ();
			vm.DataSource = "My new datasource";
			vm.DataSource.Should ().Be ("My new datasource");
		}

		[Test]
		public void DataSource_Notified() 
		{
			var vm = new GenericTestBase (null);

			vm.MonitorEvents ();
			vm.DataSource = "My new datasource";
			vm.ShouldRaisePropertyChangeFor (x => x.DataSource);
		}
	}
}

