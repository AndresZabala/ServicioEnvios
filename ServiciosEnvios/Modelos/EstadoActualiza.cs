using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServiciosEnvios.Modelos
{
    [DataContract]
    public class EstadoActualiza
    {
        [DataMember]
        public string cedula { get; set; }
        [DataMember]
        public int estado { get; set; }
    }
}