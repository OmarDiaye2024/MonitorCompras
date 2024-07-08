
GO
ALTER DATABASE [compras] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [compras].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [compras] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [compras] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [compras] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [compras] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [compras] SET ARITHABORT OFF 
GO
ALTER DATABASE [compras] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [compras] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [compras] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [compras] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [compras] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [compras] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [compras] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [compras] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [compras] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [compras] SET  DISABLE_BROKER 
GO
ALTER DATABASE [compras] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [compras] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [compras] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [compras] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [compras] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [compras] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [compras] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [compras] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [compras] SET  MULTI_USER 
GO
ALTER DATABASE [compras] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [compras] SET DB_CHAINING OFF 
GO
ALTER DATABASE [compras] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [compras] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [compras] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [compras] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [compras] SET QUERY_STORE = OFF
GO
USE [compras]
GO
/****** Object:  Table [dbo].[Article]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Article](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GA_CODEARTICLE] [varchar](50) NULL,
	[GA_CHARLIBRE3] [varchar](50) NULL,
	[GA_FOURNPRINC] [varchar](50) NULL,
	[GA_LIBREART2] [int] NULL,
	[GA_LIBREART5] [int] NULL,
	[GA_LIBELLE] [varchar](100) NULL,
	[GA_ARTICLE] [varchar](50) NULL,
	[GA_CODEBARRE] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BestSeller]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BestSeller](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CodigoArticulo] [varchar](100) NULL,
	[Descripcion] [varchar](100) NULL,
	[Infaltable] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CodigoMarca]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CodigoMarca](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GA2_FAMILLENIV4] [nvarchar](50) NULL,
	[GA2_CODEARTICLE] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DatoProveedores]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DatoProveedores](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[T_TIERS] [varchar](50) NULL,
	[T_LIBELLE] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Envase]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Envase](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[YX_CODE] [bigint] NULL,
	[YX_LIBELLE] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MarcasSucursal]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MarcasSucursal](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[rubro] [varchar](50) NULL,
	[marca] [varchar](50) NULL,
	[proveedor] [varchar](50) NULL,
	[descripcion] [varchar](50) NULL,
	[sucursal] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NombreMarcas]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NombreMarcas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CC_CODE] [varchar](50) NULL,
	[CC_LIBELLE] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NombreProveedores]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NombreProveedores](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[T_TIERS] [varchar](50) NULL,
	[T_LIBELLE] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NombreSucursales]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NombreSucursales](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ET_ETABLISSEMENT] [varchar](50) NULL,
	[ET_LIBELLE] [varchar](50) NULL,
	[ESTADO] [varchar](50) NULL,
	[OBSERVACIONES] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OC1201]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OC1201](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PK] [varchar](50) NULL,
	[SKU] [varchar](50) NULL,
	[SUC] [int] NULL,
	[UN] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OcPendientes]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OcPendientes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PK] [varchar](50) NULL,
	[GL_CODEARTICLE] [varchar](50) NULL,
	[GL_DEPOT] [varchar](50) NULL,
	[OCs_Pendientes] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PedidosTransito]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PedidosTransito](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PK] [varchar](50) NULL,
	[GL_CODEARTICLE] [varchar](50) NULL,
	[GL_DEPOT] [varchar](50) NULL,
	[Pedidos_En_Transito] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pendientes]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pendientes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PK] [varchar](50) NULL,
	[SKU] [varchar](50) NULL,
	[SUC] [int] NULL,
	[UN] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Preparacion]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Preparacion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PK] [varchar](50) NULL,
	[SKU] [varchar](50) NULL,
	[SUC] [int] NULL,
	[UN] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pvp]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pvp](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GF_ARTICLE] [varchar](50) NULL,
	[GF_PRIXUNITAIRE] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rotacion]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rotacion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SKU] [varchar](50) NULL,
	[ROTACION] [varchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rubros]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rubros](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[YX_CODE] [numeric](18, 0) NULL,
	[YX_LIBELLE] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SapStock]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SapStock](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ARTICULO] [varchar](100) NULL,
	[CODTIENDA] [varchar](100) NULL,
	[OnHand] [varchar](100) NULL,
	[IsCommited] [varchar](100) NULL,
	[StockValido] [varchar](100) NULL,
	[Concatenado] [varchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stock_Fisico]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock_Fisico](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Stock_Fisico] [int] NULL,
	[PK] [varchar](100) NULL,
	[GQ_DEPOT] [int] NULL,
	[GQ_ARTICLE] [varchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockProveedor]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockProveedor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EAN13] [numeric](18, 0) NULL,
	[SKU_CORTO] [varchar](50) NULL,
	[STOCK] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SucursalesInactivas]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SucursalesInactivas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ET_ETABLISSEMENT] [varchar](50) NULL,
	[ET_LIBELLE] [varchar](50) NULL,
	[ESTADO] [varchar](50) NULL,
	[OBSERVACIONES] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuarios]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[usuario] [varchar](50) NULL,
	[clave] [varchar](50) NULL,
	[mail] [varchar](50) NULL,
	[nombre] [varchar](50) NULL,
	[apellido] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta_Mes_1]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta_Mes_1](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Mes_1] [varchar](100) NULL,
	[PK] [varchar](100) NULL,
	[GL_CODEARTICLE] [varchar](100) NULL,
	[GL_ARTICLE] [varchar](100) NULL,
	[GL_LIBELLE] [varchar](100) NULL,
	[GL_DEPOT] [varchar](100) NULL,
	[Venta_Mes_En_Curso] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta_Mes_10]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta_Mes_10](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Mes_10] [varchar](100) NULL,
	[PK] [varchar](100) NULL,
	[GL_CODEARTICLE] [varchar](100) NULL,
	[GL_ARTICLE] [varchar](100) NULL,
	[GL_LIBELLE] [varchar](100) NULL,
	[GL_DEPOT] [varchar](100) NULL,
	[Venta_Mes_En_Curso] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta_Mes_11]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta_Mes_11](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Mes_11] [varchar](100) NULL,
	[PK] [varchar](100) NULL,
	[GL_CODEARTICLE] [varchar](100) NULL,
	[GL_ARTICLE] [varchar](100) NULL,
	[GL_LIBELLE] [varchar](100) NULL,
	[GL_DEPOT] [varchar](100) NULL,
	[Venta_Mes_En_Curso] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta_Mes_12]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta_Mes_12](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Mes_12] [varchar](100) NULL,
	[PK] [varchar](100) NULL,
	[GL_CODEARTICLE] [varchar](100) NULL,
	[GL_ARTICLE] [varchar](100) NULL,
	[GL_LIBELLE] [varchar](100) NULL,
	[GL_DEPOT] [varchar](100) NULL,
	[Venta_Mes_En_Curso] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta_Mes_13]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta_Mes_13](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Mes_13] [varchar](100) NULL,
	[PK] [varchar](100) NULL,
	[GL_CODEARTICLE] [varchar](100) NULL,
	[GL_ARTICLE] [varchar](100) NULL,
	[GL_LIBELLE] [varchar](100) NULL,
	[GL_DEPOT] [varchar](100) NULL,
	[Venta_Mes_En_Curso] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta_Mes_14]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta_Mes_14](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Mes_14] [varchar](100) NULL,
	[PK] [varchar](100) NULL,
	[GL_CODEARTICLE] [varchar](100) NULL,
	[GL_ARTICLE] [varchar](100) NULL,
	[GL_LIBELLE] [varchar](100) NULL,
	[GL_DEPOT] [varchar](100) NULL,
	[Venta_Mes_En_Curso] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta_Mes_15]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta_Mes_15](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Mes_15] [varchar](100) NULL,
	[PK] [varchar](100) NULL,
	[GL_CODEARTICLE] [varchar](100) NULL,
	[GL_ARTICLE] [varchar](100) NULL,
	[GL_LIBELLE] [varchar](100) NULL,
	[GL_DEPOT] [varchar](100) NULL,
	[Venta_Mes_En_Curso] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta_Mes_16]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta_Mes_16](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Mes_16] [varchar](100) NULL,
	[PK] [varchar](100) NULL,
	[GL_CODEARTICLE] [varchar](100) NULL,
	[GL_ARTICLE] [varchar](100) NULL,
	[GL_LIBELLE] [varchar](100) NULL,
	[GL_DEPOT] [varchar](100) NULL,
	[Venta_Mes_En_Curso] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta_Mes_17]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta_Mes_17](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Mes_17] [varchar](100) NULL,
	[PK] [varchar](100) NULL,
	[GL_CODEARTICLE] [varchar](100) NULL,
	[GL_ARTICLE] [varchar](100) NULL,
	[GL_LIBELLE] [varchar](100) NULL,
	[GL_DEPOT] [varchar](100) NULL,
	[Venta_Mes_En_Curso] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta_Mes_18]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta_Mes_18](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Mes_18] [varchar](100) NULL,
	[PK] [varchar](100) NULL,
	[GL_CODEARTICLE] [varchar](100) NULL,
	[GL_ARTICLE] [varchar](100) NULL,
	[GL_LIBELLE] [varchar](100) NULL,
	[GL_DEPOT] [varchar](100) NULL,
	[Venta_Mes_En_Curso] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta_Mes_2]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta_Mes_2](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Mes_2] [varchar](100) NULL,
	[PK] [varchar](100) NULL,
	[GL_CODEARTICLE] [varchar](100) NULL,
	[GL_ARTICLE] [varchar](100) NULL,
	[GL_LIBELLE] [varchar](100) NULL,
	[GL_DEPOT] [varchar](100) NULL,
	[Venta_Mes_En_Curso] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta_Mes_3]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta_Mes_3](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Mes_3] [varchar](100) NULL,
	[PK] [varchar](100) NULL,
	[GL_CODEARTICLE] [varchar](100) NULL,
	[GL_ARTICLE] [varchar](100) NULL,
	[GL_LIBELLE] [varchar](100) NULL,
	[GL_DEPOT] [varchar](100) NULL,
	[Venta_Mes_En_Curso] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta_Mes_4]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta_Mes_4](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Mes_4] [varchar](100) NULL,
	[PK] [varchar](100) NULL,
	[GL_CODEARTICLE] [varchar](100) NULL,
	[GL_ARTICLE] [varchar](100) NULL,
	[GL_LIBELLE] [varchar](100) NULL,
	[GL_DEPOT] [varchar](100) NULL,
	[Venta_Mes_En_Curso] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta_Mes_5]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta_Mes_5](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Mes_5] [varchar](100) NULL,
	[PK] [varchar](100) NULL,
	[GL_CODEARTICLE] [varchar](100) NULL,
	[GL_ARTICLE] [varchar](100) NULL,
	[GL_LIBELLE] [varchar](100) NULL,
	[GL_DEPOT] [varchar](100) NULL,
	[Venta_Mes_En_Curso] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta_Mes_6]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta_Mes_6](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Mes_6] [varchar](100) NULL,
	[PK] [varchar](100) NULL,
	[GL_CODEARTICLE] [varchar](100) NULL,
	[GL_ARTICLE] [varchar](100) NULL,
	[GL_LIBELLE] [varchar](100) NULL,
	[GL_DEPOT] [varchar](100) NULL,
	[Venta_Mes_En_Curso] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta_Mes_7]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta_Mes_7](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Mes_7] [varchar](100) NULL,
	[PK] [varchar](100) NULL,
	[GL_CODEARTICLE] [varchar](100) NULL,
	[GL_ARTICLE] [varchar](100) NULL,
	[GL_LIBELLE] [varchar](100) NULL,
	[GL_DEPOT] [varchar](100) NULL,
	[Venta_Mes_En_Curso] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta_Mes_8]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta_Mes_8](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Mes_8] [varchar](100) NULL,
	[PK] [varchar](100) NULL,
	[GL_CODEARTICLE] [varchar](100) NULL,
	[GL_ARTICLE] [varchar](100) NULL,
	[GL_LIBELLE] [varchar](100) NULL,
	[GL_DEPOT] [varchar](100) NULL,
	[Venta_Mes_En_Curso] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta_Mes_9]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta_Mes_9](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Mes_9] [varchar](100) NULL,
	[PK] [varchar](100) NULL,
	[GL_CODEARTICLE] [varchar](100) NULL,
	[GL_ARTICLE] [varchar](100) NULL,
	[GL_LIBELLE] [varchar](100) NULL,
	[GL_DEPOT] [varchar](100) NULL,
	[Venta_Mes_En_Curso] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta_Mes_En_Curso]    Script Date: 16/5/2024 10:20:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta_Mes_En_Curso](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Mes_En_Curso] [varchar](100) NULL,
	[PK] [varchar](100) NULL,
	[GL_CODEARTICLE] [varchar](100) NULL,
	[GL_ARTICLE] [varchar](100) NULL,
	[GL_LIBELLE] [varchar](100) NULL,
	[GL_DEPOT] [varchar](100) NULL,
	[Venta_Mes_En_Curso] [int] NULL
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [compras] SET  READ_WRITE 
GO
