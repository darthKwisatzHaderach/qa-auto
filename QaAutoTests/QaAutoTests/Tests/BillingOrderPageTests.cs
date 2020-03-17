using System.Collections;

using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Framework;

using QaAutoTests.DataObjects;
using QaAutoTests.Dictionaries;
using QaAutoTests.Pages;

namespace QaAutoTests.Tests
{
	[AllureSuite("Billing order tests")]
	[Parallelizable(ParallelScope.Fixtures)]
	public class BillingOrderPageTests : BaseTest
	{
		public BillingOrderPageTests(Browser browser, string version) : base(browser, version) {}

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

		[AllureSeverity(SeverityLevel.normal)]
		[Test]
		public void RequiredFieldsTest()
		{
			var billingOrderPage = new BillingOrderPage(Driver);

			billingOrderPage.ClickSubmitButton();

			Assert.IsTrue(billingOrderPage.IsRequiredFieldsValidationErrorsDisplayed());
		}

		[AllureSeverity(SeverityLevel.critical)]
		[Test]
		public void TotalAmountTest()
		{
			var billingOrderPage = new BillingOrderPage(Driver);
			var firstItemAmount = "$ 10.00";
			var secondItemAmount = "$ 20.00";
			var thirdItemAmount = "$ 30.00";

			billingOrderPage.ClickFirstItemRadioButton();

			var result1 = billingOrderPage.IsExpectedTotalAmountDisplayed(firstItemAmount);

			billingOrderPage.ClickSecondItemRadioButton();

			var result2 = billingOrderPage.IsExpectedTotalAmountDisplayed(secondItemAmount);

			billingOrderPage.ClickThirdItemRadioButton();

			var result3 = billingOrderPage.IsExpectedTotalAmountDisplayed(thirdItemAmount);

			Assert.Multiple(() =>
			{
				Assert.IsTrue(result1, "Incorrect total amount for first item");
				Assert.IsTrue(result2, "Incorrect total amount for second item");
				Assert.IsTrue(result3, "Incorrect total amount for third item");
			});
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
