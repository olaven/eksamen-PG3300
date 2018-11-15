using System;
using System.Linq;
using FleaMarket;
using Item;


namespace FleaMarket
{
    public class Salesman : Person
    {
    
        public Salesman(string name) : base(name)
        {
        
        }
        public override void Act()
        {
            if(Items.Count < 1)
                return;
            IItem itemForSale = Items.ToArray()[0];
            Items.Remove(itemForSale);
            Market.Instance.AddItem(itemForSale);
            Console.WriteLine("{0} put up their item for sale: {1}", Name, itemForSale.getInformation());
        }
    }

}

