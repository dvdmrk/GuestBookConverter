using CsvHelper.Configuration.Attributes;

namespace DaveWritesCode.GuestBookConverter.Classes
{
    public class TheKnotCSV
    {
        [Name("First Name")]
        public string FirstName { get; set; }
        [Name("Last Name")]
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        [Name("Household Leader First Name")]
        public string HouseholdLeaderFristName { get; set; }
        [Name("Household Leader Last Name")]
        public string HouseholdLeaderLastName { get; set; }
        public string HouseholdFullName => $"{HouseholdLeaderFristName} {HouseholdLeaderLastName}";
        public string Phone { get; set; }
        public string Email { get; set; }
        [Name("Street Address 1")]
        public string StreetAddress1 { get; set; }
        [Name("Street Address 2")]
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        [Name("Zip/Postal Code")]
        public string ZipCode { get; set; }
        [Name("State/Province")]
        public string State { get; set; }
        public string Country { get; set; }
        [Name("Wedding - Invited")]
        public string WeddingInvited { get; set; }
        [Name("Wedding - RSVP")]
        public string WeddingRSVP { get; set; }
    }
}