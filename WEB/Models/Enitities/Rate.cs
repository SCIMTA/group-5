namespace taka.Models.Enitities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Rate")]
    public partial class Rate
    {
        public int ID { get; set; }

        [Column("Rate")]
        public int? Rate1 { get; set; }

        public string Comment { get; set; }

        public int? idUser { get; set; }

        public int? idBook { get; set; }

        public virtual Book Book { get; set; }

        public virtual User User { get; set; }
    }
}
