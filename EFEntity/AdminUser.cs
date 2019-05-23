namespace EFEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdminUser")]
    public partial class AdminUser:Base
    {
        public AdminUser()
        {
            this.Roles = new List<Role>();
        }

        [Required]
        [StringLength(50)]       
        public string Name { get; set; }

        public long schoolId { get; set; }
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

        public virtual ICollection<Role> Roles { get; set; }
    }
}
