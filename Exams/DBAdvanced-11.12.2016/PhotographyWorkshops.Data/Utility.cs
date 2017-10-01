using PhotographyWorkshops.Models;

namespace PhotographyWorkshops.Data
{
    public static class Utility
    {
        public static void Init()
        {
            using (var context = new PhotographyWorkshopsContext())
            {
                //context.Database.Initialize(true);
                context.Workshops.Add(new Workshop()
                {
                    Name = "a",
                    Location = "a",
                    PricePerParticipant = 1m,
                    Trainer = new Photographer()
                    {
                        FirstName = "a",
                        LastName = "aa",
                        PrimaryCamera = new DSLRCamera()
                        {
                            Make = "aaa",
                            Model = "aaa",
                            MinISO = 100
                        },
                        SecondaryCamera = new DSLRCamera()
                        {
                            Make = "aaaaaaa",
                            Model = "aaaaaaab",
                            MinISO = 1000
                        }

                    }

                });
                context.SaveChanges();
            }
        }

    }
}
