using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoSegundoParcialPrueba1.Models.IA
{
    public class PreguntaIA
    {
        // ✅ Atributos privados
        private string _cliente;
        private string _texto;
        private string _categoria; // Ej: "Habitacion", "Pago", "Consejo"

        // ✅ Propiedades públicas con validación
        public string Cliente
        {
            get { return _cliente; }
            set
            {
                if (value == null || value == "")
                {
                    throw new Exception("El nombre del cliente no puede estar vacío.");
                }
                _cliente = value;
            }
        }

        public string Texto
        {
            get { return _texto; }
            set
            {
                if (value == null || value == "")
                {
                    throw new Exception("La pregunta no puede estar vacía.");
                }
                _texto = value;
            }
        }

        public string Categoria
        {
            get { return _categoria; }
            set
            {
                if (value == null || value == "")
                {
                    throw new Exception("La categoría de la pregunta no puede estar vacía.");
                }
                _categoria = value;
            }
        }

        // ✅ Constructor
        public PreguntaIA(string cliente, string texto, string categoria)
        {
            Cliente = cliente;
            Texto = texto;
            Categoria = categoria;
        }
    }
}
