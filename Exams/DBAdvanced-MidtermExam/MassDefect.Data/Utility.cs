namespace MassDefect.Data
{
    public static class Utility
    {
        public static void Init()
        {
            var context = new MassDefectContext();
            context.Database.Initialize(true);
        }
    }
}
