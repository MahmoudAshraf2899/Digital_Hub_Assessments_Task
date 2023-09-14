﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Context_Layer.Models
{
    [Table("assessment_answers")]
    public partial class AssessmentAnswer
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("assessment_id")]
        public long AssessmentId { get; set; }
        [Column("question_id")]
        public long QuestionId { get; set; }
        [Required]
        [Column("answer", TypeName = "text")]
        public string Answer { get; set; }
        [Column("user_id")]
        public long UserId { get; set; }
        [Column("score")]
        public byte Score { get; set; }
        [Column("created_at", TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at", TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("AssessmentId")]
        [InverseProperty("AssessmentAnswers")]
        public virtual Assessment Assessment { get; set; }
        [ForeignKey("QuestionId")]
        [InverseProperty("AssessmentAnswers")]
        public virtual AssessmentQuestion Question { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("AssessmentAnswers")]
        public virtual User User { get; set; }
    }
}