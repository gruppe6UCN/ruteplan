using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.FilerHelpersTest
{
    class FileHelperTestClass
    {
        //Example SOmething THINGIe of Thing:
        // http://www.filehelpers.net/example/QuickStart/ReadWriteRecordByRecord/
        
        [DelimitedRecord(",")]
        public class DefaultRouteFileHelperTest
        {
            public long ID;

            public double TrailerType;

            public bool ExtraRoute;
        }

        public void TestFileHelperSheitsz()
        {
            var engine = new FileHelperEngine<DefaultRouteFileHelperTest>();
            var records = engine.ReadFile("C:\\Users\\The Baron\\Dropbox\\3. Projekt\\Arla Food\\EventyrIBarbiesPrincesseSLot.csv");

            foreach (var record in records)
            {
                Console.WriteLine(record.ID);
                Console.WriteLine(record.TrailerType);
                Console.WriteLine(record.ExtraRoute);
                Console.WriteLine();

                //Can then instead of console writeline here, create a model class instead
                //and then return/store that to database or some sheitz.
            }
        }


    }
}
