namespace taka.Models.Enitities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SuggestBook")]
    public partial class SuggestBook
    {
        public int ID { get; set; }

        public int idBook { get; set; }

        public int idCategory { get; set; }

        public virtual Book Book { get; set; }

        public virtual Category Category { get; set; }
    }
}
