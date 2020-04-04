using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServiciosEnvios.Utilidades
{
    [DataContract]
    public class ModelActualizaEstado
    {
        [DataMember]
        public bool error { get; set; }
        [DataMember]    
        public string msj { get; set; }
    }
}