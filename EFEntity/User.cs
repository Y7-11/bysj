namespace EFEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User:Base
    {
        [Required]
        [StringLength(20)]
        public string PhoneNum { get; set; }

        [Required]
        [StringLength(100)]
        public string PasswordHash { get; set; }

        public string Account { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public string NickName { get; set; }

        [Required]
        [StringLength(20)]
        public string PasswordSalt { get; set; }

        public long level { get; set; }

        //public virtual Guide Guide { get; set; }

        //[ForeignKey("Uid")]
        //public ICollection<AdminUser> AdminUsers { get; set; }

    }
}
