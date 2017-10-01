namespace OOP_Intro
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Family
    {
        private List<Person> familyMembers;

        public Family()
        {
            familyMembers = new List<Person>();
        }

        public void AddMember(Person member)
        {
            this.familyMembers.Add(member);
        }

        public Person GetOldestMember()
        {
            Person oldest = this.familyMembers[0];

            for (int i = 1 ; i < this.familyMembers.Count; i++)
            {
                if (this.familyMembers[i].Age > oldest.Age)
                {
                    oldest = this.familyMembers[i];
                }
            }

            return oldest;
        }
    }
}
