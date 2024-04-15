using System.ComponentModel.DataAnnotations;

namespace FribergHomes.API.Models
{
    //Creator: Tobias Ledin

    public class House : SalesObject
    {
        [Required]
        public double PlotArea { get; set; }
        [Required]
        public double YearlyCost { get; set; }


        protected House(int price) : base(price)
        {

        }

    }
}
