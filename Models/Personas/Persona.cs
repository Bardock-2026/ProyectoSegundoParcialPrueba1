using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoSegundoParcialPrueba1.Models.Personas
{
    public class Persona
    {
        // --- CAMPOS PRIVADOS ---
        private string nombre;
        private string cedula;
        private string telefono;
        private string email;
        private string ciudad;

        // --- PROPIEDADES ---
        public int Id { get; set; } // SQL lo asigna automáticamente

        public string Nombre
        {
            get => nombre;
            set
            {
                if (value == null || value == "")
                    throw new Exception("El nombre no puede estar vacío.");
                nombre = value;
            }
        }

        public string Cedula
        {
            get => cedula;
            set
            {
                if (value == null || value == "" || value.Length != 10)
                    throw new Exception("La cédula debe tener exactamente 10 dígitos.");
                cedula = value;
            }
        }

        public string Telefono
        {
            get => telefono;
            set
            {
                if (value == null || value == "" || value.Length < 7)
                    throw new Exception("El teléfono debe tener al menos 7 dígitos.");
                telefono = value;
            }
        }

        public string Email
        {
            get => email;
            set
            {
                if (value == null || value == "" || !value.Contains("@"))
                    throw new Exception("El email no es válido.");
                email = value;
            }
        }

        public string Ciudad
        {
            get => ciudad;
            set
            {
                if (value == null || value == "")
                    throw new Exception("La ciudad no puede estar vacía.");
                ciudad = value;
            }
        }

        // --- CONSTRUCTOR VACÍO (EF Core) ---
        public Persona() { }

        // --- CONSTRUCTOR CON PARÁMETROS ---
        public Persona(string nombre, string cedula, string telefono, string email, string ciudad)
        {
            this.Nombre = nombre;
            this.Cedula = cedula;
            this.Telefono = telefono;
            this.Email = email;
            this.Ciudad = ciudad;
        }
    }
}