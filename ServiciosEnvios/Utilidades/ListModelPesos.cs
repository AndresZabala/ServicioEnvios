using ServiciosEnvios.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServiciosEnvios.Utilidades
{
    [DataContract]
    public class ListModelPesos
    {
        [DataMember]
        public int CODIGO { get; set; }
        [DataMember]
        public Ciudad CIUDAD_ORIG { get; set; }
        [DataMember]
        public Ciudad CIUDAD_DEST { get; set; }
        [DataMember]
        public int PRECIOKL { get; set; }
    }
}