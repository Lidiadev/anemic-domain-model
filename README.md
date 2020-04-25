# From anemic to a rich domain model 

A sample application on how to change the Domain Model from anemic to a rich, encapsulated one by:
- decoupling the domain models from the data contracts
- pushing the domain logic from services down to domain entities and value objects
- exposing the minumum mutable API possible
- not allowing the model to reside in an invalid state (almost no public setters and fewer primitive types).

### Branches

- [Initial](https://github.com/Lidiadev/anemic-domain-model/tree/initial) - the `initial` branch shows the anemic domain model before refactoring.
- Master - the `master` branch shows the code after refactoring the anemic domain model. 
