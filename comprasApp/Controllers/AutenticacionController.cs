using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace comprasApp.Controllers
{
    public class AutenticacionController : Controller
    {
        //private string connectionString = "Data Source=DESKTOP-90QTNE5\\MSSQLSERVER01;Initial Catalog=compras;Integrated Security=True;Connect Timeout=900";
        private string connectionString = "Data Source=(local);Initial Catalog=compras;Integrated Security=True;Connect Timeout=900"; // Reemplaza con tu cadena de conexión
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string usuario, string clave)
        {
            string query = "SELECT COUNT(*) FROM usuarios WHERE usuario = @Usuario AND clave = @Clave;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Usuario", usuario);
                    command.Parameters.AddWithValue("@Clave", clave);

                    int count = (int)command.ExecuteScalar();

                    if (count == 1)
                    {
                        FormsAuthentication.SetAuthCookie(usuario, false);
                        return RedirectToAction("Index", "Filtro"); // Redirige a la página de inicio
                    }
                    else
                    {
                        // Si el inicio de sesión es incorrecto, establece un mensaje de alerta
                        TempData["ErrorMessage"] = "Usuario o contraseña incorrectos.";
                        return RedirectToAction("Login"); // Redirige de nuevo a la página de inicio de sesión
                    }
                }
             
            }
        }
      

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }

}