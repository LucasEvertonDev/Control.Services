using InventoryControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Domain.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        IQueryable<T> Table { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task CommitAsync();

        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T>? FindById(int? id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        Task<T> Insert(T domain);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        Task<T> Update(T domain);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        Task<T> Delete(T domain);
    }
}
