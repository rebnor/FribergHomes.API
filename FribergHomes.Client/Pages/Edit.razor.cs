using FribergHomes.Client.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Reflection.Metadata;

namespace FribergHomes.Client.Pages
{
    //Author: Tobias 2024-05-06
    // Update: Nu kan man editera allt och man får upp Alert-box som säger att det funkade / Reb 2024-05-19

    public partial class Edit
    {
        [Parameter]
        public string? ObjectType { get; set; }

        [Parameter]
        public string Id { get; set; }

        private string? _category;

        private List<string>? _categoryIconNames = new();

        public SalesObjectDTO? SalesObject { get; set; }

        public RealtorDTO? Realtor { get; set; }

        public AgencyDTO? Agency { get; set; }

        public CategoryDTO? Category { get; set; }

        private int intId;
        private Guid guidId;
        private bool isEdited = false;


        protected override async Task OnParametersSetAsync()
        {
            bool isInt = int.TryParse(Id, out intId);
            bool isGuid = Guid.TryParse(Id, out guidId);

            switch (ObjectType)
            {
                case "salesobject":
                    if (isInt)
                    {
                        SalesObject = await SalesObjectService.Get(intId);
                        _category = SalesObject.CategoryName;
                    }
                    break;

                case "realtor":
                    if (isGuid)
                    {
                        Realtor = await RealtorService.GetRealtorByIdAsync(Id);
                        StateHasChanged();
                    }
                    break;

                case "agency":
                    if (isInt)
                    {
                        Agency = await AgencyService.GetAgencyByIdAsync(intId);
                    }
                    break;

                case "category":
                    if (isInt)
                    {
                        var categories = await CategoryService.GetAllCategoriesAsync();
                        if (categories != null)
                        {
                            foreach (var category in categories)
                            {
                                _categoryIconNames!.Add(category.IconUrl);
                            }

                            Category = await CategoryService.GetCategoryByIdAsync(intId);
                        }
                    }
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
                case "salesobject":
                    SalesObject!.ChangeDate = DateTime.Now;
                    SalesObject!.ChangeName = await AuthService.GetUserName();
                    await SalesObjectService.Update(intId, SalesObject!);
                    isEdited = true;
                    break;

                case "realtor":
                    Realtor = await RealtorService.UpdateRealtorAsync(Realtor!);
                    isEdited = true;
                    break;

                case "agency":

                    Agency = await AgencyService.UpdateAgencyAsync(intId, Agency!);
                    isEdited = true;
                    break;

                case "_category":

                    Category = await CategoryService.UpdateCategoryAsync(Category!);
                    isEdited = true;
                    break;


                    //case "county":

                    //    County = await CountyService.UpdateCountyAsync(County!);
                    //    break;
            }
        }

    }
}
