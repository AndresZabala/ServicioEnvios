using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServiciosEnvios.Utilidades
{

    [DataContract]
    public class Mensajes
    {

        [DataMember]
        private bool error { get; set; } = false;
        [DataMember]
        private string msj { get; set; }

        public Mensajes exitoso(string mensaje)
        {
            return new Mensajes
            {
                error = false,
                msj = mensaje
            };
        }

        public Mensajes errores(string mensaje)
        {
           return new Mensajes
            {
                error = true,
                msj = mensaje
            };
        }

    }
}