using System.Linq;
using System.Security.Principal;
using System.Web;
using EpiPersonalization.Criteria.UkPostcodes.Models;
using EPiServer.Personalization.VisitorGroups;

namespace EpiPersonalization.Criteria.UkPostcodes
{
    [VisitorGroupCriterion(
        Category = "Geolocation",
        DisplayName = "UK Postal Codes",
        Description = "Personalization criteria based on customer's UK address")]

    public class UkPostcodeCriterion : CriterionBase<UkPostcodeCriterionSettings>
    {
        public override bool IsMatch(IPrincipal principal, HttpContextBase httpContext)
        {
            //user location
            var visitorlocation = GetVisitorPostcode();

            // Wildcard in OutwardCode
            if (Model.OutwardCode.Contains("*"))
            {
                var nOutwardcode = Model.OutwardCode.Replace("*", "");

                return visitorlocation.OutwardCode.StartsWith(nOutwardcode);
            }

            // Wildcard in InwardCode
            if (Model.InwardCode.Contains("*"))
            {
                var nInwardcode = Model.OutwardCode.Replace("*", "");

                return visitorlocation.OutwardCode == Model.OutwardCode && visitorlocation.OutwardCode.StartsWith(nInwardcode);
            }

            // no Wildcard
            if (!Model.OutwardCode.Contains("*") && !Model.InwardCode.Contains("*"))
            {
               return visitorlocation.OutwardCode == Model.OutwardCode && visitorlocation.InwardCode == Model.InwardCode;
            }


            return false;
        }

        private Postcode GetVisitorPostcode()
        {

            //ToDo: Get Customer postcode
            var postcode = "M2 2AN";

            var userLocation = new Postcode();
            
            var codes = postcode.Split(new[] {' '});

            if (codes.Any() && codes.Length == 2)
            {
                userLocation.OutwardCode = codes[0].ToUpper();
                userLocation.InwardCode = codes[1].ToUpper();
            }
            
            return userLocation;
        }
    }
}
