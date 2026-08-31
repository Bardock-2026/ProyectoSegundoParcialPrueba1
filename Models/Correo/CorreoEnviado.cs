using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoSegundoParcialPrueba1.Models.Correo
{
    public class CorreoEnviado
    {
        public int Id { get; set; }
        public string Destinatario { get; set; }
        public string Asunto { get; set; }
        public string Cuerpo { get; set; }
        public DateTime FechaEnvio { get; set; } // 👈 debe estar aquí
    }
}