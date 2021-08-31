using HoraExtra;
using System.Collections.Generic;
using System.IO;

namespace CasaDoCodigo
{
    class DataService : IDataService
    {

        private readonly ApplicationContext contexto;

        public DataService(ApplicationContext conexto)
        {
            this.contexto = conexto;
        }

        public void InicializaDB()
        {
            contexto.Database.EnsureCreated();
        }

    }

}
