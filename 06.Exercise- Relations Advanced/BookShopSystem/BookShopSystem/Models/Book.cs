namespace BookShopSystem.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public enum EditionType
    {
        Noral,
        Promo,
        Gold
    }

    public enum AgeRestriction
    {
        Minor,
        Teen,
        Adult
    }

    public class Book
    {
        public Book()
        {
            this.Categories = new HashSet<Category>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public EditionType EditionType { get; set; }

        public decimal Price { get; set; }

        public int Copies { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public AgeRestriction AgeRestriction { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
        
        public virtual ICollection<Book> RelatedBooks { get; set; }
    }
}
