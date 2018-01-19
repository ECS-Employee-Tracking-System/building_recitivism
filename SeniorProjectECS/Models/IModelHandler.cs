using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProjectECS.Models
{
    public interface IModelHandler<T>
    {
        T GetModel(int id);
        IEnumerable<T> GetModels();
        void AddModel(T Model);
        void UpdateModel(T Model);
    }
}
