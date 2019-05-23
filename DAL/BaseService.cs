using EFEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class BaseService<T> where T : Base
    {
        private Dbcontext ctx;        
        public BaseService(Dbcontext ctx)
        {
            this.ctx = ctx;
        }
        /// <summary>
        /// 获取所有没有软删除的数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            try
            {

                return ctx.Set<T>().Where(e => e.IsDeleted == false);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        /// <summary>
        /// 获得被软删除的数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetRecoveries()
        {
            return ctx.Set<T>().Where(e => e.IsDeleted == true);
        }

        /// <summary>
        /// 获取总数据条数
        /// </summary>
        /// <returns></returns>
        public long GetTotalCount()
        {
            return GetAll().LongCount();
        }

        public IQueryable<T> GetPageDate(int startIndex, int count)
        {
            return GetAll().OrderBy(e => e.CreateDateTime).Skip(startIndex).Take(count);
        }

        /// <summary>
        /// 查找id==id的数据，如果找不到返回null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(long id)
        {
            return GetAll().Where(e => e.Id == id).SingleOrDefault();
        }

        /// <summary>
        /// 查找被删除的数据中id==id的数据，如果找不到返回null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetBydelId(long id)
        {
            return GetRecoveries().Where(e => e.Id == id).SingleOrDefault();
        }

        public void MarkDeleted(long id)
        {
            var data = GetById(id);
            data.IsDeleted = true;
            ctx.SaveChanges();
        }

        public void RecoveryDeleted(long id)
        {
            var data = GetBydelId(id);
            data.IsDeleted = false;
            ctx.SaveChanges();
        }
    }
}
