namespace PhotographersDB
{
    class Startup
    {
        static void Main()
        {
            var context = new PhotographersDBContext();
            context.Database.Initialize(true);
        }
    }
}
