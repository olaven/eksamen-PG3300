using System;
using System.Threading;
using Item;

namespace FleaMarket
{
    public class Customer : Person
    {
        public Customer(string name) : base(name)
        {
            Market.Instance.EventHappening += (sender, eventArgs) => { 
                AttemptBuy(eventArgs);
            };
        }

        public void AttemptBuy(EventArgs e)
        {
            new Thread(() =>
            {
                Thread.Sleep(new Random().Next(50, 100));
                ItemForSaleEventArgs args = (ItemForSaleEventArgs) e;
                Market.Instance.BuyItem(this, args.Item);
            }).Start(); 

        }
        
        
        public override void Act()
        {

            /*new Thread(() =>
            {
                Thread.Sleep(new Random().Next(50, 100));
                
                Market.Instance.BuyItem(this);
            }).Start(); */

        }
    }
}

/*

var items = Market.Instance.GetItems();
                if (items.Count != 0)
                {
                    Console.WriteLine(Name + "Customer acting");
                    
                    IItem item = items.ToArray()[0];
                    Market.Instance.GetItems().Remove(item);

                    Console.WriteLine(Name + " bought:");
                    Console.WriteLine(item.getInformation());
                    Console.WriteLine("\n");
                }*/