﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Context_Layer.Models
{
    [Table("assessment_meta")]
    public partial class AssessmentMetum
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("assessment_id")]
        public long AssessmentId { get; set; }
        [Column("type", TypeName = "text")]
        public string Type { get; set; }
        [Column("value", TypeName = "text")]
        public string Value { get; set; }
        [Column("created_at", TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at", TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("AssessmentId")]
        [InverseProperty("AssessmentMeta")]
        public virtual Assessment Assessment { get; set; }
    }
}