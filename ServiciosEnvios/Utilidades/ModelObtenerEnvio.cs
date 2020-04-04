using ServiciosEnvios.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServiciosEnvios.Utilidades
{
    [DataContract]
    public class ModelObtenerEnvio
    {
        [DataMember]
        public bool error { get; set; }
        [DataMember]
        public string msj { get; set; }
        [DataMember]
        public EnvioInfo content { get; set; }


    }
}