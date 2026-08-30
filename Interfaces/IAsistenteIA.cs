using ProyectoSegundoParcialPrueba1.Models.IA;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoSegundoParcialPrueba1.Interfaces
{
    public interface IAsistenteIA
    {
        Task<RespuestaIA> PreguntarAsync(PreguntaIA pregunta);
    }
}