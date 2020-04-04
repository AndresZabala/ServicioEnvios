using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServiciosEnvios.Modelos
{
    [DataContract]
    public class Pesos
    {
        [DataMember]
        public int CODIGO { get; set; }
        [DataMember]
        public int CIUD_ORIG { get; set; }
        [DataMember]
        public int CIUD_DEST { get; set; }
        [DataMember]
        public int PRECIOKL { get; set; } 
    }
}