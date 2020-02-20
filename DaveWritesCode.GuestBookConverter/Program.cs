using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using DaveWritesCode.GuestBookConverter.Classes;

namespace DaveWritesCode.GuestBookConverter
{
    class Program
    {
        static string importLocation;
        static string exportLocation;
        static IList<TheKnotCSV> Imports = new List<TheKnotCSV>();
        static IEnumerable<MintedCSV> Exports;
        static void Main(string[] args)
        {
            Console.WriteLine("This is a handy utility for exporting a CSV formatted for Minted.com from a CSV formatted by the Knot.");
            Console.WriteLine("Where is your CSV located?");

            do
            {
                importLocation = Console.ReadLine();
            } while (!importFileExists());

            ImportCSV();
            ConvertCSV();

            Console.WriteLine("Where would you like your Minted CSV Exported?");
            exportLocation = Console.ReadLine() + "MintedCSV.csv";

            ExportCSV();

            Console.WriteLine("Operation completed.");
            Console.ReadLine();
        }

        private static void ExportCSV()
        {
            using (var writer = new StreamWriter(exportLocation))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                try
                {
                    csv.WriteRecords(Exports);
                    if (File.Exists(exportLocation))
                    Console.WriteLine("Export Succeeded.");
                    else throw new Exception();
                }
                catch
                {
                    Console.WriteLine("Export Failed.");
                }
            }
        }

        private static void ConvertCSV()
        {
            Exports = Imports
                        .Where(e => e.WeddingInvited == "Not Sent" && e.WeddingRSVP == "No Response")
                        .GroupBy(e => e.HouseholdFullName)
                        .Select(e => new MintedCSV(e));
            Console.WriteLine("Conversion Succeeded.");
        }

        private static void ImportCSV()
        {
            using (var reader = new StreamReader(importLocation))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                try
                {
                    var imports = csv.GetRecords<TheKnotCSV>();
                    foreach (var import in imports)
                        Imports.Add(import);
                    Console.WriteLine("Import Successeded.");
                }
                catch
                {
                    Console.WriteLine("Import Failed.");
                }
            }
        }

        static bool importFileExists()
        {
            var fileExists = File.Exists(importLocation);
            if (!fileExists) Console.WriteLine("Where is your CSV really located?");
            return fileExists;
        }
    }
}
