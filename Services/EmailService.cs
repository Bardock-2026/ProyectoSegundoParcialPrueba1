using ProyectoSegundoParcialPrueba1.Models.Correo;
using ProyectoSegundoParcialPrueba1.Models.IA;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace ProyectoSegundoParcialPrueba1.Services
{
    public class EmailService
    {
        public void EnviarCorreo(string destinatario, string asunto, string cuerpo)
        {
            try
            {
                var smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new System.Net.NetworkCredential(
                        "Ferchoandres28@gmail.com",
                        "rvaz dpzl cadh kcaz" // 👈 tu contraseña de aplicación de Gmail
                    ),
                    EnableSsl = true
                };

                var mensaje = new MailMessage("Ferchoandres28@gmail.com", destinatario, asunto, cuerpo);
                smtp.Send(mensaje);

                Console.WriteLine("Correo enviado correctamente.");

                // Guardar en SQL
                using (var db = new ChatContext())
                {
                    var registro = new CorreoEnviado
                    {
                        Destinatario = destinatario,
                        Asunto = asunto,
                        Cuerpo = cuerpo
                    };
                    db.CorreosEnviados.Add(registro);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar correo: {ex.Message}");
            }
        }
    }
}