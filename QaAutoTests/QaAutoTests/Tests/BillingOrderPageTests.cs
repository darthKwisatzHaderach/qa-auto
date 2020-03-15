using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Framework;

using QaAutoTests.DataObjects;
using QaAutoTests.Dictionaries;
using QaAutoTests.Pages;
using System.Collections;

namespace QaAutoTests.Tests
{
	[AllureSuite("Billing order tests")]
	[Parallelizable(ParallelScope.Fixtures)]
	public class BillingOrderPageTests : BaseTest
	{
		public BillingOrderPageTests(Browser browser) : base(browser) {}

		[SetUp]
		public void SetUp()
		{
			var authorizationPage = new AuthorizationPage(Driver);

			authorizationPage
				.GoToPage("http://qaauto.co.nz/billing-order-form/")
				.LogIn("Testing");
		}

		[AllureTms("TestCaseSource-Attribute")]
		[AllureSeverity(SeverityLevel.blocker)]
		[TestCaseSource("TestCases")]
		public bool SubmitFormWithAllParametersTest(BillingOrder order)
		{
			var billingOrderPage = new BillingOrderPage(Driver);

			billingOrderPage.SendOrderForm(order);

			return billingOrderPage.IsSuccessMessageDisplayed();
		}

		public static IEnumerable TestCases
		{
			get
			{
				yield return new TestCaseData(new BillingOrder() { LastName = "Smith" }).SetName("Simple last name").Returns(true);
				yield return new TestCaseData(new BillingOrder() { LastName = "O'Brien" }).SetName("Last name with apostrophe").Returns(true);
				yield return new TestCaseData(new BillingOrder() { LastName = "Smith-Klein" }).SetName("Last name with hypen").Returns(true);
				yield return new TestCaseData(new BillingOrder() { LastName = "Li" }).SetName("Short last name").Returns(true);
				yield return new TestCaseData(new BillingOrder() { LastName = "" }).SetName("Empty last name").Returns(false);
			}
		}
	}
}
