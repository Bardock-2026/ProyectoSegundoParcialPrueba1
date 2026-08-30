using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoSegundoParcialPrueba1.Models.IA
{
    public class RespuestaIA
    {
        // ✅ Atributos privados
        private string _texto;
        private string _modeloUtilizado;
        private DateTime _fecha;

        // ✅ Propiedades públicas con validación
        public string Texto
        {
            get { return _texto; }
            set
            {
                if (value == null || value == "")
                {
                    throw new Exception("El texto de la respuesta no puede estar vacío.");
                }
                _texto = value;
            }
        }

        public string ModeloUtilizado
        {
            get { return _modeloUtilizado; }
            set
            {
                if (value == null || value == "")
                {
                    throw new Exception("El modelo utilizado no puede estar vacío.");
                }
                _modeloUtilizado = value;
            }
        }

        public DateTime Fecha
        {
            get { return _fecha; }
            set
            {
                if (value == null)
                {
                    throw new Exception("La fecha de la respuesta no puede ser nula.");
                }
                _fecha = value;
            }
        }

        // ✅ Constructor
        public RespuestaIA(string texto, string modeloUtilizado)
        {
            Texto = texto;
            ModeloUtilizado = modeloUtilizado;
            Fecha = DateTime.Now;
        }
    }
}

