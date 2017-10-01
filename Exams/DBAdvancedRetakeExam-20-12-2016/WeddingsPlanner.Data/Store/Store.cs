using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using WeddingsPlanner.Data.DTOs;
using WeddingsPlanner.Models;
using WeddingsPlanner.Models.Enums;

namespace WeddingsPlanner.Data.Store
{
    public static class Store
    {
        public static void StoreAgencies(IEnumerable<AgencieDTO> agencies)
        {
            using (var context = new WeddingsPlannerContext())
            {
                foreach (var agencie in agencies)
                {
                    Agency currentAgencie = new Agency()
                    {
                        Name = agencie.Name,
                        EmployeesCount = agencie.EmployeesCount,
                        Town = agencie.Town
                    };

                    context.Agencies.Add(currentAgencie);

                    Console.WriteLine($"Successfully imported {agencie.Name}");
                }

                context.SaveChanges();
            }
            
        }

        public static void StorePeople (IEnumerable<PersonDTO> people)
        {
            using (var context = new WeddingsPlannerContext())
            {
                foreach (var person in people)
                {
                   
                    if (person.FirstName == null ||
                        person.MiddleInitial == null ||
                        person.LastName == null)
                    {
                        Console.WriteLine("Error. Invalid data provided");
                        continue;
                    }

                    var currentPerson = new Person()
                    {
                        FirstName = person.FirstName,
                        MiddleNameInitial = person.MiddleInitial,
                        LastName = person.LastName,
                        Gender = person.Gender,
                        BirthDate = person.BirthDay,
                        Phone = person.Phone,
                        Email = person.Email
                    };

                    try
                    {
                        context.People.Add(currentPerson);
                        context.SaveChanges();
                        Console.WriteLine($"Successfully imported {person.FirstName} {person.MiddleInitial} {person.LastName}");
                    }
                    catch (DbEntityValidationException)
                    {
                        context.People.Remove(currentPerson);
                        Console.WriteLine("Error. Invalid data provided");
                    }
                }
                
            }
            
        }

        public static void StoreWeddings(IEnumerable<WeddingDTO> weddings)
        {
            using (var context = new WeddingsPlannerContext())
            {
                foreach (var w in weddings)
                {
                    var bride =context.People.FirstOrDefault(p => p.FirstName + " " + p.MiddleNameInitial + " " + p.LastName == w.Bride);
                    var bridegroom = context.People.FirstOrDefault(p => p.FirstName + " " + p.MiddleNameInitial + " " + p.LastName == w.Bridegroom);
                    var agency = context.Agencies.FirstOrDefault(a => a.Name == w.Agency);

                    if (bride == null || bridegroom == null || agency == null)
                    {
                        Console.WriteLine("Error. Invalid data provided.");
                        continue;
                    }

                    var currentWedding = new Wedding()
                    {
                        Bride = bride,
                        Bridegroom = bridegroom,
                        Agency = agency,
                        Date = w.Date
                    };

                    if (w.Guests != null)
                    {
                        foreach (var g in w.Guests)
                        {
                            var guest =
                                context.People.FirstOrDefault(
                                    p => p.FirstName + " " + p.MiddleNameInitial + " " + p.LastName == g.Name);
                            if (guest != null)
                            {
                                currentWedding.Invitations.Add(new Invitation()
                                {
                                    Family = g.Family,
                                    IsAttending = g.RSVP,
                                    Guest = guest

                                });
                            }
                        }
                    }

                    try
                    {
                        context.Weddings.Add(currentWedding);
                        context.SaveChanges();
                        Console.WriteLine($"Successfully imported wedding of {currentWedding.Bride.FirstName} and {currentWedding.Bridegroom.FirstName}");
                    }
                    catch (DbEntityValidationException)
                    {
                        Console.WriteLine("Error. Invalid data provided.");
                    }
                }



            }
        }

        public static void StoreVenues(IEnumerable<XElement> venuesXml)
        {
            using (var context = new WeddingsPlannerContext())
            {
                List<Venue> venues = new List<Venue>();

                foreach (var v in venuesXml)
                {
                    var currentVenue = new Venue()
                    {
                        Name = v.Attribute("name").Value,
                        Capacity = int.Parse(v.Element("capacity").Value),
                        Town = v.Element("town").Value
                    };

                    venues.Add(currentVenue);
                    Console.WriteLine($"Successfully imported {currentVenue.Name}");
                }
                context.Venues.AddRange(venues);
                context.SaveChanges();

                Random rnd = new Random();
                var venuesCount = context.Venues.Count();

                foreach (var w in context.Weddings)
                {
                    for (int i = 0; i < 2; i++)
                    {
                     w.Venues.Add(context.Venues.Find(rnd.Next(1, venuesCount)));   
                    }
                }
                context.SaveChanges();

            }
        }

        public static void StorePresents(IEnumerable<XElement> presentsXml)
        {
            using (var context = new WeddingsPlannerContext())
            {
                foreach (var p in presentsXml)
                {
                    var type = p.Attribute("type");
                    var invitationIdXml = p.Attribute("invitation-id");
                    if (type == null || invitationIdXml == null)
                    {
                        Console.WriteLine("Error. Invalid data provided");
                        continue;
                    }

                    var invitationId = int.Parse(invitationIdXml.Value);
                    if(invitationId < 0 || invitationId > context.Invitations.Count())
                    {
                        Console.WriteLine("Error. Invalid data provided");
                        continue;
                    }

                    if (type.Value == "cash")
                    {
                        var amount = p.Attribute("amount");
                        if (amount == null)
                        {
                            Console.WriteLine("Error. Invalid data provided");
                            continue;
                        }

                        Cash present = new Cash()
                        {
                            CashAmount = decimal.Parse(amount.Value)
                        };

                        var invitation = context.Invitations.Find(invitationId);
                        invitation.Present = present;
                        try
                        {
                            context.SaveChanges();
                            Console.WriteLine($"Succesfully imported gift from {invitation.Guest.FullName}");
                        }
                        catch (DbEntityValidationException)
                        {
                            Console.WriteLine("Error. Invalid data provided");
                        }

                    }
                    else if (type.Value == "gift")
                    {
                        var nameXml = p.Attribute("present-name");
                        if (nameXml == null)
                        {
                            Console.WriteLine("Error. Invalid data provided");
                            continue;
                        }

                        var presentSize = p.Attribute("size");
                        var size = PresentSize.NotSpecified;
                        if (presentSize != null)
                        {
                            if (!Enum.TryParse(presentSize.Value , out size))
                            {
                                Console.WriteLine("Error. Invalid data provided");
                                continue;
                            }
                        }

                        var gift = new Gift()
                        {
                            Size = size,
                            Name = nameXml.Value
                        };

                        var invitation = context.Invitations.Find(invitationId);
                        invitation.Present = gift;
                        try
                        {
                            context.SaveChanges();
                            Console.WriteLine($"Succesfully imported gift from {invitation.Guest.FullName}");
                        }
                        catch (DbEntityValidationException)
                        {
                            Console.WriteLine("Error. Invalid data provided");
                        }

                    }

                }
            }
        }
    }
}
