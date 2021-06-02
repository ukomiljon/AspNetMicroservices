using System.ComponentModel.DataAnnotations;

namespace FeatureSwitch.API.Dto
{
    public class FeatureResponse
    {
        [Required]
        [RegularExpression("True")]
        public bool CanAccess { get; internal set; }
    }
}