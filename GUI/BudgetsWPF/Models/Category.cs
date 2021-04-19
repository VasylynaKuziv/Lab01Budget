using System;
namespace Lab01.Entities
{
    public class Category
    {
        //public Guid Guid { get; }
        public string Name { get; set; }
        public string Description { get; set; }

        public static Category DefaultCategory = new("", "");

        public Category(string name, string description)
        {
            //Guid = Guid.NewGuid();
            Name = name;
            Description = description;
        }

        public Category()
        {
        }

        protected bool Equals(Category cat)
        {
            return Name == cat.Name && Description == cat.Description;
        }

    }
}
