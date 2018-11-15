using System; 
using FleaMarket; 

namespace Item
{
    public class ConcreteItem : IItem
    {
        public string Name { get; set; }
        public Person Owner { get; set; }

        private readonly double _price; 
        

        

        public ConcreteItem(string name, double price, Person owner)
        {
            Name = name;
            Owner = owner;

            this._price = price; 
        }
        
        public double getPrice()
        {
            return _price;
        }
        
        public string getCondition()
        {
            return "not specified"; 
        }

        public string getDamage()
        {
            return "not specified"; 
        }

        public string getModification()
        {
            return "not specified"; 
        }

        public string getInformation()
        {
            return "" +
                   "\nName: " + Name +
                   "\nPrice: " + getPrice() +
                   "\nOwner: " + Owner + //TODO: Fix en fin toString på person 
                   "\n\n"
                ; 
        }

    }
}