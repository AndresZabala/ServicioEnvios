using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServiciosEnvios.Modelos

{

    [DataContract]
    public class Clientes
    {
        [DataMember]
        public string CEDULA { get; set; }
        [DataMember]
        public string NOMBRES { get; set; }
        [DataMember]
        public string APELLIDOS { get; set; }
        [DataMember]
        public DateTime FECHA_NAC { get; set; }
        [DataMember]
        public string DIRECCION { get; set; }
        [DataMember]
        public string TELEFONO { get; set; }
        [DataMember]
        public string EMAIL { get; set; }
        [DataMember]
        public DateTime FECHA_REG { get; set; } = DateTime.Now;
        [DataMember]
        public int ESTADO { get; set; }
    }
}