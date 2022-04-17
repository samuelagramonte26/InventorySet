drop DATABASE inventoryset;
create database inventorySet;
use inventorySet;

create table permissions(
    id int auto_increment primary key,
    permissions enum('read','write','remove') not null,
    description varchar(100),
    active enum('si','no') default 'si'
    );

create table userType(
    id int auto_increment primary key,
    userType varchar(30) not null,
    description varchar(100),
    active enum('si','no') default 'si'
);

create table user_permissions(
    id int auto_increment primary key,
    userType_id int,
    permissions_id int,
  active enum('si','no') default 'si',
   FOREIGN KEY(userType_id) references userType(id),
  FOREIGN KEY(permissions_id) references permissions(id)
);

create table users(
    id int auto_increment primary key,
    user varchar(30),
    password varchar(20),
    userType_id int,
   active enum('si','no') default 'si',
    FOREIGN key(userType_id) references userType(id)
);

create table product_category(
    id int auto_increment primary key,
    category varchar(50),
    description varchar(100),
    date_inserted date,
    date_edited date,
    date_removed date,
    user_creator int,
    user_editor int,
    user_delete int,
   active enum('si','no') default 'si'
);

create table location(
    id int auto_increment primary key,
    location varchar(100),
    date_inserted date,
    date_edited date,
    date_removed date,
    user_creator int,
    user_editor int,
    user_delete int,
   active enum('si','no') default 'si'
);

create table product(
    id int auto_increment primary key,
    product varchar(80),
    price_in float,
    price_out float,
    category_id int,
    location_id int,
    date_inserted date,
    date_edited date,
    date_removed date,
    user_creator int,
    user_editor int,
    user_delete int,
   active enum('si','no') default 'si',
    FOREIGN KEY(category_id) references product_category(id),
    FOREIGN KEY(location_id) references location(id)
);

create table inventory(
id int auto_increment primary key,
product_id int,
quantity int,
stock int,
action enum('IN','OUT'),
date_inserted date,
date_edited date,
date_removed date,
user_creator int,
user_editor int,
user_delete int,
active enum('si','no') default 'si',
FOREIGN KEY(product_id) references product(id)


);

insert into permissions(permissions,description) values (1,'solo lectura'),(2,'solo escritura'),(3,' solo remover');
insert into userType(userType,description) values ('administrador','usuario con todos los privilegios');
insert into user_permissions(permissions_id,userType_id) values (1,1),(2,1),(3,1);
insert into users(user,password,userType_id) values ('admin','admin',1);