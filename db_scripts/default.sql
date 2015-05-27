delete from Customer
delete from DefaultDeliveryStop
delete from DefaultRoute

DECLARE @dr_id bigint;
DECLARE @dds_id bigint;
DECLARE @c_id bigint;

INSERT into DefaultRoute values('STOR', '1:00:00', 0);
set @dr_id = SCOPE_IDENTITY();
INSERT into DefaultDeliveryStop values(@dr_id, '1:30:00');
SET @dds_id = SCOPE_IDENTITY();
INSERT into Customer values(@dds_id, 'Adelgade', 39, 9500, 'Hobro');
SET @c_id = SCOPE_IDENTITY();
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into DefaultDeliveryStop values(@dr_id, '2:30:00');
SET @dds_id = SCOPE_IDENTITY();
INSERT into Customer values(@dds_id, 'Mariagervej', 175, 8920, 'Randers Nv');
SET @c_id = SCOPE_IDENTITY();
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into DefaultDeliveryStop values(@dr_id, '3:30:00');
SET @dds_id = SCOPE_IDENTITY();
INSERT into Customer values(@dds_id, 'Dytmærsken', 14, 8900, 'Randers C');
SET @c_id = SCOPE_IDENTITY();
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into DefaultDeliveryStop values(@dr_id, '4:30:00');
SET @dds_id = SCOPE_IDENTITY();
INSERT into Customer values(@dds_id, 'Gravene', 37, 8800, 'Viborg');
SET @c_id = SCOPE_IDENTITY();
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into DefaultDeliveryStop values(@dr_id, '5:30:00');
SET @dds_id = SCOPE_IDENTITY();
INSERT into Customer values(@dds_id, 'Søndergade', 19, 7800, 'Skive');
SET @c_id = SCOPE_IDENTITY();
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')



INSERT into DefaultRoute values('STOR', '2:00:00', 0);
SET @dr_id = SCOPE_IDENTITY();
INSERT into DefaultDeliveryStop values(@dr_id, '2:30:00');
SET @dds_id = SCOPE_IDENTITY();
INSERT into Customer values(@dds_id, 'Jernbanegade', 9, 9530, 'Støvring');
SET @c_id = SCOPE_IDENTITY();
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into DefaultDeliveryStop values(@dr_id, '3:30:00');
SET @dds_id = SCOPE_IDENTITY();
INSERT into Customer values(@dds_id, 'Banegårdsvej', 4, 9500, 'Hobro');
SET @c_id = SCOPE_IDENTITY();
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into DefaultDeliveryStop values(@dr_id, '4:30:00');
SET @dds_id = SCOPE_IDENTITY();
INSERT into Customer values(@dds_id, 'Jernbanegade', 6, 9560, 'Hadsund');
SET @c_id = SCOPE_IDENTITY();
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into DefaultDeliveryStop values(@dr_id, '5:30:00');
SET @dds_id = SCOPE_IDENTITY();
INSERT into Customer values(@dds_id, 'Mariagervej', 78, 8920, 'Randers NV');
SET @c_id = SCOPE_IDENTITY();
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into DefaultDeliveryStop values(@dr_id, '6:30:00');
SET @dds_id = SCOPE_IDENTITY();
INSERT into Customer values(@dds_id, 'Vennelystvej', 35, 8960, 'Randers SØ');
SET @c_id = SCOPE_IDENTITY();
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')



INSERT into DefaultRoute values('STOR', '3:00:00', 0);
SET @dr_id = SCOPE_IDENTITY();
INSERT into DefaultDeliveryStop values(@dr_id, '3:30:00');
SET @dds_id = SCOPE_IDENTITY();
INSERT into Customer values(@dds_id, 'Fruensgaard Plads', 1, 9550, 'Mariager');
SET @c_id = SCOPE_IDENTITY();
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into DefaultDeliveryStop values(@dr_id, '4:30:00');
SET @dds_id = SCOPE_IDENTITY();
INSERT into Customer values(@dds_id, 'Himmerlandsgade', 2, 9560, 'Hadsund');
SET @c_id = SCOPE_IDENTITY();
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into DefaultDeliveryStop values(@dr_id, '5:30:00');
SET @dds_id = SCOPE_IDENTITY();
INSERT into Customer values(@dds_id, 'Terndrup Center', 1, 9575, 'Terndrup');
SET @c_id = SCOPE_IDENTITY();
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into DefaultDeliveryStop values(@dr_id, '6:30:00');
SET @dds_id = SCOPE_IDENTITY();
INSERT into Customer values(@dds_id, 'Danmarksgade', 21, 9293, 'Kongerslev');
SET @c_id = SCOPE_IDENTITY();
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into DefaultDeliveryStop values(@dr_id, '7:30:00');
SET @dds_id = SCOPE_IDENTITY();
INSERT into Customer values(@dds_id, 'Hobrovej', 53, 9530, 'Støvring');
SET @c_id = SCOPE_IDENTITY();
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')



INSERT into DefaultRoute values('STOR', '4:00:00', 0);
SET @dr_id = SCOPE_IDENTITY();
INSERT into DefaultDeliveryStop values(@dr_id, '4:30:00');
SET @dds_id = SCOPE_IDENTITY();
INSERT into Customer values(@dds_id, 'Thurøvej', 9, 9500, 'Hobro');
SET @c_id = SCOPE_IDENTITY();
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into DefaultDeliveryStop values(@dr_id, '5:30:00');
SET @dds_id = SCOPE_IDENTITY();
INSERT into Customer values(@dds_id, 'Forsythiavej', 2, 9500, 'Hobro');
SET @c_id = SCOPE_IDENTITY();
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into DefaultDeliveryStop values(@dr_id, '6:30:00');
SET @dds_id = SCOPE_IDENTITY();
INSERT into Customer values(@dds_id, 'Mariagervej', 65, 8920, 'Randers');
SET @c_id = SCOPE_IDENTITY();
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into DefaultDeliveryStop values(@dr_id, '7:30:00');
SET @dds_id = SCOPE_IDENTITY();
INSERT into Customer values(@dds_id, 'Vestervangsvej', 12, 8800, 'Viborg');
SET @c_id = SCOPE_IDENTITY();
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into DefaultDeliveryStop values(@dr_id, '8:30:00');
SET @dds_id = SCOPE_IDENTITY();
INSERT into Customer values(@dds_id, 'Søndergade', 1, 7800, 'Skive');
SET @c_id = SCOPE_IDENTITY();
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')



INSERT into DefaultRoute values('STOR', '5:00:00', 0);
SET @dr_id = SCOPE_IDENTITY();
INSERT into DefaultDeliveryStop values(@dr_id, '5:30:00');
SET @dds_id = SCOPE_IDENTITY();
INSERT into Customer values(@dds_id, 'H I BIES PLADS', 2, 9500, 'HOBRO');
SET @c_id = SCOPE_IDENTITY();
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into DefaultDeliveryStop values(@dr_id, '6:30:00');
SET @dds_id = SCOPE_IDENTITY();
INSERT into Customer values(@dds_id, 'HOSTRUPVEJ', 87, 9500, 'HOBRO');
SET @c_id = SCOPE_IDENTITY();
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into DefaultDeliveryStop values(@dr_id, '7:30:00');
SET @dds_id = SCOPE_IDENTITY();
INSERT into Customer values(@dds_id, 'HOSTRUPVEJ', 87, 9500, 'HOBRO');
SET @c_id = SCOPE_IDENTITY();
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into DefaultDeliveryStop values(@dr_id, '8:30:00');
SET @dds_id = SCOPE_IDENTITY();
INSERT into Customer values(@dds_id, 'TERNDRUP CENTER', 1, 9575, 'TERNDRUP');
SET @c_id = SCOPE_IDENTITY();
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into DefaultDeliveryStop values(@dr_id, '9:30:00');
SET @dds_id = SCOPE_IDENTITY();
INSERT into Customer values(@dds_id, 'SVERRIGGÅRDSVEJ CENTRET', 16, 9520, 'SKØRPING');
SET @c_id = SCOPE_IDENTITY();
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')
INSERT into TransportUnit values(@dds_id, @c_id, 'EU_PAL')


select * from TransportUnit
