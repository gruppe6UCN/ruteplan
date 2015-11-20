using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    class ImportService : IImportService
    {
        private static IImportService IMS = new ImportService();

        //TODO: Everything... Also not use a string.
        public string Import()
        {
            return "Nej, du blev snydt, thihi fniz. #Putin!"
        }

    }
}
