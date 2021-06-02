using System.ComponentModel.DataAnnotations;

namespace FeatureSwitch.API.Dto
{
    public class FeatureRequest
    {
        [Required]         
        [StringLength(50)]
        public string FeatureName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("True")]
        public bool Enable { get; set; }
    }

}