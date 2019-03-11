create table GuidQueries
(
	Id uniqueidentifier not null
		constraint GuidQueries_pk
			primary key nonclustered,
	ExternalId uniqueidentifier not null
)
go
insert into GuidQueries (Id, ExternalId) values ('00000000-0000-0000-0000-000000000001','00000000-0000-0000-0000-00000000000A')
go
insert into GuidQueries (Id, ExternalId) values ('00000000-0000-0000-0000-000000000002','00000000-0000-0000-0000-00000000000B')
go