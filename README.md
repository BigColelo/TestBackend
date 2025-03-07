# Test Backend

Un'applicazione backend per la gestione di clienti, dipendenti e fornitori, con funzionalità di esportazione dei dati dei clienti in formato XML.

---

## **Panoramica**
Questo progetto è un'applicazione backend sviluppata in .NET che fornisce API per la gestione di clienti, dipendenti e fornitori. Include funzionalità come:
- Recupero di elenchi filtrati.
- Esportazione dei dati in formato XML.
- Seeding del database con dati di esempio per sviluppo.

---

## **Funzionalità**
- **Gestione Clienti**: Recupera e filtra lista di clienti in base a criteri specifici.
- **Gestione Dipendenti**: Recupera e filtra lista di dipendenti.
- **Gestione Fornitori**: Recupera e filtra lista di fornitori.
- **Esportazione XML**: Esporta i dati dei clienti filtrati in formato XML.
- **Seeding del Database**: Popola il database con dati di esempio per sviluppo.

---

## **Tecnologie Utilizzate**
- **.NET 9.0**: Framework principale per lo sviluppo dell'applicazione.
- **Entity Framework Core**: ORM per la gestione del database.
- **MediatR**: Per la gestione delle query e dei comandi.
- **Swagger**: Per la documentazione delle API.
- **SQLite**: Database leggero utilizzato per lo sviluppo.
- **Bogus**: Libreria per la generazione di dati fittizi per il seeding.
