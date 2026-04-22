using CommunityToolkit.Maui.Views;

namespace MyGold5.Pages.Popups;

public partial class FilterByCategoryPopup : Popup
{
	public FilterByCategoryPopup(FilterByCategoryPopupPageModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}
}