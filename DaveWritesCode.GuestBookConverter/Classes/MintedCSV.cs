using System;
using System.Linq;
using CsvHelper.Configuration.Attributes;

namespace DaveWritesCode.GuestBookConverter.Classes
{
    public class MintedCSV
    {
        public MintedCSV(IGrouping<string, TheKnotCSV> household)
        {
            var hoh = household.FirstOrDefault();
            var partySize = household.Count();

            if (partySize > 2)
            {
                NameOnEnvelope = $"{hoh.HouseholdLeaderLastName} Household";
            }
            else if (partySize == 2)
            {
                if (household.Any(e => e.FullName.Contains("Plus One") || e.FullName.Contains("1")))
                {
                    NameOnEnvelope = $"{household.FirstOrDefault(e => !e.FullName.Contains("Plus One") && !e.FullName.Contains("1")).FullName} and Guest";
                }
                else if (household.All(e => e.LastName == hoh.LastName))
                {
                    NameOnEnvelope = $"{String.Join(" and ", household.Select(e => e.FirstName))} {hoh.LastName}";
                }
                else
                {
                    NameOnEnvelope = $"{String.Join(" and ", household.Select(e => e.FullName))}";
                }
            }
            else
            {
                NameOnEnvelope = String.Join(", ", household.ToList().Select(e => e.FullName));
            }

            StreetAddress1 = hoh.StreetAddress1;
            StreetAddress2 = hoh.StreetAddress2;
            City = hoh.City;
            State = hoh.State;
            ZipCode = hoh.ZipCode;
            Country = hoh.Country;
            Email = hoh.Email;
            Phone = hoh.Phone;
        }

        [Name("Name on Envelope")]
        public string NameOnEnvelope { get; set; }
        [Name("Street Address 1")]
        public string StreetAddress1 { get; set; }
        [Name("Street Address 2 (Optional)")]
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        [Name("State/Region")]
        public string State { get; set; }
        [Name("Zip/Postal Code")]
        public string ZipCode { get; set; }
        public string Country { get; set; }
        [Name("Email (Optional)")]
        public string Email { get; set; }
        [Name("Phone (Optional)")]
        public string Phone { get; set; }
    }
}