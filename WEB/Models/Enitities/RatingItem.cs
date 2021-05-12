namespace taka.Models.Enitities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RatingItem")]
    public partial class RatingItem
    {
        public int ID { get; set; }

        public int? idUser { get; set; }

        public int? idBook { get; set; }

        public double? Score { get; set; }

        public DateTime? RateTimestamp { get; set; }
    }
}
