/*TRIGGER*/
///Supression de Reservation
create trigger t1 on Reservation instead of Delete as
begin
Declare @a int 
set @a = (select codeRes from deleted);
delete from QteReservee where codeRes = @a
delete from Reservation where codeRes = @a
end

///ajout de reservation
create trigger t2 on Reservation for insert as
begin
update Serveur set nbrRes = nbrRes+1 from Serveur S,inserted I where S.IDSer = I.IDSer;
declare @a int
set @a = (select nbrRes From inserted I,Serveur S where S.IDSer = I.IDSEr)
if (@a = 3)
update Serveur set actif = '-' From serveur S,Inserted I where S.IDSer = I.IDSer
end

///supression des serveurs
create trigger t3 on Reservation for Delete as
begin
update Serveur set nbrRes = nbrRes-1 from Serveur S,inserted I where S.IDSer = I.IDSer;
declare @a int
set @a = (select nbrRes From inserted I,Serveur S where S.IDSer = I.IDSEr)
if (@a != 3)
update Serveur set actif = '+' From serveur S,Inserted I where S.IDSer = I.IDSer
end

///interdit de supprimer un serveur qu a encore des reservation
create trigger t4 on Serveur instead of delete
as begin
declare @nbrRes int
set @nbrRes = (select S.nbrRes from Serveur S,deleted D where S.IDSer = D.IDSer)
if (@nbrRes != 0)
raiserror ('Impossible de Supprimmer le Serveur : Il a encore des Reservation',48,5)
else 
delete from Serveur where IDSer = (select IDSer from deleted)  
end

///interdit de supprimer une tablle encore utiliser
create trigger t5 on Tablee instead of delete 
as begin 
declare @NumT int 
if exists (select Numtablee from deleted where NumTablee in (select NumTablee from reservation))
raiserror ('Impossible de Supprimmer la table : Il est encore utilise',48,5)
else 
delete from tablee where Numtablee = (select NumTablee from deleted)
end

/*PROCEDURE*/

create proc table_disponible as begin 
declare @NumT int,@NbrPlace int
create Table T (Num_Table int,Nbr_Place int)
declare Cr1 cursor for select NumTablee,NbrPlaceTablee from tablee
open Cr1
fetch next from Cr1 into @NumT,@NbrPlace
while (@@FETCH_STATUS = 0) 
begin 
if exists (select NumTablee from tablee where NumTablee not in (Select NumTablee from reservation) and NumTablee = @NumT)
insert into T values (@NumT,@NbrPlace) 
fetch next from Cr1 into @NumT,@NbrPlace
end 
close Cr1
deallocate Cr1
select * from T
end

create proc Facture @CodR int as begin 
declare @nomPlat varchar(200),@PrixPlat decimal,@qte int,@Montant decimal
create Table Ta (Nom_Plat varchar(200),Prix_Plat decimal,Qte int,Montant decimal)
declare C1 cursor for 
select NomPlate,prixPlate,qte, prixplate*qte as 'prix total' from QteReservee q,Plate p where CodeRes= @CodR and p.codeplat=q.codeplat
open C1
fetch next from C1 into @nomPlat,@PrixPlat,@qte,@Montant
while (@@FETCH_STATUS = 0)
begin
insert into Ta values (@nomPlat,@PrixPlat,@qte,@Montant)
fetch next from C1 into @nomPlat,@PrixPlat,@qte,@Montant
end
close C1
deallocate C1
end


create proc reser_serveur @Log varchar(30) as begin 
declare @codeRes int,@date datetime,@numT int
create Table Te (CodeReservation int,dateReservation datetime,numero_table int)
declare C1 cursor for 
select r.codeRes,r.dateRes,r.numtablee from Reservation r,Serveur s where r.IDSer=s.IDSer and s.login=@Log
open C1
fetch next from C1 into @codeRes,@date,@numT
while (@@FETCH_STATUS = 0)
begin
insert into Te values (@codeRes,@date,@numT)
fetch next from C1 into @codeRes,@date,@numT
end
close C1
deallocate C1
end

create proc annuler_res @a int
as
delete from Reservation
where CodeRes=@a

create proc inserer_qteRes @a int,@b int,@c int
as
insert into QteReservee values(@a,@b,@c)