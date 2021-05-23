namespace taka.Models.Enitities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Image")]
    public partial class Image
    {
        public int ID { get; set; }

        [StringLength(2048)]
        public string Url { get; set; }

        public int? idBook { get; set; }
        public int? order { get; set; }

        public virtual Book Book { get; set; }
    }
}
