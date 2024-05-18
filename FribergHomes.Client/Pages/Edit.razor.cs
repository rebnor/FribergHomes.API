using FribergHomes.Client.DTOs;
using Microsoft.AspNetCore.Components;

namespace FribergHomes.Client.Pages
{
    //Author: Tobias 2024-05-06

    public partial class Edit
    {
        [Parameter]
        public string? ObjectType { get; set; }

        [Parameter]
        public int Id { get; set; }
        [Parameter]
        public string? StringId { get; set; }

        private string? _category;

        private List<string>? _categoryIconNames = new();

        public SalesObjectDTO? SalesObject { get; set; }

        public RealtorDTO? Realtor { get; set; }

        public AgencyDTO? Agency { get; set; }

        public CategoryDTO? Category { get; set; }

        //public CountyDTO? County { get; set; }


        protected override async Task OnParametersSetAsync()
        {
            switch (ObjectType)
            {
                case "salesObject":
                    SalesObject = await SalesObjectService.Get(Id);
                    _category = SalesObject.CategoryName;
                    break;

                case "realtor":

                    Realtor = await RealtorService.GetRealtorByIdAsync(StringId);
                    break;


                case "agency":
                    Agency = await AgencyService.GetAgencyByIdAsync(Id);
                    break;

                case "category":

                    var categories = await CategoryService.GetAllCategoriesAsync();
                    if (categories == null)
                    {
                        break;
                    }

                    foreach (var category in categories)
                    {
                        _categoryIconNames!.Add(category.IconUrl);
                    }

                    Category = await CategoryService.GetCategoryByIdAsync(Id);

                    break;

                    //case "county":

                    //    County = await CountyService.GetCountyByIdAsync(Id);
                    //    break;
            }
        }

        private void UpdatePreview()
        {
            StateHasChanged();
        }

        private async Task Submit()
        {
            switch (ObjectType)
            {
                case "salesObject":
                    SalesObject!.ChangeDate = DateTime.Now;
                    SalesObject!.ChangeName = await AuthService.GetUserName();

                    await SalesObjectService.Update(Id, SalesObject!);
                    NavMan.NavigateTo("/realtorprofile");

                    break;

                case "realtor":
                    if (!string.IsNullOrEmpty(StringId))
                        Realtor = await RealtorService.UpdateRealtorAsync(StringId, Realtor!);
                    break;

                case "agency":
                    Agency = await AgencyService.UpdateAgencyAsync(Id, Agency!);
                    break;

                case "_category":

                    Category = await CategoryService.UpdateCategoryAsync(Category!);
                    break;


                    //case "county":

                    //    County = await CountyService.UpdateCountyAsync(County!);
                    //    break;
            }
        }

    }
}
