using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationMigrate.Core;

namespace TranslationMigrate.Console
{
    /// <summary>
    /// Fetch last 5000 record from MasterDev
    /// Get last 10 day record which last modified on from Dev
    /// Compare Dev to MasterDev, if masterDev has no record, directly insert MasterDev db. 
    /// If exist and not match directly update.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var translationManager = new TranslationManager();
            translationManager.Sync();
        }
    }
}
