namespace Projection_Dto
{
    public class EmployeeDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public string ManagerLastName { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName} {Salary} - Manager: {ManagerLastName ?? "[no manager]"}";
        }
    }
}
