using System;
namespace Lab01.Entities
{
    public class Category
    {
        private int _id;            
        private string _name;
        private string? _description;  //optional
        private string _color; 
        private string _icon;

        private static int IdCounter = 0;

        public int Id
        {
            get { return _id; }
            private set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string? Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public Category(string name, string description, string color, string icon)
        {
            this._id = IdCounter;
            this._name = name;
            this._description = description;
            this._color = color;
            this._icon = icon;

            IdCounter += 1;
        }

        public Category(string name, string color, string icon)
        {
            this._id = IdCounter;
            this._name = name;
            this._color = color;
            this._icon = icon;

            IdCounter += 1;
        }

        public bool Validate()
        {
            var result = true;

            if (Id < 0)
                result = false;
            if (String.IsNullOrWhiteSpace(Name))
                result = false;

            return result;
        }
    }
}
