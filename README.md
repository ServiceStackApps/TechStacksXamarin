# TechStacksXamarin
This project is an example application showing the use of ServiceStack with a Xamarin.Forms client. Xamarin.Forms allows for maximum code reuse between iOS and Android platform application whilst enabling customization through styling or custom platform specific renderers. This combined with ServiceStack.Client allows for a strongly typed end to end solution where views can used ServiceStack reference in or as View Models.

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

### Using the JsonServiceClient 
