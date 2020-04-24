using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Entities
{
    public class Customer : Entity
    {
        private string _name;  
        public virtual CustomerName Name 
        {
            get => (CustomerName)_name;
            set => _name = value; 
        }

        private string _email;
        public virtual Email Email
        {
            get => (Email)_email;
            protected set => _email = value;
        }

       public virtual CustomerStatus Status { get; set; }

        private decimal _moneySpent;
        public virtual Dollars MoneySpent
        {
            get => Dollars.Of(_moneySpent);
            protected set => _moneySpent = value;
        }

        private readonly IList<PurchasedMovie> _purchasedMovies;
        public virtual IReadOnlyCollection<PurchasedMovie> PurchasedMovies => _purchasedMovies.ToList();

        protected Customer()
        {
            _purchasedMovies = new List<PurchasedMovie>();
        }

        public Customer(CustomerName name, Email email) : this()
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _email = email ?? throw new ArgumentNullException(nameof(email));

            Status = CustomerStatus.Regular;
            MoneySpent = Dollars.Of(0);
        }

        public virtual void AddPurchasedMovie(Movie movie, ExpirationDate expirationDate, Dollars price)
        {
            var purchasedMovie = new PurchasedMovie(movie, this, price, expirationDate);
            _purchasedMovies.Add(purchasedMovie);

            MoneySpent += price;
        }
    }
}
