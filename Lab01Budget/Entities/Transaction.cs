using System;
using System.Collections.Generic;

public enum Currency
{
    dollar,
    euro,
    uan
}
namespace Lab01.Entities
{
    public class Transaction
    {
        private static int InstanceCount = 0;

        private int _id;
        private double _sum;
        private Currency _currency;
        private Category _category;
        private string? _description;
        private DateTime _date;
        private int _senderId;
        private int _receiverId;


        public int Id
        {
            get { return _id; }
            private set { _id = value; }
        }
        public double Sum
        {
            get { return _sum; }
            set { _sum = value; }
        }
        public Currency Currency
        {
            get { return _currency; }
            set { _currency = value; }
        }
        public Category Category
        {
            get { return _category; }
            set { _category = value; }
        }
        public string? Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public int SenderId
        {
            get { return _senderId; }
            set { _senderId = value; }
        }
        public int ReceiverId
        {
            get { return _receiverId; }
            set { _receiverId = value; }
        }

        public Transaction(double sum, Currency currency, Category category, DateTime date, int senderId, int receiverId, string description = "")
        {
            _id = InstanceCount;
            _sum = sum;
            _currency = currency;
            _category = category;
            _description = description;
            _senderId = senderId;
            _receiverId = receiverId;
            _date = date;

            InstanceCount += 1;
        }
        public Transaction(double sum, Currency currency, Category category, DateTime date, int receiverId, string description = "")
        {
            _id = InstanceCount;
            _sum = sum;
            _currency = currency;
            _category = category;
            _description = description;
            _receiverId = receiverId;
            _date = date;

            InstanceCount += 1;
        }

        public bool Validate()
        {
            var result = true;

            if (Id < 0)
                result = false;
            if (Category == null)
                result = false;
            if (SenderId <= 0)
                result = false;
            if (ReceiverId <= 0)
                result = false;
            if (ReceiverId == SenderId)
                result = false;
            return result;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Category: {Category}\n Sum: {Sum}, Currency: {Currency}\nDate: {Date}";
        }
    }
}