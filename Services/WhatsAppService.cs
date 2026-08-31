using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ProyectoSegundoParcialPrueba1.Models.Correo;
using ProyectoSegundoParcialPrueba1.Models.IA;
using ProyectoSegundoParcialPrueba1.Models.Wasap;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mail;
using System.Text;

namespace ProyectoSegundoParcialPrueba1.Services
{
    public class WhatsAppService
    {
        public void EnviarWhatsApp(string numeroLocal, string mensaje)
        {
            try
            {
                // Convertir número local (ej: 0995839776) a internacional
                string numeroInternacional = ConvertirNumero(numeroLocal);

                string url = $"https://wa.me/{numeroInternacional}?text={Uri.EscapeDataString(mensaje)}";

                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });

                Console.WriteLine($"WhatsApp abierto para {numeroInternacional}, mensaje listo para enviar.");

                // Guardar en SQL
                using (var db = new ChatContext())
                {
                    var registro = new WhatsAppEnviado
                    {
                        NumeroDestino = numeroLocal,   // guardamos el número tal como lo ingresaste
                        Mensaje = mensaje,
                        FechaEnvio = DateTime.Now
                    };
                    db.WhatsAppsEnviados.Add(registro);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al abrir WhatsApp: {ex.Message}");
            }
        }

        private string ConvertirNumero(string numeroLocal)
        {
            if (numeroLocal.StartsWith("0"))
            {
                numeroLocal = numeroLocal.Substring(1);
            }
            return "593" + numeroLocal;
        }
    }
}