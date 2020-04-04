using ServiciosEnvios.Modelos;
using ServiciosEnvios.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServiciosEnvios
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IService
    {
        //Solicitud de insertar Cliente en la base de datos. Este metodo al implementarlo
        //en la clase servirá tambien para actualizar un cliente.
        [OperationContract]
        [WebInvoke (Method = "POST", UriTemplate = "/crearCliente", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Mensajes crearCliente(CLIENTE cliente);

        //Solicitud que busca un cliente en el sistema
        [OperationContract]
        [WebGet(UriTemplate = "/getCliente/{id}", ResponseFormat = WebMessageFormat.Json)]
        ModeCliente getCliente(string id);

        //Solicitud cambiar estado de un cliente
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/actualizarEstadoCliente", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ModelActualizaEstado actualizarEstadoCliente(EstadoActualiza estadoActualiza);

        //Solicitud de registro de la Mercancia
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/registrarEnvio", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ModelEnvioInfo registrarEnvioInfo(EnvioInfo objEnvioinfo);

        //Solicitud que retorna toda la informacion de la tabla pesos
        [OperationContract]
        [WebGet(UriTemplate = "/getPesos", ResponseFormat = WebMessageFormat.Json)]
        ModelPesos getPesos();

        //Solicitud que retorna toda la informacion de la tabla estados
        [OperationContract]
        [WebGet(UriTemplate = "/getEstados", ResponseFormat = WebMessageFormat.Json)]
        List<Estados> getEstados();

        //Solicitud que realiza la actualizacion del estado de envio
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "actualizarEstadoEnvio", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Mensajes actualizarEstadoEnvio(ActualizarEstadoEnvio objActualizaEstado);

        //Solicitud que hace la consulta sobre toda la informacion de un Envio por el codigo del envio
        [OperationContract]
        [WebGet(UriTemplate = "/obtenerEnvio/{id}", ResponseFormat = WebMessageFormat.Json)]
        ModelEnvioInfo obtenerEnvio(string id);
    }
}
