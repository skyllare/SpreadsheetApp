using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    public interface Command
    {
        /// <summary>
        /// inferface execute method.
        /// </summary>
        /// <param name="s">spreadsheet.</param>
        void Execute(Spreadsheet s);

        /// <summary>
        /// inferface unexecute method.
        /// </summary>
        /// <param name="s">spreadsheet.</param>
        void Unexecute(Spreadsheet s);

        /// <summary>
        /// gets row.
        /// </summary>
        /// <returns>spreadsheet row.</returns>
        int GetRow();

        /// <summary>
        /// gets column.
        /// </summary>
        /// <returns>spreadsheet column.</returns>
        int GetCol();

    }
}
