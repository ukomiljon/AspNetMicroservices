namespace FeatureSwitch.API.Dto
{
    public class FeatureRequest
    {
        public string FeatureName { get; set; }
        public string Email { get; set; }
        public bool Enable { get; set; }
    }
}