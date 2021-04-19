using System;
using System.Text.Json.Serialization;

using DataStorage;

public enum Currency
{
    dollar = 28,
    euro = 33,
    uan = 1
}
namespace Lab01.Entities
{
    public class Transaction : IStorable
    {
        public Guid Guid { get; }
        public decimal Sum { get; set; }
        public string Currency { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public Transaction(decimal sum, string currency, Category category, DateTime date, string description)
        {
            Guid = Guid.NewGuid();
            Sum = sum;
            Currency = currency;
            Category = category;
            Description = description;
            Date = date;
        }

        [JsonConstructor]
        public Transaction(Guid guid, decimal sum, string currency, Category category, string description, DateTime date)
        {
            Guid = guid;
            Sum = sum;
            Currency = currency;
            Category = category;
            Description = description;
            Date = date;
        }

        public Transaction(string currency)
        {
            Guid = Guid.NewGuid();
            Currency = currency;
            Date = DateTime.Now;
        }

        /*
        public override string ToString()
        {
            return $"Category: {Category}\n Sum: {Sum}, Currency: {Currency}\nDate: {Date}";
        }
        */
    }
}