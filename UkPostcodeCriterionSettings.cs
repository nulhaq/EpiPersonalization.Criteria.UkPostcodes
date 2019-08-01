using System.ComponentModel.DataAnnotations;
using EPiServer.Personalization.VisitorGroups;

namespace EpiPersonalization.Criteria.UkPostcodes
{
    public class UkPostcodeCriterionSettings : CriterionModelBase
    {
        [Required]
        public string OutwardCode { get; set; }
        [Required]
        public string InwardCode { get; set; }
        
        public override ICriterionModel Copy()
        {
            return ShallowCopy();
        }
    }
}
