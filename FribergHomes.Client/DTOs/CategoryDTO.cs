﻿using System.ComponentModel.DataAnnotations;

namespace FribergHomes.Client.DTOs
{
    /* Class for Category.
     * IconUrl should be a https-link of an Icon, example FontAwesome.
     * @ Author: Rebecka 2024-04-15
     */
    public class CategoryDTO
    {
        public int Id { get; set; }
        [Required]
        [Length(3, 30, ErrorMessage = "Kategorinamnet måste bestå av minst 3 och max 30 bokstäver")]
        [RegularExpression(@"^[a-zA-ZåäöÅÄÖ\s-]+$", ErrorMessage = "Kategorinamnet får enbart innehålla a-ö, bindestreck och mellanslag")]
        public string Name { get; set; } = string.Empty;
        public string IconUrl { get; set; } = string.Empty;

    }
}
