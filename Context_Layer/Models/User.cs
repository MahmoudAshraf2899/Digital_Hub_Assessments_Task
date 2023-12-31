﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Context_Layer.Models
{
    [Table("users")]
    public partial class User
    {
        public User()
        {
            AssessmentAnswers = new HashSet<AssessmentAnswer>();
            AssessmentEnrols = new HashSet<AssessmentEnrol>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("api_key")]
        [StringLength(36)]
        [Unicode(false)]
        public string ApiKey { get; set; }
        [Required]
        [Column("username")]
        [StringLength(255)]
        [Unicode(false)]
        public string Username { get; set; }
        [Required]
        [Column("password")]
        [StringLength(255)]
        [Unicode(false)]
        public string Password { get; set; }
        [Column("first_name")]
        [StringLength(255)]
        [Unicode(false)]
        public string FirstName { get; set; }
        [Column("last_name")]
        [StringLength(255)]
        [Unicode(false)]
        public string LastName { get; set; }
        [Required]
        [Column("email")]
        [StringLength(255)]
        [Unicode(false)]
        public string Email { get; set; }
        [Column("is_banned")]
        public byte IsBanned { get; set; }
        [Column("is_verified")]
        public byte IsVerified { get; set; }
        [Required]
        [Column("confirm_code")]
        [StringLength(36)]
        [Unicode(false)]
        public string ConfirmCode { get; set; }
        [Column("confirmed_at", TypeName = "datetime")]
        public DateTime? ConfirmedAt { get; set; }
        [Column("password_changed_at", TypeName = "datetime")]
        public DateTime? PasswordChangedAt { get; set; }
        [Column("display_name")]
        [StringLength(255)]
        [Unicode(false)]
        public string DisplayName { get; set; }
        [Column("user_url")]
        [StringLength(255)]
        [Unicode(false)]
        public string UserUrl { get; set; }
        [Column("is_ldap")]
        public byte IsLdap { get; set; }
        [Column("created_by")]
        public long CreatedBy { get; set; }
        [Column("updated_by")]
        public long UpdatedBy { get; set; }
        [Column("remember_token")]
        [StringLength(100)]
        [Unicode(false)]
        public string RememberToken { get; set; }
        [Column("created_at", TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at", TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }
        [Column("deleted_at", TypeName = "datetime")]
        public DateTime? DeletedAt { get; set; }
        [Column("otp")]
        [StringLength(255)]
        [Unicode(false)]
        public string Otp { get; set; }
        [Column("otp_created_at", TypeName = "datetime")]
        public DateTime? OtpCreatedAt { get; set; }
        [Column("profile_picture_id")]
        public long? ProfilePictureId { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<AssessmentAnswer> AssessmentAnswers { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<AssessmentEnrol> AssessmentEnrols { get; set; }
    }
}