using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class UsuarioConPersona
    {
        
            public int IdUsuario { get; set; }
            public int IdPersona { get; set; }
            public string Username { get; set; }
            public bool Activo { get; set; }  
            public string PersonaNombre { get; set; }
            public string RolNombre { get; set; }
            public string SucursalNombre { get; set; }
            public int IdRol { get; set; }
            
        }
    
}
