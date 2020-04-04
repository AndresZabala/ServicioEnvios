using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServiciosEnvios.Modelos
{

    [DataContract]
    public class Estados
    {
        [DataMember]
        public int COD_ESTADO { get; set; }
        [DataMember]
        public string DESCRIPCION { get; set; }
    }
}