using NUnit.Framework;
using QaAutoTests.Dictionaries;
using QaAutoTests.Extensions;

namespace QaAutoTests.DataObjects
{
	public class BillingOrder
	{
		public string FirstName;
		public string LastName;
		public string Email;
		public string Phone;
		public string City;
		public string ZipCode;
		public State State;
		public string AddressLine1;
		public string AddressLine2;
		public int ItemNumber;
		public string Comment;

		public BillingOrder(
			string firstName = null,
			string lastName = null,
			string email = null,
			string phone = null,
			string city = null,
			string zipCode = null,
			State state = State.AK,
			string addressLine1 = null,
			string addressLine2 = null,
			int itemNumber = 1,
			string comment = null)
		{
			FirstName = string.IsNullOrEmpty(firstName) ? TestContext.CurrentContext.Random.GetString(6, "abcdefghijklmnopqrstuvwxyz ").FirstCharToUpper() : firstName;
			LastName = string.IsNullOrEmpty(lastName) ? TestContext.CurrentContext.Random.GetString(10, "abcdefghijklmnopqrstuvwxyz ").FirstCharToUpper() : lastName;
			Email = string.IsNullOrEmpty(email) ? TestContext.CurrentContext.Random.GetString(10, "abcdefghijklmnopqrstuvwxyz1234567890") + "@gmail.com" : email;
			Phone = string.IsNullOrEmpty(phone) ? TestContext.CurrentContext.Random.GetString(10, "1234567890") : phone;
			City = string.IsNullOrEmpty(city) ? TestContext.CurrentContext.Random.GetString(10, "abcdefghijklmnopqrstuvwxyz1234567890").FirstCharToUpper() : city;
			ZipCode = string.IsNullOrEmpty(zipCode) ? TestContext.CurrentContext.Random.GetString(5, "1234567890") : zipCode;
			State = state;
			AddressLine1 = string.IsNullOrEmpty(addressLine1) ? TestContext.CurrentContext.Random.GetString(6, "abcdefghijklmnopqrstuvwxyz").FirstCharToUpper() : addressLine1;
			AddressLine2 = string.IsNullOrEmpty(addressLine2) ? TestContext.CurrentContext.Random.GetString(6, "abcdefghijklmnopqrstuvwxyz").FirstCharToUpper() : addressLine2;
			ItemNumber = itemNumber;
			Comment = string.IsNullOrEmpty(comment) ? TestContext.CurrentContext.Random.GetString(100, "abcde fghijklm nopqrstu vwxyz 12345 67890").FirstCharToUpper() : comment;
		}
	};
}