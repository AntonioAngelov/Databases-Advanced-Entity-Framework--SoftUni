using System.Linq;
using WeddingsPlanner.Models;
using WeddingsPlanner.Models.Enums;

namespace WeddingsPlanner.Data
{
    public static class Utility
    {
        public static void Init()
        {
            var context = new WeddingsPlannerContext();
            //context.Database.Initialize(true);
            
        }
    }
}
