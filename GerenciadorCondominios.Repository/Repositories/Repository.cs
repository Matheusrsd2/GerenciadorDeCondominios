
using GerenciadorCondominios.Repository;
using GerenciadorCondominios.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciadorCondominios.Repository.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly Context _contexto;

        public Repository(Context contexto)
        {
            _contexto = contexto;
        }


        public async Task Update(TEntity entity)
        {
            try
            {
                var update = _contexto.Set<TEntity>().Update(entity);
                update.State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var entity = await this.GetById(id);
                _contexto.Set<TEntity>().Remove(entity);
                await _contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

       
        public async Task Add(TEntity entity)
        {
            try
            {
                await _contexto.AddAsync(entity);
                await _contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<TEntity> GetById(int id)
        {
            try
            {
                return await _contexto.Set<TEntity>().FindAsync(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<TEntity> GetById(string id)
        {
            try
            {
                return await _contexto.Set<TEntity>().FindAsync(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            try
            {
                return await _contexto.Set<TEntity>().ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}