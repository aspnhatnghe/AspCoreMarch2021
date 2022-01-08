using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Entities
{
    [Table("Feature")]
    public class Feature
    {
        [Key]
        public int FeatureId { get; set; }
        [Required]
        [MaxLength(100)]
        public string FeatureName { get; set; }
        [Required]
        [MaxLength(250)]
        public string FeatureUrl { get; set; }
    }

    [Table("Permission")]
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        public int FeatureId { get; set; }
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        [ForeignKey("FeatureId")]
        public Feature Feature { get; set; }

        public bool Access { get; set; } = true;
        public bool New { get; set; } = false;
        public bool Modify { get; set; } = false;
        public bool Remove { get; set; } = false;
    }
}
