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
	[TestFixture(Browser.Chrome, "80.0")]
	[TestFixture(Browser.Firefox, "80.0")]
	public class BaseTest
	{
		public IWebDriver Driver;
		private Browser _browser;
		private string _version;

		public BaseTest(Browser browser, string version)
		{
			_browser = browser;
			_version = version;
		}

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			Environment.CurrentDirectory = Path.GetDirectoryName(GetType().Assembly.Location);

			DriverOptions options = initOptions(_browser, _version);
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

		private DriverOptions initOptions(Browser browser, string version)
		{
			DriverOptions options;

			switch (browser)
			{
				case Browser.Chrome:
					options = new ChromeOptions();
					break;
				case Browser.Firefox:
					options = new FirefoxOptions();
					break;
				case Browser.IE:
					options = new InternetExplorerOptions();
					break;
				case Browser.Edge:
					options = new EdgeOptions();
					break;
				case Browser.Opera:
					options = new OperaOptions();
					break;
				case Browser.Safari:
					options = new SafariOptions();
					break;
				default:
					options = new ChromeOptions();
					break;
			}

			options.BrowserVersion = version;

			return options;
		}
	}
}
