namespace EFEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AminUser")]
    public partial class AminUser
    {
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string PhoneNum { get; set; }

        [Required]
        [StringLength(100)]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(20)]
        public string PasswordSalt { get; set; }

        [Required]
        [StringLength(30)]
        public string Email { get; set; }

        public int LoginErrorTimes { get; set; }

        public DateTime? LastLoginErrorDateTime { get; set; }

        public DateTime CreateDateTime { get; set; }

        public bool IsDeleted { get; set; }
    }
}
