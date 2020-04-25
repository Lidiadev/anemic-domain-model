using CSharpFunctionalExtensions;
using Logic.Common;
using Logic.Movies;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Customers
{
    public class Customer : Entity
    {
        private string _name;  
        public virtual CustomerName Name 
        {
            get => (CustomerName)_name;
            set => _name = value; 
        }

        private readonly string _email;
        public virtual Email Email => (Email)_email;

        public virtual CustomerStatus Status { get; protected set; }

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

        public virtual void PurchasedMovie(Movie movie)
        {
            if (HasPurchasedMovie(movie))
                throw new Exception($"The movie is already purchased: {movie.Name}.");

            ExpirationDate expirationDate = movie.GetExpirationDate();
            Dollars price = movie.CalculatePrice(Status);

            var purchasedMovie = new PurchasedMovie(movie, this, price, expirationDate);
            _purchasedMovies.Add(purchasedMovie);

            MoneySpent += price;
        }

        public virtual void Promote()
        {
            if (CanPromote().IsFailure)
                throw new Exception("The customer cannot be promoted.");

            Status = Status.Promote();
        }

        public virtual Result CanPromote()
        {
            if (Status.IsAdvanced)
                return Result.Failure("The customer already has the Advanced status.");

            if (PurchasedMovies.Count(x =>
                x.ExpirationDate == ExpirationDate.Infinite || x.ExpirationDate.Date >= DateTime.UtcNow.AddDays(-30)) < 2)
                return Result.Failure("The customer has to have at least 2 active movies during the last 30 days.");

            if (PurchasedMovies.Where(x => x.PurchaseDate > DateTime.UtcNow.AddYears(-1)).Sum(x => x.Price) < 100m)
                return Result.Failure("The customer has to have at least 100 dollars spent during the last year.");

            return Result.Ok();
        }

        public virtual bool HasPurchasedMovie(Movie movie)
        {
            return PurchasedMovies.Any(x => x.Movie == movie && !x.ExpirationDate.IsExpired);
        }
    }
}
