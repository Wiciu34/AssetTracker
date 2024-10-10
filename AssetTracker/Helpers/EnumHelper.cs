using Microsoft.AspNetCore.Mvc.Rendering;

namespace AssetTracker.Helpers;

public static class EnumHelper
{
    public static List<SelectListItem> GetSelectListItems<TEnum>() where TEnum : Enum
    {
        var list = new List<SelectListItem>();

        foreach (var value in Enum.GetValues(typeof(TEnum)))
        {
            list.Add(new SelectListItem
            {
                Text = Enum.GetName(typeof(TEnum), value),
                Value = ((int)value).ToString()
            });
        }

        return list;
    }
}
