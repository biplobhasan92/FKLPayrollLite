using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Panchrukhi.Basic_Settings;
using Panchrukhi.Holidays;
using Panchrukhi.Report;

namespace Panchrukhi
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Application.Run(new frmLogIn());
             Application.Run(new frmSalary());
            // Application.Run(new frmSalaryDeduction());
            // Application.Run(new frmPData());
            // Application.Run(new frmSlotData());
            // Application.Run(new frmCategory());
            // Application.Run(new frmDesigData());
            // Application.Run(new frmClassData());
            // Application.Run(new frmAttendanceData());
            // Application.Run(new frmSectionData());
            // Application.Run(new frmAddNewMachine());
            // Application.Run(new IPStatusCheck());
            // Application.Run(new SecondSystem());
            // Application.Run(new frmAttendanceProcess());
            // Application.Run(new frmReportPersonalInfo());
            // Application.Run(new frmPersonalPromotion());
        }
    }
}
