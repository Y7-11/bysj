namespace EFEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Role")]
    public partial class Role:Base
    {
        public Role()
        {
            this.Permissions=new List<Permission>();
            this.AdminUsers=new List<AdminUser>();
        }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; } 

        public ICollection<AdminUser> AdminUsers { get; set; }

    }
}
