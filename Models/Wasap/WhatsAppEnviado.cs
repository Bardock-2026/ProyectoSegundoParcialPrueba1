using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoSegundoParcialPrueba1.Models.Wasap
{
    public class WhatsAppEnviado
    {
        public int Id { get; set; }
        public string NumeroDestino { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaEnvio { get; set; } // 👈 también aquí
    }
}