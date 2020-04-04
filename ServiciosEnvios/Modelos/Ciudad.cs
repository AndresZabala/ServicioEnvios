using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServiciosEnvios.Modelos
{
    [DataContract]
    public class Ciudad
    {
        [DataMember]
        public int COD_CIUD { get; set; }
        [DataMember]
        public string NOMBRE { get; set; }
    }
}