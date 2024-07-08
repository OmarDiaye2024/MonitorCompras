using comprasApp.DataAccess;
using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace comprasApp.Controllers
{
    public class MaisonController : Controller
    {
        private DataAccessLayer dataAccess = new DataAccessLayer();
        public ActionResult IndexMaison()
        {


            ViewBag.Marcas = dataAccess.getSelectItems("SELECT * FROM NombreMarcas WHERE CC_CODE IN ('319', '320', '322', '343', '332', '335', '316', '312', '318', '321', '323'," +
                " '351', '338', '346', '329', '352', '347', '336','341', '324', '350', '360', '327', '340', '342', '337', '353','333', '354', '222') Order by CC_LIBELLE", "CC_LIBELLE", "CC_CODE");

            ViewBag.Tiendas = dataAccess.getSelectItems("SELECT * FROM NombreSucursales WHERE ET_ETABLISSEMENT IN ('3000', '3001','1800') Order by ET_LIBELLE", "ET_LIBELLE", "ET_ETABLISSEMENT");

            ViewBag.Proveedores = dataAccess.getSelectItems("SELECT * FROM NombreProveedores WHERE T_LIBELLE IN ('A M R S.A.', 'ARRUZZOLI PABLO ESTEBAN', 'AXEN S.R.L'," +
         " 'BRANDIE S.A', 'CAS TRADING & COMPANY SRL', 'CEMABESA S.A', 'CORONIL MARTHA BEATRIZ', 'COUGAR S.R.L.', 'DENTITE AROMATIQUE SA', 'DIFFUPAR S.A.', 'DUKAAN HOME', 'EKLIS SA', 'ESPALMA S.A'," +
         " 'FASHION COOK S.A.', 'LAS CASIANAS SRL', 'LENZA CLARA MARIA', 'LEXO S.A.', 'MARIA CAROLINA SCHALUM (LUIS&LEWIS)', 'MERCATOR SA', 'MISHKA SA', 'MOOVE SRL', 'PRINOX SRL', 'REVIFAS SA', " +
         "'ROBERTO MIGUEL ANGEL CROCCO', 'SUEÑO FUEGINO SA', 'TEKNO HOMES SA.', '') Order by T_LIBELLE", "T_LIBELLE", "T_TIERS");




            ViewBag.ShowEnvases = false;
            ViewBag.Rubros = dataAccess.getSelectItems("SELECT * FROM Rubros WHERE YX_CODE IN ('308', '309', '310') Order by YX_LIBELLE", "YX_LIBELLE", "YX_CODE");

            return View("IndexHogar");
        }



        [HttpPost]
        public ActionResult ExportToExcelMaison()
        {
            Response.ContentEncoding = Encoding.UTF8;

            Response.Charset = "UTF-8";
            DateTime now = DateTime.Now;
            string timeString = now.ToString("HHmmssfff");
            if (timeString.Length < 9)
                timeString = timeString.PadLeft(9, '0'); // Rellenar con ceros a la izquierda si es necesario


            string fileName = Request.Form["fileName"] + timeString + ".xlsx";
            string publicUrl = Request.Form["publicUrl"];
            // Define tu consulta SQL aquí


            string query = @"SELECT
							
								NombreMarcas.CC_LIBELLE AS NombreMarca,
								ARTICLE.GA_CODEARTICLE AS CodigoArticulo,
								ARTICLE.GA_CODEBARRE AS EAN13Codigo,
		
								Rubros.YX_LIBELLE AS Rubro,
								Article.GA_LIBELLE AS Producto,
							    NombreSucursales.ET_ETABLISSEMENT AS Tienda,
								NombreSucursales.ET_LIBELLE as SucDescripcion,
								
						
								ISNULL((SELECT TOP 1 Venta_Mes_En_Curso.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_En_Curso WHERE Venta_Mes_En_Curso.PK =
								CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)),0) AS VentaMesEnCurso,

								ISNULL((SELECT TOP 1 Stock1800.StockValido FROM compras.dbo.Stock1800 WHERE Stock1800.ARTICULO = Article.GA_CODEARTICLE and Stock1800.CODTIENDA = NombreSucursales.ET_ETABLISSEMENT AND Stock1800.CODTIENDA IN  (1800, 3000, 3001) ),0) AS SockFisico,
								ISNULL((SELECT  T0.AvgPrice FROM compras.dbo.T0 WHERE T0.ItemCode = Article.GA_CODEARTICLE ),0) AS Costos,
								ISNULL((SELECT TOP 1 Venta_Semana_1.Venta_Semana_En_Curso FROM compras.dbo.Venta_Semana_1 WHERE  Venta_Semana_1.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)),0) AS VentaS1,		
								ISNULL((SELECT TOP 1 Venta_Semana_2.Venta_Semana_En_Curso FROM compras.dbo.Venta_Semana_2 WHERE  Venta_Semana_2.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)),0) AS VentaS2,	
								ISNULL((SELECT TOP 1 Venta_Semana_3.Venta_Semana_En_Curso FROM compras.dbo.Venta_Semana_3 WHERE  Venta_Semana_3.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)),0) AS VentaS3,
								ISNULL((SELECT TOP 1 Venta_Semana_4.Venta_Semana_En_Curso FROM compras.dbo.Venta_Semana_4 WHERE  Venta_Semana_4.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)),0) AS VentaS4,

								ISNULL((SELECT TOP 1 Venta_Mes_1.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_1 WHERE Venta_Mes_1.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)),0) AS Venta1,
								ISNULL((SELECT TOP 1 Venta_Mes_2.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_2 WHERE Venta_Mes_2.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)),0) AS Venta2,
								ISNULL((SELECT TOP 1 Venta_Mes_3.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_3 WHERE Venta_Mes_3.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)),0) AS Venta3,
								(SELECT TOP 1 DatoProveedores.T_LIBELLE FROM compras.dbo.DatoProveedores WHERE DatoProveedores.T_TIERS = Article.GA_FOURNPRINC) AS ProveedorNombre,
								ISNULL((SELECT TOP 1 Pvp.GF_PRIXUNITAIRE FROM compras.dbo.Pvp WHERE Pvp.GF_ARTICLE = Article.GA_ARTICLE),0) AS Pvp,
								CASE 
									WHEN (
										ISNULL((SELECT TOP 1 Venta_Mes_13.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_13 WHERE Venta_Mes_13.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)), 0) +
										ISNULL((SELECT TOP 1 Venta_Mes_14.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_14 WHERE Venta_Mes_14.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)), 0) +
										ISNULL((SELECT TOP 1 Venta_Mes_15.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_15 WHERE Venta_Mes_15.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)), 0)
									) = 0 THEN 0
									ELSE
										CAST(ROUND(ISNULL((SELECT TOP 1 Venta_Mes_12.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_12 WHERE Venta_Mes_12.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)), 0) /
										((ISNULL((SELECT TOP 1 Venta_Mes_13.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_13 WHERE Venta_Mes_13.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)), 0) +
										ISNULL((SELECT TOP 1 Venta_Mes_14.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_14 WHERE Venta_Mes_14.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)), 0) +
										ISNULL((SELECT TOP 1 Venta_Mes_15.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_15 WHERE Venta_Mes_15.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)), 0))/3.0) * 
										((ISNULL((SELECT TOP 1 Venta_Mes_1.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_1 WHERE Venta_Mes_1.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)), 0) +
										ISNULL((SELECT TOP 1 Venta_Mes_2.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_2 WHERE Venta_Mes_2.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)), 0) +
										ISNULL((SELECT TOP 1 Venta_Mes_3.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_3 WHERE Venta_Mes_3.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)), 0))/3.0),0) AS INT)
									END AS VentaEstimada,
								(CASE
									WHEN (SELECT TOP 1 Rotacion.ROTACION FROM compras.dbo.Rotacion WHERE Rotacion.SKU = Article.GA_CODEARTICLE) ='A' THEN 3
									WHEN (SELECT TOP 1 Rotacion.ROTACION FROM compras.dbo.Rotacion WHERE Rotacion.SKU = Article.GA_CODEARTICLE) ='B' THEN 2
									ELSE 1
								END) * (CASE 
									WHEN (
										ISNULL((SELECT TOP 1 Venta_Mes_13.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_13 WHERE Venta_Mes_13.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)), 0) +
										ISNULL((SELECT TOP 1 Venta_Mes_14.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_14 WHERE Venta_Mes_14.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)), 0) +
										ISNULL((SELECT TOP 1 Venta_Mes_15.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_15 WHERE Venta_Mes_15.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)), 0)
									) = 0 THEN 0
									ELSE
										CAST(ROUND(ISNULL((SELECT TOP 1 Venta_Mes_12.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_12 WHERE Venta_Mes_12.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)), 0) /
										((ISNULL((SELECT TOP 1 Venta_Mes_13.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_13 WHERE Venta_Mes_13.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)), 0) +
										ISNULL((SELECT TOP 1 Venta_Mes_14.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_14 WHERE Venta_Mes_14.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)), 0) +
										ISNULL((SELECT TOP 1 Venta_Mes_15.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_15 WHERE Venta_Mes_15.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)), 0))/3.0) * 
										((ISNULL((SELECT TOP 1 Venta_Mes_1.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_1 WHERE Venta_Mes_1.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)), 0) +
										ISNULL((SELECT TOP 1 Venta_Mes_2.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_2 WHERE Venta_Mes_2.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)), 0) +
										ISNULL((SELECT TOP 1 Venta_Mes_3.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_3 WHERE Venta_Mes_3.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)), 0))/3.0),0) AS INT)
									END) -
									ISNULL((SELECT TOP 1 Stock_Fisico.Stock_Fisico FROM compras.dbo.Stock_Fisico WHERE Stock_Fisico.PK = CONCAT(Article.GA_ARTICLE, NombreSucursales.ET_ETABLISSEMENT)),0) -
									ISNULL((SELECT TOP 1 OCPendientes.OCs_Pendientes FROM compras.dbo.OCPendientes WHERE OCPendientes.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)),0) -
									ISNULL((SELECT TOP 1 Pendientes.UN FROM compras.dbo.Pendientes WHERE Pendientes.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)),0) -
									ISNULL((SELECT TOP 1 Preparacion.UN FROM compras.dbo.Preparacion WHERE Preparacion.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)),0) -
									ISNULL((SELECT TOP 1 PedidosTransito.Pedidos_En_Transito FROM compras.dbo.PedidosTransito WHERE PedidosTransito.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)),0) AS Sugerido
							FROM
								compras.dbo.NombreSucursales, compras.dbo.CodigoMarca
								INNER JOIN compras.dbo.NombreMarcas ON CodigoMarca.GA2_FAMILLENIV4 = NombreMarcas.CC_CODE
								INNER JOIN compras.dbo.ARTICLE ON CodigoMarca.GA2_CODEARTICLE = ARTICLE.GA_CODEARTICLE
								INNER JOIN compras.dbo.Rubros ON Rubros.YX_CODE = Article.GA_LIBREART2
								INNER JOIN compras.dbo.Envase ON Envase.YX_CODE = Article.GA_LIBREART5
								INNER JOIN compras.dbo.DatoProveedores on DatoProveedores.T_TIERS = Article.GA_FOURNPRINC
							WHERE compras.dbo.CodigoMarca.GA2_FAMILLENIV4 <>''" + Request.Form["conditions"] +
                            " ORDER BY NombreSucursales.ET_ETABLISSEMENT";


            string filePath = Server.MapPath("~/Files/" + fileName);
            string localhostPath = publicUrl + "/Files/" + fileName;
            dataAccess.ExportToExcelSheets(query, filePath);
            var fileData = new
            {
                tempFile = Url.Content(localhostPath),
                fileName = Request.Form["fileName"] + ".xlsx"
            };

            return Json(fileData);
        }

    }
}