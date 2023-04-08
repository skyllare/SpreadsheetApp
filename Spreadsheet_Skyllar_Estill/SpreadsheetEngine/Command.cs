using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    public interface Command
    {
        void Execute(Spreadsheet s);

        void Unexecute(Spreadsheet s);

        int GetRow();

        int GetCol();

    }
}
