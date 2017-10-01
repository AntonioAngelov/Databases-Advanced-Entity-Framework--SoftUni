using System.Collections.Generic;
using System.Threading;

namespace WeddingsPlanner.Data.DTOs
{
    public class GuestListDTO
    {
        public GuestListDTO()
        {
            this.Guests = new List<string>();
            this.Agency = new AgencyShortDTO();
        }

        public string Bride { get; set; }

        public string Bridegroom{ get; set; }

        public virtual AgencyShortDTO Agency { get; set; }

        public int InvitedGuests { get; set; }

        public int BrideGuests { get; set; }

        public int BridegroomGuests { get; set; }

        public int AtendingGuests { get; set; }

        public List<string> Guests { get; set; }

    }
}
