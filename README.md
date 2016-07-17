# TechStacksXamarin
This project is an example application showing the use of ServiceStack with a Xamarin.Forms Android and iOS client. Xamarin.Forms allows for maximum code reuse between iOS and Android platform application whilst enabling customization through styling or custom platform specific renderers. This combined with ServiceStack.Client allows for a strongly typed end to end solution where views can used ServiceStack reference in or as View Models.

[![TechStacks Xamarin.Forms app running on iOS and Android](https://raw.githubusercontent.com/ServiceStack/Assets/master/img/apps/TechStacksXamForms/video_preview.png)](https://www.youtube.com/watch?v=4ghchU3xKs4)

## Overview
This example was created using Visual Studio 2015's built in templates for Xamarin Forms. This generates the basis of a cross platform application for iOS and Android*.
> *This template also generates Windows Phone, UWP and Windows platform projects, however these require additional SDKs and can cause problems when creating the solution. This example just focuses on Android and iOS.

### Getting Started
The project template used to created this example was the `Blank Xaml App (Xamarin.Forms Portable)` which creates a shared Xamarin Forms project and then one for each platform (eg iOS and Android). 

![](https://raw.githubusercontent.com/ServiceStack/Assets/master/img/apps/TechStacksXamForms/solution.png)

Once the project is created, we'll want to add types that represent data coming from our ServiceStack services. This is when we can use the ServiceStackVS extension to add a ServiceStack Reference.

- Right click on the (Portable) shared project and select `Add ServiceStack Reference`.
- Enter the base URL of your ServiceStack endpoint and choose a name and click OK.

The above steps will download the C# types that are used for integrating with the ServiceStack endpoint. It will also include required ServiceStack NuGet dependencies to use the JsonServiceClient client and ServiceStack.Text serialization libraries required. These packages will also have to be added to the platform specific projects to ensure they are deployed correctly. To do this

- Right click on the platform specific project and select `Manage NuGet Packages`.
- Search for `ServiceStack.Client` and install.

Once these are added, we can start testing our clients using the JsonServiceClient.

### Fetching data from a ServiceStack endpoint
Since we our clients are on iOS and Android, it's more performant to use the `HttpClient` as opposed to the usual `WebRequest`/`WebResponse` classes when making the requests. ServiceStack provides a client for this use case in the `ServiceStack.HttpClient` NuGet package and the `JsonHttpClient` class. The `JsonHttpClient` is interchangeable with the `JsonServiceClient` so if you're already building your Xamarin.Forms application and have used the `JsonServiceClient`, you can add the `ServiceStack.Client` NuGet package and switch seamlessly.
> Remember to add NuGet dependencies to the platform specific projects as well as the shared one.

For ease of use, in this project the `JsonHttpClient` is wrapped in a static `AppUtils.ServiceClient` property to reuse the client through our client applications. 

``` csharp
public class AppUtils
{
	private static JsonHttpClient serviceClient;
	public static JsonHttpClient ServiceClient
	{
		get { return serviceClient ?? (serviceClient = new JsonHttpClient("http://techstacks.io/")); }
	}
}
```

Then in our views/pages, we can fetch data on creation.

``` csharp
private async Task InitData()
{
	var response = await AppUtils.ServiceClient.GetAsync(new AppOverview());
    TopTechsData = response.TopTechnologies;
    ...
}
```

#### Binding data
One way to take advantage of the strongly typed client on our Xamarin.Forms application is be using our ServiceStack reference types in our Views/Pages. Xamarin provides generic observable wrappers that can be bound to. For example, the TopRated technologies page, we create an `ObservableCollection<T>` for binding lists.

``` csharp
public ObservableCollection<TechnologyInfo> TopTechsDataSource { get; set; }
```

The generic `ObserableCollection<T>` is used as a `Datasource` for ListViews and other controls. To reference the object properties by name in XAML, you can ensure the correct `BindingContext` is set for your view by setting the `BindingContext` object to `this`, eg `this.BindingContext = this;` or local to a property, eg `this.BindingContext = this.technology;`. This way, binding to property values can be done in XAML. For ListViews, the `ItemSource` can be used to initialize binding. Eg,

``` csharp
...
this.ListView.ItemsSource = TopTechsDataSource;
...
```

``` XAML
<ListView x:Name="ListView">
  <ListView.ItemTemplate>
    <DataTemplate>
      <TextCell Text="{Binding Name}"></TextCell>
    </DataTemplate>
  </ListView.ItemTemplate>
</ListView>
```

