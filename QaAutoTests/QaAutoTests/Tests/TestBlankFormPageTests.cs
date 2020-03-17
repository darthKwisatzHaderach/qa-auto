using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Framework;

using QaAutoTests.Dictionaries;
using QaAutoTests.Pages;

namespace QaAutoTests.Tests
{
	[AllureSuite("Test blank form page tests")]
	[Parallelizable(ParallelScope.Fixtures)]
	class TestBlankFormPageTests : BaseTest
	{
		public TestBlankFormPageTests(Browser browser, string version) : base(browser, version) { }

		[SetUp]
		public void SetUp()
		{
			var authorizationPage = new AuthorizationPage(Driver);

			authorizationPage
				.GoToPage("http://qaauto.co.nz/test-blank-form/")
				.LogIn("Testing");
		}

		[AllureIssue("2000")]
		[AllureSeverity(SeverityLevel.critical)]
		[TestCase("John", "Doe", "email@gmail.com", "Comment")]
		[TestCase("John", "Doe", "email@gmail.com", "ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTU+")]
		[TestCase("John", "Doe", "email@gmail.com", "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789+")]
		[TestCase("John", "Doe", "email@gmail.com", "~!@#$%^&*()_+{}|:\" <>?`-=[];',./~!@#$%^&*()_+{}|:\"<>?`-=[];',./ ~!@#$%^&*()_+{}|:\"<>?`-=[];',./~!@#$%X")]
		public void SubmitFormWithAllParametersTest(string firstName, string lastName, string email, string comment)
		{
			var testBlankFormPage = new TestBlankFormPage(Driver);

			testBlankFormPage.SendForm(firstName: firstName, lastName: lastName, email: email, comment: comment);

			Assert.IsTrue(testBlankFormPage.IsSuccessMessageDisplayed());
		}
	}
}
