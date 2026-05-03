using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepository
{
    public interface IUnitOfWork
    {
        IAppUser Account { get; }
        IDepartmentsRepository Departments { get; }
        ISells sells { get; }
        void Save();
    }
}
