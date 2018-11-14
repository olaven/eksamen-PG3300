using System;

namespace Market
{
    public abstract class ItemDecorator : Item
    {
        private readonly Item _item;
        protected readonly PriceAdjuster priceAdjuster; 
        
        public string Name { get; set; }
        public double Price { get; set; }
        public Person Owner { get; set; }

        protected ItemDecorator(Item original)
        {
            _item = original; 
            priceAdjuster = new PriceAdjuster();
        }

        public virtual string getCondition()
        {
            return _item.getCondition(); 
        }

        public virtual string getDamage()
        {
            return _item.getDamage(); 
        }

        public virtual string getModification()
        {
            return _item.getModification(); 
        }

        public string getInformation()
        {
            return "" +
                   "\nName: " + Name +
                   "\nPrice: " + Price +
                   "\nOwner: " + Owner + //TODO: Fix en fin toString på person 
                   "\n\n" +
                   
                   "\nCondition: " + getCondition() +
                   "\nDamage: " + getDamage() +
                   "\nModification: " + getModification() +
                   "\n\n"
                ; 
        }
    }
}