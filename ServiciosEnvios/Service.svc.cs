using ServiciosEnvios.Modelos;
using ServiciosEnvios.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiciosEnvios
{

    public class Service : IService
    {
        //instancia de la Conexion con Base de datos utilizando EntityFramework
        enviosEntidades bd = new enviosEntidades();
        Util utilidades = new Util();
        Mensajes mensaje = new Mensajes();


        /*
         * 
         * METODOS "CLIENTES"
         * 
         */

        //metodo que permite crear y actualizar cliente. 
        public Mensajes crearCliente(CLIENTE cliente)
        {
            CLIENTE efCliente = bd.CLIENTE.Find(cliente.CEDULA);
            try
            {
                if (efCliente == null)
                {
                    string date = DateTime.Now.ToString();
                    string fechaRegistro = Convert.ToDateTime(date).ToString("yyyy-MM-dd");
                    //No se encontró un cliente por lo tante se puede ingresar
                    cliente.FECHA_REG = fechaRegistro;
                    bd.CLIENTE.Add(cliente);
                    bd.SaveChanges();
                    return mensaje.exitoso("Cliente Registrado Exitosamente.");
                }
                else
                {
                    //Se actualiza el cliente dado que lo encontro
                    cliente.FECHA_REG = efCliente.FECHA_REG;
                    bd.Entry(efCliente).CurrentValues.SetValues(cliente);
                    bd.SaveChanges();
                    return mensaje.exitoso("Cliente Actualizado Exitosamente.");
                }
            }
            catch
            {
                return mensaje.errores("Error Inesperado !!");
            }
        }

        public ModeCliente getCliente(string cedula)
        {
            try
            {
                var objResult = bd.CLIENTE.Find(cedula);
                if (objResult != null)
                {
                    //Encontro al cliente
                    return new ModeCliente
                    {
                        error = false,
                        msj = "Cliente encontrado.",
                        cliente = new Cliente
                        {
                            CEDULA = objResult.CEDULA,
                            NOMBRES = objResult.NOMBRES,
                            APELLIDOS = objResult.APELLIDOS,
                            FECHA_NAC = objResult.FECHA_NAC,
                            DIRECCION = objResult.DIRECCION,
                            TELEFONO = objResult.TELEFONO,
                            EMAIL = objResult.EMAIL,
                            FECHA_REG = objResult.FECHA_REG,
                            ESTADO = objResult.ESTADO
                        }
                    };
                }
                else
                {
                    return new ModeCliente
                    {
                        error = true,
                        msj = "Cliente no se encuentra registrado.",
                        cliente = null
                    };
                }
            }
            catch
            {
                return new ModeCliente
                {
                    error = true,
                    msj = "No se logor consultar el cliente, intente de nuevo.",
                    cliente = null
                };
            }

        }

        public ModelActualizaEstado actualizarEstadoCliente(EstadoActualiza estadoActualiza)
        {
            try
            {
                var objEstado = bd.CLIENTE.Find(estadoActualiza.cedula);
                if (objEstado != null)
                {
                    //Encontro un cliente se actualiza el estado
                    objEstado.ESTADO = estadoActualiza.estado;
                    bd.SaveChanges();

                    return new ModelActualizaEstado
                    {
                        error = false,
                        msj = "Estado Actualizado"
                    };
                }
                else
                {
                    return new ModelActualizaEstado
                    {
                        error = true,
                        msj = "Estado no esta Actualizado",
                    };
                }
            }
            catch
            {
                return new ModelActualizaEstado
                {
                    error = true,
                    msj = "Error Intente de nuevo",
                };
            }

        }

        /*
         * 
         * METODOS "ENVIOS"
         * 
         */
        public ModelEnvioInfo registrarEnvioInfo(EnvioInfo objEnvioinfo)
        {
            try
            {
                if (objEnvioinfo != null)
                {
                    var objEnvio = new ENVIOS
                    {
                        CEDULA_EMI = objEnvioinfo.CEDULA_EMI.CEDULA,
                        CEDULA_DES = objEnvioinfo.CEDULA_DES.CEDULA,
                        CIUD_ORIG = objEnvioinfo.CIUD_ORIG.COD_CIUD,
                        CIUD_DEST = objEnvioinfo.CIUD_DEST.COD_CIUD,
                        PESO = objEnvioinfo.PESO,
                        VALOR_ASEG = objEnvioinfo.VALOR_ASEG,
                        PRECIOKL = objEnvioinfo.PRECIOKL,
                        VALOR_ENVI = objEnvioinfo.VALOR_ENVI,
                        COD_ESTADO = objEnvioinfo.COD_ESTADO.COD_ESTADO
                    };

                    bd.ENVIOS.Add(objEnvio);
                    bd.SaveChanges();

                    var IDGuia = bd.ENVIOS.Max(x => x.COD_GUIA);
                    objEnvioinfo.COD_GUIA = IDGuia;

                    return new ModelEnvioInfo
                    {
                        error = false,
                        msj = "Envio registrado correctamente",
                        content = objEnvioinfo
                    };
                }
                else
                {
                    return new ModelEnvioInfo
                    {
                        error = true,
                        msj = "No se logró registrar el envio.",
                        content = null
                    };
                }
            }
            catch(DbEntityValidationException e)
            {
                return new ModelEnvioInfo
                {
                    error = true,
                    msj = "Error al intentar registrar el envio. Intente de nuevo.",
                    content = null
                };
            }
        }


        public ModelEnvioInfo obtenerEnvio(string codigoGuia)
        {
            try
            {
                var objetoEnviado = bd.ENVIOS.Find(int.Parse(codigoGuia)); //Objeto del "ENVIO" que se quiere obtener

                if (objetoEnviado != null)
                {
                    var objCedulaEmi = bd.CLIENTE.Find(objetoEnviado.CEDULA_EMI); //Se obtiene informacion del "Cliente" Emisor
                    var objCedulaDest = bd.CLIENTE.Find(objetoEnviado.CEDULA_DES); //Se obtiene informacion del "Cliente" Receptor
                    var objCiudadOrig = bd.CIUDAD.Find(objetoEnviado.CIUD_ORIG);  //Se obtiene informacion de la "Ciudad" de origen
                    var objCiudadDest = bd.CIUDAD.Find(objetoEnviado.CIUD_DEST);
                    var objEstadoEnvio = bd.ESTADOS.Find(objetoEnviado.COD_ESTADO);


                    var _CEDULA_EMI = new Cliente
                    {
                        CEDULA = objCedulaEmi.CEDULA,
                        NOMBRES = objCedulaEmi.NOMBRES,
                        APELLIDOS = objCedulaEmi.APELLIDOS,
                        FECHA_NAC = objCedulaEmi.FECHA_NAC,
                        DIRECCION = objCedulaEmi.DIRECCION,
                        TELEFONO = objCedulaEmi.TELEFONO,
                        EMAIL = objCedulaEmi.EMAIL,
                        FECHA_REG = objCedulaEmi.FECHA_REG,
                        ESTADO = objCedulaEmi.ESTADO
                    };
                    var _CEDULA_DES = new Cliente
                    {
                        CEDULA = objCedulaDest.CEDULA,
                        NOMBRES = objCedulaDest.NOMBRES,
                        APELLIDOS = objCedulaDest.APELLIDOS,
                        FECHA_NAC = objCedulaDest.FECHA_NAC,
                        DIRECCION = objCedulaDest.DIRECCION,
                        TELEFONO = objCedulaDest.TELEFONO,
                        EMAIL = objCedulaDest.EMAIL,
                        FECHA_REG = objCedulaDest.FECHA_REG,
                        ESTADO = objCedulaDest.ESTADO
                    };

                    var _CIUD_ORIG = new Ciudad
                    {
                        COD_CIUD = objCiudadOrig.COD_CIUD,
                        NOMBRE = objCiudadOrig.NOMBRE
                    };

                    var _CIUD_DEST = new Ciudad
                    {
                        COD_CIUD = objCiudadDest.COD_CIUD,
                        NOMBRE = objCiudadDest.NOMBRE
                    };

                    var _COD_ESTADO = new Estados
                    {
                        COD_ESTADO = objEstadoEnvio.COD_ESTADO,
                        DESCRIPCION = objEstadoEnvio.DESCRIPCION
                    };

                    return new ModelEnvioInfo
                    {
                        error = false,
                        msj = "Envio encontrado",
                        content = new EnvioInfo
                        {
                            COD_GUIA = objetoEnviado.COD_GUIA,
                            CEDULA_EMI = _CEDULA_EMI,
                            CEDULA_DES = _CEDULA_DES,
                            CIUD_ORIG = _CIUD_ORIG,
                            CIUD_DEST = _CIUD_DEST,
                            PESO = objetoEnviado.PESO,
                            VALOR_ASEG = objetoEnviado.VALOR_ASEG,
                            PRECIOKL = objetoEnviado.PRECIOKL,
                            VALOR_ENVI = objetoEnviado.VALOR_ENVI,
                            COD_ESTADO = _COD_ESTADO
                        }
                    };
                }
                else
                {
                    return new ModelEnvioInfo
                    {
                        error = true,
                        msj = "No se encontró el envio. Intente de nuevo.",
                        content = null
                    };
                }
            }
            catch
            {
                return new ModelEnvioInfo
                {
                    error = true,
                    msj = "Error inexperado. Intente de nuevo.",
                    content = null
                };
            }
           
        }

        public Mensajes actualizarEstadoEnvio(ActualizarEstadoEnvio objActualiza)
        {
            try {
                var objEstadoGuia = bd.ENVIOS.Find(objActualiza.COD_GUIA);
                if (objEstadoGuia != null)
                {
                    //Encontro la guia enviada entoces se actualiza
                    objEstadoGuia.COD_ESTADO = objActualiza.COD_ESTADO;
                    bd.SaveChanges();
                    return mensaje.exitoso("Estado Actualizado.");
                }
                else
                {
                    return mensaje.errores("No se logró actualizar el estado del envio.");
                }
            }
            catch
            {
                return mensaje.errores("Error inexperado, intente de nuevo");
            }
        }
          /*
          * 
          * METODOS "PESOS"
          * 
          */
        public ModelPesos getPesos()
        {
            List<PESOS> objPesos = new List<PESOS>(); //objeto para almacenar la informacion de la tabla pesos por parte de entityFramework               
            List<CIUDAD> objCiudad = new List<CIUDAD>();
            List<ListModelPesos> objetoPesosFin  = new List<ListModelPesos>();

            try                                       
            {
                objPesos = bd.PESOS.ToList();
                objCiudad = bd.CIUDAD.ToList();
                if (objPesos != null)
                {
                    foreach (var codigoCiudades in objPesos)
                    {
                        var CIUD_ORIG = bd.CIUDAD.Find(codigoCiudades.CIUD_ORIG);
                        var CIUD_DEST = bd.CIUDAD.Find(codigoCiudades.CIUD_DEST);
                        if(!CIUD_DEST.Equals(null) && !CIUD_ORIG.Equals(null))
                        {
                            var objPesoFinal = new ListModelPesos
                            {
                                CODIGO = codigoCiudades.CODIGO,
                                CIUDAD_ORIG = new Ciudad
                                {
                                    COD_CIUD = CIUD_ORIG.COD_CIUD,
                                    NOMBRE = CIUD_ORIG.NOMBRE
                                },
                                CIUDAD_DEST = new Ciudad
                                {
                                    COD_CIUD = CIUD_DEST.COD_CIUD,
                                    NOMBRE = CIUD_DEST.NOMBRE,
                                },
                                PRECIOKL = codigoCiudades.PRECIOKL
                            };
                            objetoPesosFin.Add(objPesoFinal);
                        }
                    }
                    return new ModelPesos
                    {
                        error = false,
                        msj = "Lista Pesos",
                        content = objetoPesosFin
                    };
                }
                else
                {
                    //No encontro la lista
                    return new ModelPesos
                    {
                        error = true,
                        msj = "No se encontraron registros.",
                        content = null
                    };
                }
            }
            catch
            {
                return new ModelPesos
                {
                    error = true,
                    msj = "Error inexperado. Intente de nuevo.",
                    content = null
                };
            }                            
        }

        /*
        * 
        * METODOS "ESTADOS"
        * 
        */
        public List<Estados> getEstados()
        {
            var listaEstados = bd.ESTADOS.Select(
                 e => new Estados
                 {
                     COD_ESTADO = e.COD_ESTADO,
                     DESCRIPCION = e.DESCRIPCION,
                 }).ToList();

            return listaEstados;
        }
    }
}
