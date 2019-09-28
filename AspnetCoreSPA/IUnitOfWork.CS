using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreSPATemplate.Repositories;

namespace AspnetCoreSPATemplate
{
    public interface IUnitOfWork
    {
        IContactRepository ContactRepository { get; }

        // TODO: add these methods if we need them.
        // void Save();
    }
}
