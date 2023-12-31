﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Context_Layer.Models
{
    [Table("assessment_options")]
    public partial class AssessmentOption
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("option", TypeName = "text")]
        public string Option { get; set; }
        [Column("question_id")]
        public long QuestionId { get; set; }
        [Column("correct")]
        public byte Correct { get; set; }
        [Column("order")]
        public int Order { get; set; }
        [Column("created_at", TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at", TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("QuestionId")]
        [InverseProperty("AssessmentOptions")]
        public virtual AssessmentQuestion Question { get; set; }
    }
}