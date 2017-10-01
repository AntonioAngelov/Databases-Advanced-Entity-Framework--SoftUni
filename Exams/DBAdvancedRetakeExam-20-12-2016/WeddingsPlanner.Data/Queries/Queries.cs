using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingsPlanner.Data.DTOs;
using WeddingsPlanner.Models;
using WeddingsPlanner.Models.Enums;

namespace WeddingsPlanner.Data.Queries
{
    public static class Queries
    {
        public static List<AgencieDTO> GetAgencies()
        {
            using (var context = new WeddingsPlannerContext())
            {
                var agencies = context.Agencies
                    .Select(a => new AgencieDTO()
                    {
                        Name = a.Name,
                        EmployeesCount = a.EmployeesCount,
                        Town = a.Town
                    })
                    .OrderByDescending(a => a.EmployeesCount)
                    .ThenBy(a => a.Name)
                    .ToList();

                return agencies;

            }
        }

        public static List<GuestListDTO> GetGuestLists()
        {
            using (var context = new WeddingsPlannerContext())
            {
               
                var guestLists = context.Weddings
                    .Include("Bride")
                    .Include("Bridegroom")
                    .Include("Invitations")
                    .ToList()
                   .Select(w => new GuestListDTO()
                    {
                        Bride = GetFullName(w.Bride),
                        Bridegroom = GetFullName(w.Bride),
                        Agency = new AgencyShortDTO()
                        {
                            Name = w.Agency.Name,
                            Town = w.Agency.Town
                        },
                        InvitedGuests = w.Invitations.Count,
                        BrideGuests = w.Invitations.Where(i => i.Family == Family.Bride).Count(),
                        BridegroomGuests = w.Invitations.Where(i => i.Family == Family.Bridegroom).Count(),
                        AtendingGuests = w.Invitations.Where(i => i.IsAttending == true).Count(),
                        Guests = w.Invitations.Select(i => i.Guest.FullName).ToList()
                        
                    }).ToList();

                return guestLists;
            }

        }

        public static string GetFullName(Person person)
        {
            return person.FullName;
        }
    }
}
