using System;
using System.Collections.Generic;

using NUnit.Allure.Steps;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

using QaAutoTests.Extensions;
using QaAutoTests.DataObjects;
using QaAutoTests.Dictionaries;

namespace QaAutoTests.Pages
{
	class BillingOrderPage : BasePage
	{
		public BillingOrderPage(IWebDriver driver) : base(driver) { }

		#region Simple methods

		[AllureStep("Fill first name")]
		public BillingOrderPage FillFirstName(string name)
		{
			CustomTestContext.WriteLine($"Fill first name - '{name}'");
			FirstNameInput.SendKeys(name);

			return this;
		}

		[AllureStep("Fill last name")]
		public BillingOrderPage FillLastName(string name)
		{
			CustomTestContext.WriteLine($"Fill last name - '{name}'");
			LastNameInput.SendKeys(name);

			return this;
		}

		[AllureStep("Fill email")]
		public BillingOrderPage FillEmail(string email)
		{
			CustomTestContext.WriteLine($"Fill email - '{email}'");
			EmailInput.SendKeys(email);

			return this;
		}

		[AllureStep("Fill phone")]
		public BillingOrderPage FillPhone(string phone)
		{
			CustomTestContext.WriteLine($"Fill phone - '{phone}'");
			PhoneInput.SendKeys(phone);

			return this;
		}

		[AllureStep("Fill address line 1")]
		public BillingOrderPage FillAddressLine1(string address)
		{
			CustomTestContext.WriteLine($"Fill address line 1 - '{address}'");
			AddressLine1Input.SendKeys(address);

			return this;
		}

		[AllureStep("Fill address line 2")]
		public BillingOrderPage FillAddressLine2(string address)
		{
			CustomTestContext.WriteLine($"Fill address line 2 - '{address}'");
			AddressLine2Input.SendKeys(address);

			return this;
		}

		[AllureStep("Fill city")]
		public BillingOrderPage FillCity(string city)
		{
			CustomTestContext.WriteLine($"Fill city - '{city}'");
			CityInput.SendKeys(city);

			return this;
		}

		[AllureStep("Fill zip code")]
		public BillingOrderPage FillZipCode(string code)
		{
			CustomTestContext.WriteLine($"Fill code - '{code}'");
			ZipCodeInput.SendKeys(code);

			return this;
		}

		[AllureStep("Click state dropdown")]
		public BillingOrderPage ClickStateSelect()
		{
			CustomTestContext.WriteLine("Click state dropdown");
			StateSelect.Click();

			return this;
		}

		[AllureStep("Click state option")]
		public BillingOrderPage ClickStateOption(State state)
		{
			CustomTestContext.WriteLine($"Click state option - '{state}'");
			StateOption = Driver.FindElement(By.XPath(STATE_OPTION.Replace("{code}", state.ToString())));
			StateOption.Click();

			return this;
		}

		[AllureStep("Fill comment")]
		public BillingOrderPage FillComment(string comment)
		{
			CustomTestContext.WriteLine($"Fill comment - '{comment}'");
			CommentInput.SendKeys(comment);

			return this;
		}

		[AllureStep("Click first item radio button")]
		public BillingOrderPage ClickFirstItemRadioButton()
		{
			CustomTestContext.WriteLine("Click first item radio button");
			FirstItemRadioButton.Click();

			return this;
		}

		[AllureStep("Click second item radio button")]
		public BillingOrderPage ClickSecondItemRadioButton()
		{
			CustomTestContext.WriteLine("Click second item radio button");
			SecondItemRadioButton.Click();

			return this;
		}

		[AllureStep("Click third item radio button")]
		public BillingOrderPage ClickThirdItemRadioButton()
		{
			CustomTestContext.WriteLine("Click third item radio button");
			ThirdItemRadioButton.Click();

			return this;
		}

		[AllureStep("Click submit button")]
		public BillingOrderPage ClickSubmitButton()
		{
			CustomTestContext.WriteLine("Click submit button");
			SubmitButton.Click();

			return this;
		}

		#endregion

		#region Complex methods

		[AllureStep("Submit order form")]
		public BillingOrderPage SendOrderForm(BillingOrder order)
		{
			FillFirstName(order.FirstName);
			FillLastName(order.LastName);
			FillEmail(order.Email);
			FillPhone(order.Phone);
			FillAddressLine1(order.AddressLine1);
			FillAddressLine2(order.AddressLine2);
			FillCity(order.City);
			FillZipCode(order.ZipCode);
			ClickStateSelect();
			ClickStateOption(order.State);

			switch (order.ItemNumber)
			{
				case 0:
					break;
				case 1:
					ClickFirstItemRadioButton();
					break;
				case 2:
					ClickSecondItemRadioButton();
					break;
				case 3:
					ClickThirdItemRadioButton();
					break;
				default:
					throw new Exception("Item number must be 1,2 or 3");
			}

			FillComment(order.Comment);
			ClickSubmitButton();

			return this;
		}

		#endregion

		#region Page conditions methods

		[AllureStep("Check success message displayed")]
		public bool IsSuccessMessageDisplayed()
		{
			CustomTestContext.WriteLine("Check success message displayed");

			return Driver.WaitUntilElementIsDisplay(By.XPath(SUCCESS_MESSAGE), TimeSpan.FromSeconds(3));
		}

		[AllureStep("Check validation error messages displayed")]
		public bool IsRequiredFieldsValidationErrorsDisplayed()
		{
			IList<IWebElement> requiredFields = new List<IWebElement>()
			{
				FirstNameInput,
				LastNameInput,
				EmailInput,
				PhoneInput,
				AddressLine1Input,
				CityInput,
				ZipCodeInput,
				FirstItemRadioButton,
				SecondItemRadioButton,
				ThirdItemRadioButton,
				CommentInput
			};

			Driver.WaitUntilElementIsDisplay(By.XPath(VALIDATION_ERRORS));

			foreach (var field in requiredFields)
			{
				field.Scroll();

				if (!field.GetAttribute("class").Contains("wpforms-error"))
				{
					CustomTestContext.WriteLine($"There is no validation message in field - {field.GetAttribute("name")}");
					return false;
				}
			}

			return true;
		}

		#endregion

		#region Elements

		[FindsBy(How = How.Name, Using = "wpforms[fields][0][first]")]
		protected IWebElement FirstNameInput { get; set; }

		[FindsBy(How = How.Name, Using = "wpforms[fields][0][last]")]
		protected IWebElement LastNameInput { get; set; }

		[FindsBy(How = How.Name, Using = "wpforms[fields][1]")]
		protected IWebElement EmailInput { get; set; }

		[FindsBy(How = How.Name, Using = "wpforms[fields][2]")]
		protected IWebElement PhoneInput { get; set; }

		[FindsBy(How = How.Name, Using = "wpforms[fields][3][address1]")]
		protected IWebElement AddressLine1Input { get; set; }

		[FindsBy(How = How.Name, Using = "wpforms[fields][3][address2]")]
		protected IWebElement AddressLine2Input { get; set; }

		[FindsBy(How = How.Name, Using = "wpforms[fields][3][city]")]
		protected IWebElement CityInput { get; set; }

		[FindsBy(How = How.Name, Using = "wpforms[fields][3][postal]")]
		protected IWebElement ZipCodeInput { get; set; }

		[FindsBy(How = How.Name, Using = "wpforms[fields][3][state]")]
		protected IWebElement StateSelect { get; set; }

		protected IWebElement StateOption { get; set; }

		[FindsBy(How = How.Name, Using = "wpforms[fields][6]")]
		protected IWebElement CommentInput { get; set; }

		[FindsBy(How = How.XPath, Using = FIRST_ITEM)]
		protected IWebElement FirstItemRadioButton { get; set; }

		[FindsBy(How = How.XPath, Using = SECOND_ITEM)]
		protected IWebElement SecondItemRadioButton { get; set; }

		[FindsBy(How = How.XPath, Using = THIRD_ITEM)]
		protected IWebElement ThirdItemRadioButton { get; set; }

		[FindsBy(How = How.Name, Using = "wpforms[submit]")]
		protected IWebElement SubmitButton { get; set; }

		#endregion

		#region Locators

		private const string STATE_OPTION = "//option[@value='{code}']";
		private const string FIRST_ITEM = "//input[@data-amount=\"10.00\"]";
		private const string SECOND_ITEM = "//input[@data-amount=\"20.00\"]";
		private const string THIRD_ITEM = "//input[@data-amount=\"30.00\"]";

		private const string SUCCESS_MESSAGE = "//p[text()='Thanks for contacting us! We will be in touch with you shortly.']";
		private const string VALIDATION_ERRORS = "//*[contains(@class, 'wpforms-error')]";

		#endregion
	}
}
