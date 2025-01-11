--Skrypt do utworzenia bazy danych
use master;
create database Biblioteka;
use Biblioteka;
--Utworzenie tabel w bazie
CREATE TABLE kategoria (
id int IDENTITY(1,1) PRIMARY KEY,
nazwa varchar(40) UNIQUE,
);
CREATE TABLE klient (
id int IDENTITY(1,1) PRIMARY KEY,
imie varchar(20),
nazwisko varchar(40),
telefon varchar(9) UNIQUE,
email varchar(60) UNIQUE
);
CREATE TABLE ksiazka (
id int IDENTITY(1,1) PRIMARY KEY,
nr_biblioteczny varchar(10) UNIQUE,
tytul varchar(100),
autor varchar(60),
ISBN varchar(13),
kategoria int FOREIGN KEY REFERENCES kategoria(id),
dostepna bit
);
CREATE TABLE uprawnienia (
id int IDENTITY(1,1) PRIMARY KEY,
nazwa varchar(30)
);
CREATE TABLE uzytkownicy (
id int IDENTITY(1,1) PRIMARY KEY,
email varchar(60) UNIQUE,
haslo varchar(100),
imie varchar(20),
nazwisko varchar(40),
id_uprawnienia int FOREIGN KEY REFERENCES uprawnienia(id)
);
CREATE TABLE wypozyczenia (
id int IDENTITY(1,1) PRIMARY KEY,
id_ksiazka int FOREIGN KEY REFERENCES ksiazka(id) ON DELETE CASCADE,
id_klient int FOREIGN KEY REFERENCES klient(id) ON DELETE CASCADE,
data_wypozyczenia datetime,
data_zwrotu datetime
);
--Po utworzeniu bazy należy utworzyć widoki
create view KsiazkaPerKlient as
select w.id as "id_wypozyczenia", kl.id as "id_klient", ks.id as "id_ksiazka", ks.nr_biblioteczny, ks.tytul, ks.autor, ks.ISBN, w.data_wypozyczenia, w.data_zwrotu from wypozyczenia w
right join klient kl ON w.id_klient = kl.id
left join ksiazka ks on w.id_ksiazka = ks.id;
Create View ListaKlientow as
select k.id, k.imie as "imie", k.nazwisko as "nazwisko", k.telefon as "telefon", k.email as "email", (count(w.data_wypozyczenia) - count(w.data_zwrotu)) as "wypozyczone" from wypozyczenia w
right join klient k on k.id = w.id_klient
group by k.id, k.imie, k.nazwisko, k.telefon, k.email;
create view ListaKsiazek as
select ks.id, ks.nr_biblioteczny, ks.tytul, ks.autor, ks.ISBN, kt.nazwa as "kategoria", ks.dostepna from ksiazka ks
join kategoria kt on ks.kategoria = kt.id;
--Należy dodać poziomy uprawnień użytkowników
INSERT INTO uprawnienia (nazwa) VALUES ('Administrator');
INSERT INTO uprawnienia (nazwa) VALUES ('Lepsze');
INSERT INTO uprawnienia (nazwa) VALUES ('Gorsze');
INSERT INTO uprawnienia (nazwa) VALUES ('Nowy');