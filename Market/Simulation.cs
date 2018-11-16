using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Item;

namespace FleaMarket
{
    public class Simulation
    {
        private readonly Random _random; 
        private readonly int _saleCount;
        
        private readonly List<Salesman> _salesmen; 
        private readonly List<Customer> _customers;  
        

        public Simulation(int saleCount)
        {
            _saleCount = saleCount;
            _random = new Random();

            _salesmen = PopulateSalesmen();
            _customers = PopulateCustomers(); 
            
        }

        public void Run()
        {
            
            new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(2000);
                    GetRandomSeller().Act(); // may be several sellers / composite 
                }
            }).Start();
        }

        private Salesman GetRandomSeller()
        {
            var amount = _random.Next(1, 3);

            int index = _random.Next(_salesmen.Count); 
            CompositeSalesman salesman = new CompositeSalesman( _salesmen[index]);

            // from 1, as first one is already added through constructor above. 
            for (var i = 1; i < amount; i++)
            {
                Salesman randomSalesman = _salesmen[_random.Next(_salesmen.Count)]; 
                salesman.Add(randomSalesman); 
            }

            return salesman; 
        }


        private void GiveItemsTo(IEnumerable<Person> persons)
        {
            foreach (var person in persons)
            {
                IItem item = ItemFactory.GetRandomItem(person, 5); 
                person.GetItems().Add(item);
            }
        }
        
        
        #region populating lists 
        private List<Salesman> PopulateSalesmen()
        {
            List<Person> persons = PopulatePersons(PersonType.Salesman, 5, 10);
            GiveItemsTo(persons); 
            
            return persons.Cast<Salesman>().ToList();
        }
        

        private List<Customer> PopulateCustomers()
        {
            List<Person> persons = PopulatePersons(PersonType.Customer, 10, 15);
            return persons.Cast<Customer>().ToList(); 
        }
        

        private List<Person> PopulatePersons(PersonType type, int min, int max)
        {
            PersonFactory _personFactory = new PersonFactory();
            
            var _amount = _random.Next(min, max);
            var _list = new List<Person>(); 

            for (var i = 0; i < _amount; i++)
            {
                Person person = _personFactory.getPerson(type); 
                _list.Add(person);
            }

            return _list;
        }
        #endregion

    }
}