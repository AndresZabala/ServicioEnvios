using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServiciosEnvios.Utilidades
{
    [DataContract]
    public class ActualizarEstadoEnvio
    {
        [DataMember]
        public int COD_GUIA { get; set; }
        [DataMember]
        public int COD_ESTADO { get; set; }
    }
}