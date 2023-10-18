create table CANALES
(
    ID_CN                   INTEGER               not null,
    NOMBRE_CN               VARCHAR(50)           not null,
    primary key (ID_CN)
);

create unique index CANALES_PK on CANALES (ID_CN asc);

create table TIPOS_CUENTAS
(
    ID_TC                   INTEGER               not null,
    DESCRIPCION_TC          VARCHAR(15)           not null,
    primary key (ID_TC)
);

create unique index TIPOS_CUENTAS_PK on TIPOS_CUENTAS (ID_TC asc);

create table PELICULAS
(
    ID_PL                   INTEGER               not null,
    TITULO_PL               VARCHAR(50)           not null,
    DESCRIPCION_PL          VARCHAR(500)                  ,
    DURACION_PL             VARCHAR(10)                   ,
    ELENCO_PL               VARCHAR(500)                  ,
    URL_PL                  VARCHAR(100)          not null,
	PORTADA_PL              VARCHAR(100)          not null,
    primary key (ID_PL)
);

create unique index PELICULAS_PK on PELICULAS (ID_PL asc);

create table SERIES
(
    ID_SR                   INTEGER               not null,
    NOMBRE_SR               VARCHAR(50)           not null,
    DESCRIPCION_SR          VARCHAR(500)                  ,
    CANT_CAPITULOS_SR       INTEGER                       ,
	PORTADA_SR              VARCHAR(100)          not null,
    primary key (ID_SR)
);

create unique index SERIES_PK on SERIES (ID_SR asc);

create table ESTADOS_CUENTAS
(
    ID_EC                   INTEGER               not null,
    DESCRIPCION_EC          VARCHAR(20)           not null,
    primary key (ID_EC)
);

create unique index ESTADOS_CUENTAS_PK on ESTADOS_CUENTAS (ID_EC asc);

create table GENEROS
(
    ID_GN                   INTEGER               not null,
    DESCRIPCION_GN          VARCHAR(30)           not null,
    primary key (ID_GN)
);

create unique index GENEROS_PK on GENEROS (ID_GN asc);

create table USUARIOS
(
    ID_USR                  INTEGER               not null,
    ID_TC                   INTEGER               not null,
    ID_EC                   INTEGER               not null,
    NOMBRE_USR              VARCHAR(50)           not null,
    APELLIDO_USR            VARCHAR(50)           not null,
    CORREO_USR              VARCHAR(50)           not null,
    PASSWORD_USR            VARCHAR(50)           not null,
    FECHA_ALTA_USR          DATE                  not null,
    FECHA_MODIFICACION_USR  DATE                  not null,
    VERIFICACION_USR        NUMERIC(1)            not null,
    primary key (ID_USR),
    foreign key  (ID_TC)
       references TIPOS_CUENTAS (ID_TC),
    foreign key  (ID_EC)
       references ESTADOS_CUENTAS (ID_EC)
);

create unique index USUARIOS_PK on USUARIOS (ID_USR asc);

create index RELATION_23_FK on USUARIOS (ID_TC asc);

create index RELATION_31_FK on USUARIOS (ID_EC asc);

create table CAPITULO_SERIE
(
    ID_SR                   INTEGER               not null,
    ID_CP                   INTEGER               not null,
    NOMBRE_CP               VARCHAR(50)           not null,
    DESCRIPCION_CP          VARCHAR(100)                  ,
    DURACION_CP             VARCHAR(10)                   ,
    URL_CP                  VARCHAR(100)          not null,
	PORTADA_CP              VARCHAR(100)          not null,
    primary key (ID_SR, ID_CP),
    foreign key  (ID_SR)
       references SERIES (ID_SR)
);

create unique index CAPITULO_SERIE_PK on CAPITULO_SERIE (ID_SR asc, ID_CP asc);

create index RELATION_91_FK on CAPITULO_SERIE (ID_SR asc);

create table GENEROS_PELICULAS
(
    ID_GN                   INTEGER               not null,
    ID_PL                   INTEGER               not null,
    primary key (ID_GN, ID_PL),
    foreign key  (ID_GN)
       references GENEROS (ID_GN),
    foreign key  (ID_PL)
       references PELICULAS (ID_PL)
);

create unique index RELATION_88_PK on GENEROS_PELICULAS (ID_GN asc, ID_PL asc);

create index RELATION_88_FK2 on GENEROS_PELICULAS (ID_GN asc);

create index RELATION_88_FK on GENEROS_PELICULAS (ID_PL asc);

create table GENEROS_SERIES
(
    ID_GN                   INTEGER               not null,
    ID_SR                   INTEGER               not null,
    primary key (ID_GN, ID_SR),
    foreign key  (ID_GN)
       references GENEROS (ID_GN),
    foreign key  (ID_SR)
       references SERIES (ID_SR)
);

create unique index RELATION_89_PK on GENEROS_SERIES (ID_GN asc, ID_SR asc);

create index RELATION_89_FK2 on GENEROS_SERIES (ID_GN asc);

create index RELATION_89_FK on GENEROS_SERIES (ID_SR asc);

create table HISTORIAL_PELICULAS
(
    ID_PL                   INTEGER               not null,
    ID_USR                  INTEGER               not null,
    primary key (ID_PL, ID_USR),
    foreign key  (ID_PL)
       references PELICULAS (ID_PL),
    foreign key  (ID_USR)
       references USUARIOS (ID_USR)
);

create unique index HISTORIAL_PELICULAS_PK on HISTORIAL_PELICULAS (ID_PL asc, ID_USR asc);

create index HISTORIAL_PELICULAS_FK2 on HISTORIAL_PELICULAS (ID_PL asc);

create index HISTORIAL_PELICULAS_FK on HISTORIAL_PELICULAS (ID_USR asc);

create table FAVORITOS_PELICULA
(
    ID_PL                   INTEGER               not null,
    ID_USR                  INTEGER               not null,
    primary key (ID_PL, ID_USR),
    foreign key  (ID_PL)
       references PELICULAS (ID_PL),
    foreign key  (ID_USR)
       references USUARIOS (ID_USR)
);

create unique index FAVORITOS_PELICULA_PK on FAVORITOS_PELICULA (ID_PL asc, ID_USR asc);

create index FAVORITOS_PELICULA_FK2 on FAVORITOS_PELICULA (ID_PL asc);

create index FAVORITOS_PELICULA_FK on FAVORITOS_PELICULA (ID_USR asc);

create table HISTORIAL_SERIES
(
    ID_SR                   INTEGER               not null,
    ID_USR                  INTEGER               not null,
    primary key (ID_SR, ID_USR),
    foreign key  (ID_SR)
       references SERIES (ID_SR),
    foreign key  (ID_USR)
       references USUARIOS (ID_USR)
);

create unique index HISTORIAL_SERIES_PK on HISTORIAL_SERIES (ID_SR asc, ID_USR asc);

create index HISTORIAL_SERIES_FK2 on HISTORIAL_SERIES (ID_SR asc);

create index HISTORIAL_SERIES_FK on HISTORIAL_SERIES (ID_USR asc);

create table FAVORITOS_SERIES
(
    ID_SR                   INTEGER               not null,
    ID_USR                  INTEGER               not null,
    primary key (ID_SR, ID_USR),
    foreign key  (ID_SR)
       references SERIES (ID_SR),
    foreign key  (ID_USR)
       references USUARIOS (ID_USR)
);

create unique index FAVORITOS_SERIES_PK on FAVORITOS_SERIES (ID_SR asc, ID_USR asc);

create index FAVORITOS_SERIES_FK2 on FAVORITOS_SERIES (ID_SR asc);

create index FAVORITOS_SERIES_FK on FAVORITOS_SERIES (ID_USR asc);

