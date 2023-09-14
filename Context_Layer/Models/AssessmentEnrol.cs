﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Context_Layer.Models
{
    [Table("assessment_enrols")]
    public partial class AssessmentEnrol
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("assessment_id")]
        public long AssessmentId { get; set; }
        [Column("user_id")]
        public long UserId { get; set; }
        [Column("result")]
        public int? Result { get; set; }
        [Column("score")]
        public byte Score { get; set; }
        [Column("created_at", TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at", TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("AssessmentId")]
        [InverseProperty("AssessmentEnrols")]
        public virtual Assessment Assessment { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("AssessmentEnrols")]
        public virtual User User { get; set; }
    }
}