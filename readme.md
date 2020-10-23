# Roofstock Properties


## Getting Started

This project requires setting up a Database in order to work. Please run the following script in your database and change AppSetting.json database connection configuration to your desire database.

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Properties](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[address] [varchar](150) NOT NULL,
	[year_built] [int] NOT NULL,
	[list_price] [numeric](15, 2) NOT NULL,
	[monthly_rent] [numeric](10, 2) NOT NULL,
	[gross_yield] [numeric](6, 2) NOT NULL,
 CONSTRAINT [PK__Properti__3214EC0700D74988] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)
GO


### Prerequisites

If you want build the application locally please use visual studio 2017 or 2019.

```
If you just want to run the aplication use the release version and alter the configuration file.
```

### Installing

Upon downloading rebuild test project and then rebuild the solution. 


## Authors

* **Jose Robles**
