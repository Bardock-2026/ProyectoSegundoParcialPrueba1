using ProyectoSegundoParcialPrueba1.Interfaces;
using ProyectoSegundoParcialPrueba1.Models.IA;
using OpenAI;
using OpenAI.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoSegundoParcialPrueba1.Services
{
    public class OpenAIService : IAsistenteIA
    {
#pragma warning disable OPENAI001
        private readonly ResponsesClient _cliente;
        private readonly string _modelo;

        public OpenAIService(string modelo)
        {
            if (modelo == null || modelo == "")
            {
                throw new Exception("El nombre del modelo no puede estar vacío.");
            }

            _cliente = new ResponsesClient("tu api key"); // reemplaza con tu key
            _modelo = modelo;
        }

        public async Task<RespuestaIA> PreguntarAsync(PreguntaIA pregunta)
        {
            if (pregunta == null)
            {
                throw new Exception("La pregunta no puede ser nula.");
            }

            using (var db = new ChatContext())
            {
                // 1. Buscar si ya existe la pregunta en BD
                var existente = db.Conversaciones
                                  .FirstOrDefault(c => c.Pregunta == pregunta.Texto);

                if (existente != null)
                {
                    string textoRespuesta = $"La pregunta ya existe en la base de datos.\n" +
                                            $"Respuesta previa: {existente.Respuesta}\n" +
                                            $"Modelo: {_modelo}\n" +
                                            $"Fecha guardada: {existente.Fecha}";

                    return new RespuestaIA(textoRespuesta, _modelo);
                }

                // 2. Si no existe, llamar a OpenAI
                string instrucciones = $"Eres un asistente de IA para un sistema de reservas de hotel. " +
                                       $"El cliente se llama {pregunta.Cliente}. " +
                                       $"Categoría: {pregunta.Categoria}. " +
                                       $"Pregunta: {pregunta.Texto}. " +
                                       $"Responde de manera clara y útil.";

                ResponseResult resultado = await _cliente.CreateResponseAsync(_modelo, instrucciones);
                string textoRespuestaGenerada = resultado.GetOutputText();

                // 3. Guardar en la BD
                var nueva = new Conversacion
                {
                    Cliente = pregunta.Cliente,
                    Categoria = pregunta.Categoria,
                    Pregunta = pregunta.Texto,
                    Respuesta = textoRespuestaGenerada,
                    Fecha = DateTime.Now
                };

                db.Conversaciones.Add(nueva);
                db.SaveChanges();

                return new RespuestaIA(textoRespuestaGenerada, _modelo);
            }
        }
    }
}