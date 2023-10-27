FRIST 31.OKT.2023 KL.12

LISTE SOM MÅ GJØRES:
- Expanded db CRUD ( Dropp denne modulen for nå)
- Authentication authorisation (Mangler Hashing, Salting and Configuration Ikke helt ferdig)

- Design (Ikke noe ennå), Holder på med indeks så design på listing og order.
- Oppdatere Listing View, til å legge til dato, flere bilder pris per natt etc.  Flere bilder, antall rom, størrelse
- Lage en side for order ( Side for å bestille listings er nå fikset, det mangler å finpusse og legge til dropdown kalender )
- Gjøre sånn at bare lageren / admin av listing kan endre eller fjerne

FEATURES:
- Sort f.eks (billigst, størst, dyrest etc.) / Filter
- Datoer på renting?
- Adresse på renting sted
- Pris per natt, med totalpris over dato
- Kontakt karen som satt ut listing?
- Antall senger / størrelse på listing controller ( flere ting trengs )
- Mulig rating system?

Feil:

Noe feil med sammenhengen av RentListing - Rent- RentController (Mulig at det er noe feil med variabler der, kan ikke legge til ting i databasen selv direkte)
HomeController blandet med CustomerController

FERDIG:
- Unit testing ( Ferdig, OK med testene ) 
