namespace LocalStoreDB
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Models;

    class Startup
    {
        static void Main(string[] args)
        {        
            var context = new LocalStoreContext();

            context.Database.Initialize(true);
        }
    }
}
