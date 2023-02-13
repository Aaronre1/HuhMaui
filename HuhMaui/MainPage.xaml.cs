using System.Diagnostics;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Maps;

namespace HuhMaui;

public partial class MainPage : ContentPage
{
    public Location location;

	public MainPage()
	{
		InitializeComponent();
		
	}

    public async Task GetLocation()
    {
        try
        {
            var cache = await Geolocation.Default.GetLastKnownLocationAsync();

            if (cache != null)
            {
                location = cache;
            }
            else
            {
                location = await Geolocation.Default.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Best,
                    Timeout = TimeSpan.FromSeconds(30)
                });
            }

            if (location != null)
            {
                var mapSpan = new MapSpan(location, 0.01, 0.01);
                this.map.MoveToRegion(mapSpan);
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine($"GM: Can't query location: {e.Message}");
        }
    }

    async void map_Loaded(System.Object sender, System.EventArgs e)
    {
        await GetLocation();
    }

    async void map_MapClicked(System.Object sender, Microsoft.Maui.Controls.Maps.MapClickedEventArgs e)
    {
        await GetLocation();
    }
}

