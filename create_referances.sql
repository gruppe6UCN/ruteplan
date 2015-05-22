IF OBJECT_ID('Road', 'U') IS NOT NULL DROP TABLE Road;
IF OBJECT_ID('GeoLoc', 'U') IS NOT NULL DROP TABLE GeoLoc;
IF OBJECT_ID('TransportUnit', 'U') IS NOT NULL DROP TABLE TransportUnit;
IF OBJECT_ID('Link1', 'U') IS NOT NULL DROP TABLE Link1;
IF OBJECT_ID('DeliveryStop', 'U') IS NOT NULL DROP TABLE DeliveryStop;
IF OBJECT_ID('Route', 'U') IS NOT NULL DROP TABLE Route;
IF OBJECT_ID('DefaultLink1', 'U') IS NOT NULL DROP TABLE DefaultLink1;
IF OBJECT_ID('Customer', 'U') IS NOT NULL DROP TABLE Customer;
IF OBJECT_ID('DefaultDeliveryStop', 'U') IS NOT NULL DROP TABLE DefaultDeliveryStop;
IF OBJECT_ID('DefaultRoute', 'U') IS NOT NULL DROP TABLE DefaultRoute;



create table DefaultRoute(
    id bigint IDENTITY(1,1),
    trailer_type int not null,
    time_of_departure time not null,
    extra_route tinyint not null,
    primary key(id)
);

create table DefaultDeliveryStop(
    id bigint IDENTITY(1,1),
    default_route_id bigint not null,
    time_of_delivery time not null,
    primary key(id),
    foreign key(default_route_id) references DefaultRoute(id)
);

create table Customer(
    id bigint IDENTITY(1,1),
    default_delivery_stop_id bigint not null,
    street_name varchar(50) not null,
    street_no varchar(50) not null,
    zip_code int not null,
    city varchar(50) not null,
    primary key(id)
    foreign key(default_delivery_stop_id) references DefaultDeliveryStop(id)
);

create table Route(
    id bigint IDENTITY(1,1),
    actual_of_departure time not null,
    date date not null,
    primary key(id)
);

create table DeliveryStop(
    id bigint IDENTITY(1,1),
    route_id bigint not null,
    time_of_delivery time not null,
    primary key(id),
    foreign key(route_id) references Route(id),
);

create table Link1(
    customer_id bigint not null,
    delivery_stop_id bigint not null,
    primary key(customer_id, delivery_stop_id),
    foreign key(customer_id) references Customer(id),
    foreign key(delivery_stop_id) references DeliveryStop(id)
);

create table TransportUnit(
    id bigint IDENTITY(1,1),
    customer_id bigint not null,
    delivery_stop_id bigint not null,
    type int not null,
    primary key(id),
    foreign key(customer_id) references Customer(id),
    foreign key(delivery_stop_id) references DeliveryStop(id)
);

create table GeoLoc(
    id bigint IDENTITY(1,1),
    delivery_stop_id bigint not null,
    x decimal not null,
    y decimal not null,
    primary key(id),
    foreign key(delivery_stop_id) references DeliveryStop(id)
);

create table Road (
    id bigint IDENTITY(1,1),
    from_ bigint not null,
    to_ bigint not null,
    distance decimal not null,
    time time not null,
    primary key(id),
    foreign key(from_) references GeoLoc(id),
    foreign key(to_) references GeoLoc(id)
);
