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
			FirstName = firstName ?? TestContext.CurrentContext.Random.GetString(6, "abcdefghijklmnopqrstuvwxyz ").FirstCharToUpper();
			LastName = lastName ?? TestContext.CurrentContext.Random.GetString(10, "abcdefghijklmnopqrstuvwxyz ").FirstCharToUpper();
			Email = email ?? "fake-email@gmail.com";
			Phone = phone ?? TestContext.CurrentContext.Random.GetString(10, "1234567890");
			City = city ?? TestContext.CurrentContext.Random.GetString(10, "abcdefghijklmnopqrstuvwxyz1234567890").FirstCharToUpper();
			ZipCode = zipCode ?? TestContext.CurrentContext.Random.GetString(5, "1234567890");
			State = state;
			AddressLine1 = addressLine1 ?? TestContext.CurrentContext.Random.GetString(6, "abcdefghijklmnopqrstuvwxyz").FirstCharToUpper();
			AddressLine2 = addressLine2 ?? TestContext.CurrentContext.Random.GetString(6, "abcdefghijklmnopqrstuvwxyz").FirstCharToUpper();
			ItemNumber = itemNumber;
			Comment = comment ?? TestContext.CurrentContext.Random.GetString(100, "abcde fghijklm nopqrstu vwxyz 12345 67890").FirstCharToUpper();
		}
	};
}