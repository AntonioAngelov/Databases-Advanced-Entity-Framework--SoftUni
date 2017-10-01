using PlanetHunters.Models;

namespace PlanetHunters.Data
{
    public static class Utility
    {
        public static void Init()
        {
            using (var context = new PlanetHuntersContext())
            {
                context.Database.Initialize(true);

            }
        }
    }
}
