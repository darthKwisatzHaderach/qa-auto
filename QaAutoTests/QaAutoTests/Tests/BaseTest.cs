using System;
using System.IO;

using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;

using QaAutoTests.Dictionaries;
using QaAutoTests.Extensions;

namespace QaAutoTests.Tests
{
	[AllureNUnit]
	[TestFixture(Browser.Chrome)]
	[TestFixture(Browser.Firefox)]
	public class BaseTest
	{
		public IWebDriver Driver;
		private Browser _browser;

		public BaseTest(Browser browser)
		{
			_browser = browser;
		}

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			DriverOptions options = initOptions(_browser);
			Driver = new RemoteWebDriver(options);
			Driver.Manage().Window.Maximize();
		}

		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			Driver.Dispose();
		}

		[TearDown]
		public void TearDown()
		{
			try
			{
				Driver.TakeScreenshot(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FailedTests"));
			}
			catch (Exception ex)
			{
				TestContext.WriteLine("Ошибка при снятии скриншота {0}", ex.ToString());
			}
		}

		private DriverOptions initOptions(Browser browser)
		{
			switch (browser)
			{
				case Browser.Chrome:
					return new ChromeOptions();
				case Browser.Firefox:
					return new FirefoxOptions();
				case Browser.IE:
					return new InternetExplorerOptions();
				case Browser.Edge:
					return new EdgeOptions();
				case Browser.Opera:
					return new OperaOptions();
				case Browser.Safari:
					return new SafariOptions();
				default:
					return new ChromeOptions();
			}
		}
	}
}
