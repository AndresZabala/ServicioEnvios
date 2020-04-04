using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServiciosEnvios.Utilidades
{
    [DataContract]
    public class ModelPesos

    {
        [DataMember]
        public bool error { get; set; }
        [DataMember]
        public string msj { get; set; }
        [DataMember]
        public List<ListModelPesos> content { get; set; }
    }
}