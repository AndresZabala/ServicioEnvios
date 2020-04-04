using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServiciosEnvios.Modelos
{

    [DataContract]
    public class Envios
    {
        [DataMember]
        public int COD_GUIA { get; set; }
        [DataMember]
        public int CEDULA_EMI { get; set; }
        [DataMember]
        public int CEDULA_DEST { get; set; }
        [DataMember]
        public int PESO { get; set; }
        [DataMember]
        public int VALOR_ASEG { get; set; }
        [DataMember]
        public int PRECIOKL { get; set; }
        [DataMember]
        public int VALOR_ENVI { get; set; }
        [DataMember]
        public int COD_ESTADO { get; set; }
    }
}