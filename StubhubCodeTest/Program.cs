using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace Viagogo
{
    public class Event
    {
        public string Name { get; set; }
        public string City { get; set; }
        public decimal Price { get; set; }
        public int Distance { get; set; }
    }
    public class Customer
    {
        public string Name { get; set; }
        public string City { get; set; }
    }
    public class Solution
    {
        public static Dictionary<string, int> dictionaryOfSameCity = new Dictionary<string, int>();
        static void Main(string[] args)
        {
            var events = new List<Event>{
                        new Event{ Name = "Phantom of the Opera", City = "New York"},
                        new Event{ Name = "Metallica", City = "Los Angeles"},
                        new Event{ Name = "Metallica", City = "New York"},
                        new Event{ Name = "Metallica", City = "Boston"},
                        new Event{ Name = "LadyGaGa", City = "New York"},
                        new Event{ Name = "LadyGaGa", City = "Boston"},
                        new Event{ Name = "LadyGaGa", City = "Chicago"},
                        new Event{ Name = "LadyGaGa", City = "San Francisco"},
                        new Event{ Name = "LadyGaGa", City = "Washington"}
            };

            // 1. find out all events that are in cities of customer
            // then add to email.
            var customer = new List<Customer> { new Customer { Name = "Mr. Fake", City = "New York" } };
            var query = customer;
            Console.WriteLine("To Send Email to customer with Same city sorted by Price \n");
            // 1.TASK

            foreach (var individualCustomer in query)
            {
                foreach (var ev in events.Where(x => x.City == individualCustomer.City))
                {
                    AddToEmail(individualCustomer, ev);
                }


            }

            Console.WriteLine("\n To Send Email to customer with nearest 5 cities sorted by Price \n");
            foreach (var cus in query)
            {
                var custEvents = events.Select(x => new Event { Distance = GetDistance(cus.City, x.City), Name = x.Name, City = x.City }).ToList();
                foreach (var ev in custEvents.Where(x => x.Distance != -1).OrderBy(x => x.Distance).Take(5))
                {
                    AddToEmail(cus, ev);
                }
            }

//            3.If the GetDistance method is an API call which could fail or is too expensive, how will u
//              improve the code written in 2 ? Write the code.
//              Answer: Caching
            Console.WriteLine("\n Optimized for 3rd question \n");

            var cachedDistances = new Dictionary<string, int>();
            var customerVariable = new Customer();
            var nearestFive = events.Select(ev =>
            {
                if (!cachedDistances.ContainsKey(ev.City))
                {
                    cachedDistances.Add(ev.City, GetDistance(customerVariable.City, ev.City));
                }

                return new
                {
                    Event = ev,
                    Distance = cachedDistances[ev.City]
                };
            }
                )
                .OrderBy(e => e.Distance)
                .Take(5)
                .Select(e => e.Event);
            Console.WriteLine("----");

            foreach (var item in nearestFive)
            {
                AddToEmail(customerVariable, item);
            }

            Console.ReadLine();

            // 5. If we also want to sort the resulting events by other fields like price, etc. to determine which
            // ones to send to the customer, how would you implement it

            var sortableEvents = events;

            // We can use a lambda expression to allow many complex scenarios of sorting
            sortableEvents.Sort((x, y) => x.Price.CompareTo(y.Price));
            Console.WriteLine("----");

            foreach (var item in sortableEvents)
            {
                AddToEmail(customerVariable, item, (int)item.Price);
            }
        }

        // 4. If the GetDistance method can fail, we don't want the process to fail. What can be done?
        // Ensure the logic is wrapped in a try-catch block
        static int GetDistance(string fromCity, string toCity)
        {
            var distance = -1;
            try
            {
                distance = AlphabeticalDistance(fromCity, toCity);
            }
            catch (Exception ex)
            {
                return distance;
            }
            return distance;
        }

        
        // You do not need to know how these methods work

        static void AddToEmail(Customer c, Event e, int? price = null)
        {
            Console.Out.WriteLine($"Customer: {c.Name}, Event: {e.Name}" + (e.Distance > 0 ? $", ({e.Distance} miles away)" : "") + ($" for ${e.Price}"));
        }

        



        private static int AlphabeticalDistance(string s, string t)
        {
            var result = 0;
            var i = 0;
            for (i = 0; i < Math.Min(s.Length, t.Length); i++)
            {
                result += Math.Abs(s[i] - t[i]);
            }
            for (; i < Math.Max(s.Length, t.Length); i++)
            {
                result += s.Length > t.Length ? s[i] : t[i];
            }
            return result;
        }
    }
}