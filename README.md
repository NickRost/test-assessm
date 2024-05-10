# test-assessm
CREATE DATABASE AssessmentDB;
CREATE TABLE [dbo].[Data] (
    [id]                    INT          NOT NULL IDENTITY,
    [tpep_pickup_datetime]  DATETIME     NULL,
    [tpep_dropoff_datetime] DATETIME     NULL,
    [passenger_count]       INT          NULL,
    [trip_distance]         FLOAT (53)   NULL,
    [store_and_fwd_flag]    NVARCHAR (1) NULL,
    [PULocationID]          INT          NULL,
    [DOLocationID]          INT          NULL,
    [fare_amount]           FLOAT (53)   NULL,
    [tip_amount]            FLOAT (53)   NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);


Number of rows in your table after running the program: 30000

If I had to implement it for larger files I would process data in batches.

I added all the methods. I was not sure if I have to create a full CLI with options asked but I implemented all the methods for this.
