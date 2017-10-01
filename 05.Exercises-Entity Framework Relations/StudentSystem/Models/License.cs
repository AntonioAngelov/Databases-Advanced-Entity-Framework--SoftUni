namespace StudentSystem.Models
{
    public class License
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual Resource Resource { get; set; }
    }
}
