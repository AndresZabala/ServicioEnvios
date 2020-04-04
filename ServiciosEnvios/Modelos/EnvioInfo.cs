using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServiciosEnvios.Modelos
{
    [DataContract]
    public class EnvioInfo 
    {
        [DataMember]
        public int COD_GUIA { get; set; }
        [DataMember]
        public Cliente CEDULA_EMI { get; set; }
        [DataMember]
        public Cliente CEDULA_DES { get; set; }
        [DataMember]
        public Ciudad CIUD_ORIG { get; set; }
        [DataMember]
        public Ciudad CIUD_DEST { get; set; }
        [DataMember]
        public int PESO { get; set; }
        [DataMember]
        public int VALOR_ASEG { get; set; }
        [DataMember]
        public int PRECIOKL { get; set; }
        [DataMember]
        public int VALOR_ENVI { get; set; }

        [DataMember]
        public Estados COD_ESTADO { get; set; }
    }
}