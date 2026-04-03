using Autorizador.Core.Excepciones;
using Autorizador.Core.Repositorios;
using Autorizador.Infraestructura.Datos.Contextos;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Autorizador.Infraestructura.Datos.Repositorios
{
    public class RepositorioConsultaGeneral : IRepositorioOperacionGeneral
    {
        protected readonly ContextoGeneral _contextoConsultas;

        public RepositorioConsultaGeneral(ContextoGeneral contextoConsultas)
        {
            _contextoConsultas = contextoConsultas;
        }

        public T ObtenerPorCodigo<T>(params object[] llaves) where T : class
        {
            var entidad = _contextoConsultas.Establecer<T>().Find(llaves);

            if (entidad == null)
            {
                throw new EntidadNoExisteException(typeof(T), llaves);
            }

            return entidad;
        }

        public async Task<T> ObtenerPorCodigoAsync<T>(params object[] llaves) where T : class
        {
            var entidad = await _contextoConsultas.Establecer<T>().FindAsync(llaves);

            if (entidad == null)
            {
                throw new EntidadNoExisteException(typeof(T), llaves);
            }

            return entidad;
        }

        public T ObtenerUnoONulo<T>(Expression<Func<T, bool>> filtro,
            string? incluir = null) where T : class
        {
            try
            {
                return _contextoConsultas.Establecer<T>().FirstOrDefault(filtro);
            }
            catch (Exception excepcion)
            {
                throw new InvalidOperationException(excepcion.Message);
            }
        }
        public async Task<T> ObtenerUnoONuloAsync<T>(Expression<Func<T, bool>>? filtro,
            string? incluir = null) where T : class
        {
            try
            {
                return await _contextoConsultas.Establecer<T>().FirstOrDefaultAsync(filtro);
            }
            catch (Exception excepcion)
            {
                throw new InvalidOperationException(excepcion.Message);
            }
        }
        public IList<T> ObtenerPorExpresionConLimite<T>(Expression<Func<T, bool>>? filtro = null,
            string? incluir = null,
            byte limite = 0) where T : class
        {
            try
            {
                if (filtro != null)
                {
                    if (limite == 0)
                        return _contextoConsultas.Establecer<T>().Where(filtro).ToList();
                    else
                        return _contextoConsultas.Establecer<T>().Where(filtro).Take(limite).ToList();
                }
                else
                {
                    return _contextoConsultas.Establecer<T>().ToList();
                }
            }
            catch (Exception le_excepcion)
            {
                throw new EntidadNoExisteException(typeof(T), "Error BD: " + le_excepcion);
            }
        }

        public async Task<List<T>> ObtenerPorExpresionConLimiteAsync<T>(Expression<Func<T, bool>> filtro = null,
            string incluir = null,
            byte limite = 0) where T : class
        {
            try
            {
                if (filtro != null)
                {
                    if (limite == 0)
                        return await _contextoConsultas.Establecer<T>().Where(filtro).ToListAsync();
                    else
                        return await _contextoConsultas.Establecer<T>().Where(filtro).Take(limite).ToListAsync();
                }
                else
                {
                    return await _contextoConsultas.Establecer<T>().ToListAsync();
                }
            }
            catch (Exception excepcion)
            {
                throw new EntidadNoExisteException(typeof(T), "Error BD: " + excepcion);
            }
        }

        public IQueryable<T> Listar<T>() where T : class
        {
            return _contextoConsultas
                .Establecer<T>();
        }

        public async Task<IReadOnlyList<T>> ObtenerTodoAsync<T>() where T : class
        {
            return await _contextoConsultas
                .Set<T>()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ObtenerAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await _contextoConsultas
                .Set<T>()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ObtenerAsync<T>(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string? includeString = null,
            bool disableTracking = true) where T : class
        {
            IQueryable<T> query = _contextoConsultas.Set<T>();

            if (disableTracking) query = query.AsNoTracking();

            if (string.IsNullOrEmpty(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ObtenerAsync<T>(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            List<Expression<Func<T, object>>>? includes = null,
            bool disableTracking = true) where T : class
        {
            IQueryable<T> query = _contextoConsultas
                .Set<T>();

            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public virtual async Task<T> ObtenerPorIdAsync<T>(int id) where T : class
        {
            return await _contextoConsultas
                .Set<T>()
                .FindAsync(id);
        }

        public void Adicionar<T>(T aoObjeto) where T : class
        {
            _contextoConsultas
                .Set<T>()
                .Add(aoObjeto);
        }

        public async Task<T> AdicionarAsync<T>(T entity) where T : class
        {
            try
            {
                _contextoConsultas
                .Set<T>()
                .Add(entity);
                return entity;
            }
            catch (Exception excepcion)
            {
                throw new EntidadNoExisteException(typeof(T), "Error BD: " + excepcion);
            }
        }

        public void AdicionarRango<T>(List<T> objeto) where T : class
        {
            _contextoConsultas
                .Set<T>()
                .AddRange(objeto);
        }

        public void Modificar<T>(T objeto) where T : class
        {
            _contextoConsultas
                .Set<T>()
                .Attach(objeto);
            _contextoConsultas
                .Entry(objeto).State = EntityState.Modified;
        }

        public async Task<T> ModificarAsync<T>(T objeto) where T : class
        {
            _contextoConsultas.Entry(objeto).State = EntityState.Modified;
            return objeto;
        }

        public void Eliminar<T>(T objeto) where T : class
        {
            _contextoConsultas
                .Set<T>()
                .Remove(objeto);
        }

        public async Task EliminarAsync<T>(T objeto) where T : class
        {
            _contextoConsultas
                .Set<T>()
                .Remove(objeto);
        }

        public void GuardarCambios()
        {
            try
            {
                _contextoConsultas.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new ValidacionException("Error al guardar cambios en bbdd. " + e.Message);
            }
        }

        public async Task GuardarCambiosAsync()
        {
            try
            {
                await _contextoConsultas.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ValidacionException("Error al guardar cambios en bbdd. " + ex.Message);
            }
        }
    }
}