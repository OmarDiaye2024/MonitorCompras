using comprasApp.DataAccess;
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
	public class HomeController : Controller
	{
		private DataAccessLayer dataAccess = new DataAccessLayer();


		public ActionResult Index()
		{


			ViewBag.Marcas = dataAccess.getSelectItems("SELECT * FROM NombreMarcas WHERE CC_CODE NOT IN ('319', '320', '322', '343', '332', '335', '316', '312', '318', '321', '323'," +
				" '351', '338', '346', '329', '352', '347', '336','341', '324', '350', '360', '327', '340', '342', '337', '353','333', '354') Order by CC_LIBELLE", "CC_LIBELLE", "CC_CODE");

			ViewBag.Tiendas = dataAccess.getSelectItems("SELECT * FROM NombreSucursales WHERE ET_ETABLISSEMENT NOT IN ('3000', '3001','1800') Order by ET_LIBELLE", "ET_LIBELLE", "ET_ETABLISSEMENT");

			ViewBag.Proveedores = dataAccess.getSelectItems("SELECT * FROM NombreProveedores WHERE T_LIBELLE NOT IN ('A M R S.A.', 'ARRUZZOLI PABLO ESTEBAN', 'AXEN S.R.L'," +
			" 'BRANDIE S.A', 'CAS TRADING & COMPANY SRL', 'CEMABESA S.A', 'CORONIL MARTHA BEATRIZ', 'COUGAR S.R.L.', 'DENTITE AROMATIQUE SA', 'DIFFUPAR S.A.', 'DUKAAN HOME', 'EKLIS SA', 'ESPALMA S.A'," +
			" 'FASHION COOK S.A.', 'LAS CASIANAS SRL', 'LENZA CLARA MARIA', 'LEXO S.A.', 'MARIA CAROLINA SCHALUM (LUIS&LEWIS)', 'MERCATOR SA', 'MISHKA SA', 'MOOVE SRL', 'PRINOX SRL', 'REVIFAS SA', " +
			"'ROBERTO MIGUEL ANGEL CROCCO', 'SUEÑO FUEGINO SA', 'TEKNO HOMES SA', 'VOLF S.A.') Order by T_LIBELLE", "T_LIBELLE", "T_TIERS");

			ViewBag.ShowEnvases = true;
			ViewBag.Envases = dataAccess.getSelectItems("SELECT * FROM Envase Order by YX_LIBELLE", "YX_LIBELLE", "YX_CODE");

			ViewBag.Rubros = dataAccess.getSelectItems("SELECT * FROM Rubros WHERE YX_CODE NOT IN('308','309','310') Order by YX_LIBELLE", "YX_LIBELLE", "YX_CODE");

			return View("IndexPerfumeria");
		}



        [HttpPost]
        public ActionResult ExportToExcel()
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
								CodigoMarca.GA2_FAMILLENIV4 AS CodigoMarca,
								NombreMarcas.CC_LIBELLE AS NombreMarca,
								ARTICLE.GA_CODEARTICLE AS CodigoArticulo,
								ARTICLE.GA_CODEBARRE AS EAN13Codigo,
								ARTICLE.GA_CHARLIBRE3 AS SKUCORTO,
								CONCAT(ARTICLE.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT) as Concatenado,
								Rubros.YX_LIBELLE AS Rubro,
								Article.GA_LIBELLE AS Producto,
							    NombreSucursales.ET_ETABLISSEMENT AS Tienda,
								NombreSucursales.ET_LIBELLE as SucDescripcion,
								Envase.YX_LIBELLE AS Envase,
								(SELECT TOP 1 Rotacion.ROTACION FROM compras.dbo.Rotacion WHERE Rotacion.SKU = Article.GA_CODEARTICLE) AS Rotacion,
								(SELECT TOP 1 BestSeller.Infaltable FROM compras.dbo.BestSeller WHERE BestSeller.CodigoArticulo = Article.GA_CODEARTICLE) AS Infaltable,
								ISNULL((SELECT TOP 1 StockProveedor.STOCK FROM compras.dbo.StockProveedor WHERE StockProveedor.SKU_CORTO = Article.GA_CHARLIBRE3),0) AS StockProveedor,
								ISNULL((SELECT TOP 1 Venta_Mes_En_Curso.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_En_Curso WHERE Venta_Mes_En_Curso.PK =
								CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)),0) AS VentaMesEnCurso,
								ISNULL((SELECT TOP 1 Stock_Fisico.Stock_Fisico FROM compras.dbo.Stock_Fisico WHERE Stock_Fisico.PK = CONCAT(Article.GA_ARTICLE, NombreSucursales.ET_ETABLISSEMENT)),0) AS StockFisico,
								ISNULL((SELECT TOP 1 OCPendientes.OCs_Pendientes FROM compras.dbo.OCPendientes WHERE OCPendientes.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)),0) AS OCPendientes,
								ISNULL((SELECT TOP 1 OC1201.UN FROM compras.dbo.OC1201 WHERE OC1201.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)),0) AS CantidadOCS1201,
								ISNULL((SELECT TOP 1 Pendientes.UN FROM compras.dbo.Pendientes WHERE Pendientes.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)),0) AS PedidosPendientes,
								ISNULL((SELECT TOP 1 Preparacion.UN FROM compras.dbo.Preparacion WHERE Preparacion.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)),0) AS PedidosEnPreparacion,
								ISNULL((SELECT TOP 1 PedidosTransito.Pedidos_En_Transito FROM compras.dbo.PedidosTransito WHERE PedidosTransito.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)),0) AS PedidosEnTransito,
								'' as Compra,
								

								ISNULL((SELECT TOP 1 SapStock.StockValido FROM compras.dbo.SapStock WHERE SapStock.ARTICULO = Article.GA_CODEARTICLE and SapStock.CODTIENDA = NombreSucursales.ET_ETABLISSEMENT AND SapStock.CODTIENDA != 1800 ),0) AS StockSAP,
										ISNULL((SELECT TOP 1 Venta_Mes_1.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_1 WHERE Venta_Mes_1.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)),0) AS Venta1,
								ISNULL((SELECT TOP 1 Venta_Mes_2.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_2 WHERE Venta_Mes_2.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)),0) AS Venta2,
								ISNULL((SELECT TOP 1 Venta_Mes_3.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_3 WHERE Venta_Mes_3.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)),0) AS Venta3,
								ISNULL((SELECT TOP 1 Venta_Mes_4.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_4 WHERE Venta_Mes_4.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)),0) AS Venta4,
								ISNULL((SELECT TOP 1 Venta_Mes_5.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_5 WHERE Venta_Mes_5.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)),0) AS Venta5,
								ISNULL((SELECT TOP 1 Venta_Mes_6.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_6 WHERE Venta_Mes_6.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)),0) AS Venta6,
								ISNULL((SELECT TOP 1 Venta_Mes_12.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_12 WHERE Venta_Mes_12.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)),0) AS Venta12,
								ISNULL((SELECT TOP 1 Venta_Mes_13.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_13 WHERE Venta_Mes_13.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)),0) AS Venta13,
								ISNULL((SELECT TOP 1 Venta_Mes_14.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_14 WHERE Venta_Mes_14.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)),0) AS Venta14,
								ISNULL((SELECT TOP 1 Venta_Mes_15.Venta_Mes_En_Curso FROM compras.dbo.Venta_Mes_15 WHERE Venta_Mes_15.PK = CONCAT(Article.GA_CODEARTICLE, NombreSucursales.ET_ETABLISSEMENT)),0) AS Venta15,
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
                 " order by NombreMarcas.CC_LIBELLE, Rubros.YX_LIBELLE,ARTICLE.GA_CODEARTICLE, Article.GA_LIBELLE, NombreSucursales.ET_ETABLISSEMENT";


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


  
