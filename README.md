# From an anemic to a rich domain model 

A sample application on how to change the Domain Model from anemic to a rich, encapsulated one by:
- decoupling the domain models from the data contracts
- pushing the domain logic from services down to domain entities and value objects
- exposing the minumum mutable API possible
- not allowing the model to reside in an invalid state (by having almost no public setters and fewer primitive types).

### What are the symptoms of an anemic domain model:
- keeping data and operations separately from each other 
- classes with publicly accessible properties 
- stateless services.

### Drawbacks of an anemic domain model:
- potential duplication
- lack of encapsulation
- discoverability of operations.

### Advantages of a rich domain model:
- intuitive
- better discoverability of operations
- protecting the data integrity through encapsulation.

### When an anemic domain model is applicable:
- the project is simple
- the project will not be developed for a long period of time.

### Project Details

The sample project is an API for an online theater with the following features:
- create a customer
- update a customer
- purchase a movie
- provide discounts
- retrieve a list of all customers
- retireve details about a customer.

Technologies used:
- .NET Core 2.0
- .NET Framework 4.6.2
- NHibernate.

### Branches

- [Initial](https://github.com/Lidiadev/anemic-domain-model/tree/initial) - the `initial` branch shows the anemic domain model before refactoring.
- Master - the `master` branch shows the code after refactoring the anemic domain model. 
