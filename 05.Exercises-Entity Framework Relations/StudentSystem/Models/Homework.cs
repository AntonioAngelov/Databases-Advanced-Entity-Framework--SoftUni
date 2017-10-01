namespace StudentSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Homework
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public HomeworkContentType Type { get; set; }

        public DateTime SubmissionDate { get; set; }

        public int StudentId { get; set; }

        public int CourseId { get; set; }

        public virtual Student Student { get; set; }

        public virtual Course Course { get; set; }

    }
}
