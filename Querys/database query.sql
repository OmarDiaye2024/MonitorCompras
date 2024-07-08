USE [master]
GO
/****** Object:  Database [compras]    Script Date: 16/5/2024 10:20:28 ******/
CREATE DATABASE [compras]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'compras', FILENAME = N'C:\Users\omarg\OneDrive\Escritorio\comprasAppOriginal\comprasApp\archivosDB\compras.mdf' , SIZE = 139264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'compras_log', FILENAME = N'C:\Users\omarg\OneDrive\Escritorio\comprasAppOriginal\comprasApp\archivosDB\compras_log.ldf' , SIZE = 466944KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
