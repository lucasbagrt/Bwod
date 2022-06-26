﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bwod.OrderAPI.Model.Base
{
    public class BaseEntity
    {
        [Key]
        [Column("id")]
        public int id { get; set; }
    }
}
