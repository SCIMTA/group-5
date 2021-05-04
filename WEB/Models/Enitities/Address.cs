namespace taka.Models.Enitities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Address")]
    public partial class Address
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [Column("Address")]
        [StringLength(200)]
        public string Address1 { get; set; }

        public int? idUser { get; set; }

        public virtual User User { get; set; }
    }
}
