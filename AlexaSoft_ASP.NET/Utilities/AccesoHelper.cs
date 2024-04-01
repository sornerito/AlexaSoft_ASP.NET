using Newtonsoft.Json;

namespace AlexaSoft_ASP.NET.Utilities
{
    public class AccesoHelper
    {
        public static bool TienePermiso(HttpContext httpContext, string permisoRequerido)
        {
            var permisosJson = httpContext.Session.GetString("Permisos");
            var EstadoUsuario = httpContext.Session.GetString("EstadoUsuario");
            var EstadoRol = httpContext.Session.GetString("EstadoRol");
            if (EstadoUsuario == "Activo" && EstadoRol =="Activo") {
                if (!string.IsNullOrEmpty(permisosJson))
                {
                    var permisos = JsonConvert.DeserializeObject<List<dynamic>>(permisosJson);
                    return permisos.Any(p => p.Nombre == permisoRequerido);
                }
            }
            
            return false;
        }
    }
}
