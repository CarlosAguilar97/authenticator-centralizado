using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autorizador.Core.Repositorios
{
    public interface IRepositorioOperacionGeneral : IRepositorioConsultaGeneral
    {
        /// <summary>
        /// Adiciona un objeto en el contexto.
        /// </summary>
        /// <typeparam name="T">Tipo de entidad.</typeparam>
        /// <param name="objeto">Objeto a adicionar.</param>
        void Adicionar<T>(T objeto) where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> AdicionarAsync<T>(T entity) where T : class;

        /// <summary>
        /// Adiciona un rango de objetos en el contexto.
        /// </summary>
        /// <typeparam name="T">Tipo de entidad.</typeparam>
        /// <param name="objeto">Listado de objeto a adicionar.</param>
        void AdicionarRango<T>(List<T> objeto) where T : class;

        /// <summary>
        /// Modificar entidad
        /// </summary>
        /// <param name="aoObjeto">Tipo de entidad</param>
        void Modificar<T>(T aoObjeto) where T : class;

        /// <summary>
        /// Modificar entidad async
        /// </summary>
        /// <param name="entity">Tipo de entidad</param>
        /// <returns></returns>
        Task<T> ModificarAsync<T>(T entity) where T : class;

        /// <summary>
        /// Eliminar entidad  
        /// </summary>
        /// <param name="aoObjeto">Tipo de entidad</param>
        void Eliminar<T>(T aoObjeto) where T : class;

        /// <summary>
        /// Eliminar entidad async
        /// </summary>
        /// <param name="aoObjeto">Tipo de entidad</param>
        /// <returns></returns>
        Task EliminarAsync<T>(T aoObjeto) where T : class;

        /// <summary>
        /// Guarda los cambios realizados en el contexto.
        /// </summary>
        void GuardarCambios();

        Task GuardarCambiosAsync();
    }
}
