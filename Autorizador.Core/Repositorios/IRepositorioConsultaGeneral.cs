using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Autorizador.Core.Repositorios
{
    public interface IRepositorioConsultaGeneral
    {
        /// <summary>
        /// Obtiene una entidad por su identificador.
        /// </summary>
        /// <param name="llaves">Identificador o identificadores.</param>
        /// <typeparam name="T">Tipo de entidad.</typeparam>
        /// <returns>Entidad.</returns>
        T ObtenerPorCodigo<T>(params object[] llaves) where T : class;

        /// <summary>
        /// Obtiene una entidad por su identificador.
        /// </summary>
        /// <param name="llaves">Identificador o identificadores.</param>
        /// <typeparam name="T">Tipo de entidad.</typeparam>
        /// <returns>Entidad.</returns>
        Task<T> ObtenerPorCodigoAsync<T>(params object[] llaves) where T : class;

        /// <summary>
        /// Obtiene un registro de acuerdo al filtro aplicado.
        /// </summary>
        /// <param name="filtro">Filtro de consulta.</param>
        /// <param name="incluir">Elementos a incluir.</param>
        /// <typeparam name="T">Tipo de entidad.</typeparam>
        /// <returns>Registro de la entidad.</returns>
        Task<T> ObtenerUnoONuloAsync<T>(Expression<Func<T, bool>> filtro,
            string? incluir = null) where T : class;

        /// <summary>
        /// Obtiene un registro de acuerdo al filtro aplicado.
        /// </summary>
        /// <param name="filtro">Filtro de consulta.</param>
        /// <param name="incluir">Elementos a incluir.</param>
        /// <typeparam name="T">Tipo de entidad.</typeparam>
        /// <returns>Registro de la entidad.</returns>
        T ObtenerUnoONulo<T>(Expression<Func<T, bool>> filtro,
            string? incluir = null) where T : class;

        /// <summary>
        /// Obtiene una lista de entidades de acuerdo al filtro aplicado.
        /// </summary>
        /// <param name="filtro">Filtro de consulta.</param>
        /// <param name="incluir">Elementos a incluir.</param>
        /// <param name="limite">Limite de registros de la consulta.</param>
        /// <typeparam name="T">Tipo de entidad.</typeparam>
        /// <returns>Registros de la entidad.</returns>
        Task<List<T>> ObtenerPorExpresionConLimiteAsync<T>(Expression<Func<T, bool>>? filtro = null,
            string? incluir = null,
            byte limite = 0) where T : class;

        /// <summary>
        /// Obtiene una lista de entidades de acuerdo al filtro aplicado.
        /// </summary>
        /// <param name="filtro">Filtro de consulta.</param>
        /// <param name="incluir">Elementos a incluir.</param>
        /// <param name="limite">Limite de registros de la consulta.</param>
        /// <typeparam name="T">Tipo de entidad.</typeparam>
        /// <returns>Registros de la entidad.</returns>
        IList<T> ObtenerPorExpresionConLimite<T>(Expression<Func<T, bool>>? filtro = null,
            string? incluir = null,
            byte limite = 0) where T : class;

        /// <summary>
        /// Obtiene todos los registros.
        /// </summary>
        /// <typeparam name="T">Tipo de entidad.</typeparam>
        /// <returns>Registros de la entidad.</returns>
        IQueryable<T> Listar<T>() where T : class;

        /// <summary>
        /// Obtiene todas los datos
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <returns>Registros de la entidad.</returns>
        Task<IReadOnlyList<T>> ObtenerTodoAsync<T>() where T : class;

        /// <summary>
        /// Obtener datos de la entidad por filtros
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="predicate">Predicate de la entidad</param>
        /// <returns></returns>
        Task<IReadOnlyList<T>> ObtenerAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Obtener datos de la entidad por filtros
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="predicate">Predicate de la entidad</param>
        /// <param name="orderBy">Order by de la entidad</param>
        /// <param name="includeString">Include de tipo string</param>
        /// <param name="disableTracking">Estado de tracking</param>
        /// <returns></returns>
        Task<IReadOnlyList<T>> ObtenerAsync<T>(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string? includeString = null,
            bool disableTracking = true) where T : class;

        /// <summary>
        /// Obtener datos de la entidad por filtros
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="predicate">Predicate de la entidad</param>
        /// <param name="orderBy">Order by de la entidad</param>
        /// <param name="includes">Include de entidades</param>
        /// <param name="disableTracking">Estado de tracking</param>
        /// <returns></returns>
        Task<IReadOnlyList<T>> ObtenerAsync<T>(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            List<Expression<Func<T, object>>>? includes = null,
            bool disableTracking = true) where T : class;

        /// <summary>
        /// Obtener la entidad por su ID
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="id">Id de la entidad</param>
        /// <returns>Entidad</returns>
        Task<T> ObtenerPorIdAsync<T>(int id) where T : class;

    }
}
