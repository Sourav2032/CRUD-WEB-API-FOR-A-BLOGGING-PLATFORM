using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Assignment.DAL.Entity
{
    [Table("users")]
    public partial class User
    {
        public User()
        {
            Posts = new HashSet<Post>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name", TypeName = "character varying")]
        public string Name { get; set; } = null!;
        [Column("password", TypeName = "character varying")]
        public string Password { get; set; } = null!;
        [Column("is_active")]
        public bool? IsActive { get; set; }

        [InverseProperty("CreatedByNavigation")]
        public virtual ICollection<Post> Posts { get; set; }
    }
}
