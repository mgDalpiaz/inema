using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repository.JsonFile.Interfaces
{
    public interface IJsonFileRepository
    {
        void Save<T>(string file, T obj); 

        T Read<T>(string file);

        T Read2Environment<T>(string file);

    }
}
