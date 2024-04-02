using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlexaSoft_ASP.NET.Models;
using NuGet.Protocol;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http;

namespace AlexaSoft_ASP.NET.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly AlexasoftContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuariosController(AlexasoftContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                string[] validDomains = { ".com", ".co", ".net", ".org", ".edu", ".gov" };
                bool isValidDomain = validDomains.Any(domain => addr.Host.EndsWith(domain));

                return addr.Address == email && isValidDomain;
            }
            catch
            {
                return false;
            }
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var alexasoftContext = _context.Usuarios.Include(u => u.IdRolNavigation);
            return View(await alexasoftContext.ToListAsync());
        }

        // GET: Usuarios/Login
        public async Task<IActionResult> Login()
        {
            
            return View();
        }

        // POST: Usuarios/Login
        [HttpPost]
        public async Task<IActionResult> Login(Usuario usuario)
        {
			if (!IsValidEmail(usuario.Correo))
			{
				ModelState.AddModelError("Correo", "El correo no es valido.");
				string script = @"
                <script>
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'El correo no es valido.'
                    });
                </script>";
				ViewData["CorreoNoValido"] = script;
				return View(usuario);
			}

			bool correoEnCliente = _context.Clientes.Any(cl => cl.Correo == usuario.Correo);
            bool correoEnUsuario = _context.Usuarios.Any(cl => cl.Correo == usuario.Correo);
            bool correoEnColaboradores = _context.Colaboradores.Any(cl => cl.Correo == usuario.Correo);

            if (!correoEnCliente && !correoEnUsuario && !correoEnColaboradores)
            {
                ModelState.AddModelError("Correo", "El correo no se encuentra registrado.");
                string script = @"
                <script>
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'El correo no se encuentra registrado.'
                    });
                </script>";
                ViewData["CorreoNoEncontrado"] = script;
                return View(usuario);
            }
            else
            {
                bool contrasenaCoincide = false;
                

                if (correoEnCliente)
                {
                    var usuarioIngresado = await _context.Clientes.Include(a => a.IdRolNavigation).ThenInclude(h => h.RolesPermisos).ThenInclude(a => a.IdPermisoNavigation).FirstOrDefaultAsync(r => r.Correo == usuario.Correo);
                    if (usuarioIngresado.Contrasena == usuario.Contrasena) { 
                        contrasenaCoincide = true;

                        var httpContext = _httpContextAccessor.HttpContext;
                        if (httpContext != null && httpContext.Session != null)
                        {
                            httpContext.Session.SetInt32("IdUsuario", usuarioIngresado.IdCliente);

                            httpContext.Session.SetString("Rol", usuarioIngresado.IdRolNavigation.Nombre);
                            httpContext.Session.SetString("EstadoUsuario", usuarioIngresado.Estado);
                            httpContext.Session.SetString("EstadoRol", usuarioIngresado.IdRolNavigation.Estado);
                            var listaPermisos = usuarioIngresado.IdRolNavigation.RolesPermisos
                            .Select(rp => new { Nombre = rp.IdPermisoNavigation.Nombre })
                            .ToList();
                            var listaPermisosJson = JsonConvert.SerializeObject(listaPermisos);
                            httpContext.Session.SetString("Permisos", listaPermisosJson);
                        }
                        
                    }
                }
                else if (correoEnUsuario)
                {
                    var usuarioIngresado = await _context.Usuarios.Include(a => a.IdRolNavigation).ThenInclude(h => h.RolesPermisos).ThenInclude(a => a.IdPermisoNavigation).FirstOrDefaultAsync(r => r.Correo == usuario.Correo);
                    if (usuarioIngresado.Contrasena == usuario.Contrasena)
                    {
                        contrasenaCoincide = true;

                        var httpContext = _httpContextAccessor.HttpContext;
                        if (httpContext != null && httpContext.Session != null)
                        {
                            httpContext.Session.SetInt32("IdUsuario", usuarioIngresado.IdUsuario);

                            httpContext.Session.SetString("Rol", usuarioIngresado.IdRolNavigation.Nombre);
                            httpContext.Session.SetString("EstadoUsuario", usuarioIngresado.Estado);
                            httpContext.Session.SetString("EstadoRol", usuarioIngresado.IdRolNavigation.Estado);
                            var listaPermisos = usuarioIngresado.IdRolNavigation.RolesPermisos
                            .Select(rp => new { Nombre = rp.IdPermisoNavigation.Nombre })
                            .ToList();
                            var listaPermisosJson = JsonConvert.SerializeObject(listaPermisos);
                            httpContext.Session.SetString("Permisos", listaPermisosJson);
                        }
                    }

                    }
                    else if (correoEnColaboradores)
                {
                    var usuarioIngresado = await _context.Colaboradores.Include(a => a.IdRolNavigation).ThenInclude(h => h.RolesPermisos).ThenInclude(a => a.IdPermisoNavigation).FirstOrDefaultAsync(r => r.Correo == usuario.Correo);
                    if (usuarioIngresado.Contrasena == usuario.Contrasena)
                    {
                        contrasenaCoincide = true;

                        var httpContext = _httpContextAccessor.HttpContext;
                        if (httpContext != null && httpContext.Session != null)
                        {
                            httpContext.Session.SetInt32("IdUsuario", usuarioIngresado.IdColaborador);

                            httpContext.Session.SetString("Rol", usuarioIngresado.IdRolNavigation.Nombre);
                            httpContext.Session.SetString("EstadoUsuario", usuarioIngresado.Estado);
                            httpContext.Session.SetString("EstadoRol", usuarioIngresado.IdRolNavigation.Estado);
                            var listaPermisos = usuarioIngresado.IdRolNavigation.RolesPermisos
                            .Select(rp => new { Nombre = rp.IdPermisoNavigation.Nombre })
                            .ToList();
                            var listaPermisosJson = JsonConvert.SerializeObject(listaPermisos);
                            httpContext.Session.SetString("Permisos", listaPermisosJson);
                            httpContext.Session.SetString("UsuarioAutenticado", "true");
                        }
                        
                    }
                }
                
                if (!contrasenaCoincide)
                {
                    ModelState.AddModelError("Contrasena", "La contraseña es incorrecta.");
                    string script = @"
                <script>
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'La contraseña es incorrecta.'
                    });
                </script>";
                    ViewData["ContrasenaIncorrecta"] = script;
                    return View(usuario);
                }
                correoEnCliente = false;
                correoEnUsuario = false;
                correoEnColaboradores = false;
                return RedirectToAction("Index", "Home");


            }

        }

        // GET: Usuarios/Registro
        public IActionResult Registro()
        {
            return View();
        }

        

        // POST: Usuarios/Registro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro([Bind("IdUsuario,Nombre,Correo,Telefono,Instagram,Contrasena,Estado,FechaInteraccion,IdRol")] Usuario usuario)
        {
            if (!IsValidEmail(usuario.Correo))
            {
                ModelState.AddModelError("Correo", "El correo no es valido.");
                string script = @"
                <script>
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'El correo no es valido.'
                    });
                </script>";
                ViewData["CorreoNoValido"] = script;
                return View(usuario);
            }

            bool correoExistenteUsuarios = _context.Usuarios.Any(u => u.Correo == usuario.Correo);
            bool correoExistenteColaboradores = _context.Colaboradores.Any(c => c.Correo == usuario.Correo);
            bool correoExistenteClientes = _context.Clientes.Any(cl => cl.Correo == usuario.Correo);

            bool telefonoExistenteUsuarios = _context.Usuarios.Any(u => u.Telefono == usuario.Telefono);
            bool telefonoExistenteColaboradores = _context.Colaboradores.Any(c => c.Telefono == usuario.Telefono);
            bool telefonoExistenteClientes = _context.Clientes.Any(cl => cl.Telefono == usuario.Telefono);

            if (correoExistenteUsuarios || correoExistenteColaboradores || correoExistenteClientes)
            {
                ModelState.AddModelError("Correo", "El correo ya está registrado.");
                string script = @"
                <script>
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'El correo ya está registrado.'
                    });
                </script>";
                ViewData["CorreoYaRegistrado"] = script;
                return View(usuario);
            }
            else if (telefonoExistenteUsuarios || telefonoExistenteColaboradores || telefonoExistenteClientes)
            {
                ModelState.AddModelError("Telefono", "El teléfono ya está registrado.");
                string script = @"
                <script>
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'El Telefono ya está registrado.'
                    });
                </script>";
                ViewData["TelefonoYaRegistrado"] = script;
                return View(usuario);
            }


            // Crear un nuevo objeto Cliente 
            var cliente = new Cliente
            {
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
                Telefono = usuario.Telefono,
                Instagram = usuario.Instagram != null ? usuario.Instagram : null,
                Contrasena = usuario.Contrasena,
                Estado = "Activo",
                FechaInteraccion = DateTime.Now,
                IdRol = 2
            };

            // Agregar el nuevo cliente a la tabla Clientes
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            //Redirigir al login para que se loguee
            return RedirectToAction(nameof(Login));
        }

        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        // GET: Usuarios/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Usuarios == null)
        //    {
        //        return NotFound();
        //    }

        //    var usuario = await _context.Usuarios.FindAsync(id);
        //    if (usuario == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "IdRol", usuario.IdRol);
        //    return View(usuario);
        //}

        //// POST: Usuarios/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,Nombre,Correo,Telefono,Instagram,Contrasena,Estado,FechaInteraccion,IdRol")] Usuario usuario)
        //{
        //    if (id != usuario.IdUsuario)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(usuario);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!UsuarioExists(usuario.IdUsuario))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "IdRol", usuario.IdRol);
        //    return View(usuario);
        //}

        //// GET: Usuarios/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Usuarios == null)
        //    {
        //        return NotFound();
        //    }

        //    var usuario = await _context.Usuarios
        //        .Include(u => u.IdRolNavigation)
        //        .FirstOrDefaultAsync(m => m.IdUsuario == id);
        //    if (usuario == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(usuario);
        //}

        //// POST: Usuarios/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Usuarios == null)
        //    {
        //        return Problem("Entity set 'AlexasoftContext.Usuarios'  is null.");
        //    }
        //    var usuario = await _context.Usuarios.FindAsync(id);
        //    if (usuario != null)
        //    {
        //        _context.Usuarios.Remove(usuario);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool UsuarioExists(int id)
        //{
        //  return (_context.Usuarios?.Any(e => e.IdUsuario == id)).GetValueOrDefault();
        //}
    }
}
