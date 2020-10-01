using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinvoiceLoadDataMisa.Config
{
    class CreateLogs
    {

        public static string _CreateDB = $@" USE [master] " +

            $@" CREATE DATABASE [LogMinvoice] ON  PRIMARY " +
            $@" ( NAME = N'LogMinvoice', FILENAME = N'D:\LogMinvoice\LogMinvoice.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB ) " +
            $@"  LOG ON " +
            $@" ( NAME = N'LogMinvoice_log', FILENAME = N'D:\LogMinvoice\LogMinvoice.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%) " +

            $@" ALTER DATABASE [LogMinvoice] SET COMPATIBILITY_LEVEL = 100";

        public static string _AlterDB = $@" USE [LogMinvoice] " +

            $@" IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled')) " +
            
            $@" begin " +
            
            $@" EXEC[LogMinvoice].[dbo].[sp_fulltext_database] @action = 'enable' " +
            
            $@" end " +

            $@" ALTER DATABASE [LogMinvoice] SET ANSI_NULL_DEFAULT OFF " +

            $@" ALTER DATABASE [LogMinvoice] SET ANSI_NULLS OFF " +

            $@" ALTER DATABASE [LogMinvoice] SET ANSI_PADDING OFF " +

            $@" ALTER DATABASE [LogMinvoice] SET ANSI_WARNINGS OFF " +

            $@" ALTER DATABASE [LogMinvoice] SET ARITHABORT OFF " +

            $@" ALTER DATABASE [LogMinvoice] SET AUTO_CLOSE OFF " +

            $@" ALTER DATABASE [LogMinvoice] SET AUTO_SHRINK OFF " +

            $@" ALTER DATABASE [LogMinvoice] SET AUTO_UPDATE_STATISTICS ON " +

            $@" ALTER DATABASE [LogMinvoice] SET CURSOR_CLOSE_ON_COMMIT OFF " +

            $@" ALTER DATABASE [LogMinvoice] SET CURSOR_DEFAULT  GLOBAL " +

            $@" ALTER DATABASE [LogMinvoice] SET CONCAT_NULL_YIELDS_NULL OFF " +

            $@" ALTER DATABASE [LogMinvoice] SET NUMERIC_ROUNDABORT OFF " +

            $@" ALTER DATABASE [LogMinvoice] SET QUOTED_IDENTIFIER OFF " +

            $@" ALTER DATABASE [LogMinvoice] SET RECURSIVE_TRIGGERS OFF " +

            $@" ALTER DATABASE [LogMinvoice] SET  DISABLE_BROKER " +

            $@" ALTER DATABASE [LogMinvoice] SET AUTO_UPDATE_STATISTICS_ASYNC OFF " +

            $@" ALTER DATABASE [LogMinvoice] SET DATE_CORRELATION_OPTIMIZATION OFF " +

            $@" ALTER DATABASE [LogMinvoice] SET TRUSTWORTHY OFF " +

            $@" ALTER DATABASE [LogMinvoice] SET ALLOW_SNAPSHOT_ISOLATION OFF " +

            $@" ALTER DATABASE [LogMinvoice] SET PARAMETERIZATION SIMPLE " +

            $@" ALTER DATABASE [LogMinvoice] SET READ_COMMITTED_SNAPSHOT OFF " +

            $@" ALTER DATABASE [LogMinvoice] SET HONOR_BROKER_PRIORITY OFF " +

            $@" ALTER DATABASE [LogMinvoice] SET RECOVERY SIMPLE " +

            $@" ALTER DATABASE [LogMinvoice] SET  MULTI_USER " +

            $@" ALTER DATABASE [LogMinvoice] SET PAGE_VERIFY CHECKSUM  " +

            $@" ALTER DATABASE [LogMinvoice] SET DB_CHAINING OFF " +

            $@" USE [LogMinvoice] " +

            $@" SET ANSI_NULLS ON " +

            $@" SET QUOTED_IDENTIFIER ON " +

            $@" CREATE TABLE [dbo].[SaveLogs]( " +
            $@" 	[ID] [uniqueidentifier] NULL, " +
            $@" 	[NumberMisa] [nvarchar](50) NULL, " +
            $@" 	[NumberMinvoice] [nvarchar](250) NULL, " +
            $@" 	[TimeAdd] [datetime] NULL, " +
            $@" 	[JsonConvert] [nvarchar](max) NULL, " +
            $@" 	[Editmode] [nvarchar](1) NULL, " +
            $@" 	[Inv_invoiceAuth_ID] [nvarchar](50) NULL, " +
            $@" 	[RefID] [nvarchar](50) NULL, " +
            $@" 	[Type] [nvarchar](max) NULL, " +
            $@" 	[Seri] [nvarchar](max) NULL, " +
            $@" 	[result] [nvarchar](max) NULL " +
            $@" ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] " +
            $@" USE [master] " +
            $@" ALTER DATABASE [LogMinvoice] SET  READ_WRITE  ";


    }
}
