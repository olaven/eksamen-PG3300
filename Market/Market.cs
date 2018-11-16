using System;
using System.Collections.Generic;
using System.Threading;
using Item;

namespace FleaMarket
{
    public class Market
    {
        private static Market _market;
        private List<IItem> _items;
        public EventHandler EventHappening;
        private readonly object _staticLock = new object(); 
        private static readonly object Padlock = new object();


        private Market()
        {
            //private to prohibit instantiation of market object for other classes.
            _items = new List<IItem>();

        }

        public static Market Instance
        {
            get
            {
                lock (Padlock)
                {
                    if (_market == null)
                    {
                        _market = new Market();
                    }

                    return _market;
                }
            }
        }

        public List<IItem> GetItems()
        {
            return _items;    
        }

        public void AddItem(IItem item)
        {
            _items.Add(item);
            ItemForSaleEvent(new ItemForSaleEventArgs(item));
        }

        protected void ItemForSaleEvent(EventArgs e)
        {
            EventHandler handler = EventHappening;
            if (handler != null)
            {
                //gi beskjed til customers at de kan kjøpe
                handler(this, e);
            }
        }

        //TODO: cleanup & refactor?
        public void BuyItem(Customer customer, IItem item)
        {

            lock (_staticLock)
            {
                if (_items.Count != 0 && _items.Contains(item))
                {
                    var customerBalance = customer.Wallet.Balance;
                    var seller = (Salesman) item.Owner;
                    var itemPrice = item.getPrice();
                    
                    if (customer.Wallet >= itemPrice)
                    {
                        DoTransaction(customer, item, itemPrice, seller);
                    }
                    else
                    {
                        // Console.WriteLine("{0} tries to haggle, offers {1} for item with price {2}", customer.Name, customerBalance, itemPrice);
                        //customer asks for bargain
                        if (seller.Bargain(itemPrice, customerBalance))
                        {
                            DoTransaction(customer, item, itemPrice, seller);
                        }
                        else
                        {
                            //Console.WriteLine("Haggle declined");
                        }
                        
                    }
              
                }
                else
                {
                   // Console.WriteLine(customer.Name + " attempted to buy but was too slow");
                }      
            }
        }

        private void DoTransaction(Customer customer, IItem item, float itemPrice, Salesman seller)
        {
            _items.Remove(item);
            customer.Wallet.Balance -= itemPrice;
            seller.Wallet.Balance += itemPrice;
            
            item.Owner = customer;

            
            seller.GetItems().Remove(item);
            customer.GetItems().Add(item);

            Console.WriteLine("{0, 50} bought {1}", customer.Name, item.getInformation());
        }
    }
}