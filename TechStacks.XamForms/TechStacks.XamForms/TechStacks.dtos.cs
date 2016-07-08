/* Options:
Date: 2016-07-08 06:31:00
Version: 4.060
Tip: To override a DTO option, remove "//" prefix before updating
BaseUrl: http://techstacks.io

GlobalNamespace: TechStacks.XamForms
MakePartial: False
MakeVirtual: False
//MakeDataContractsExtensible: False
//AddReturnMarker: True
//AddDescriptionAsComments: True
//AddDataContractAttributes: False
//AddIndexesToDataMembers: False
//AddGeneratedCodeAttributes: False
//AddResponseStatus: False
//AddImplicitVersion: 
//InitializeCollections: True
//IncludeTypes: 
//ExcludeTypes: 
//AddDefaultXmlNamespace: http://schemas.servicestack.net/types
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ServiceStack;
using ServiceStack.DataAnnotations;
using TechStacks.XamForms;


namespace TechStacks.XamForms
{

    [Route("/tech")]
    public class ClientAllTechnologies
    {
    }

    [Route("/stacks")]
    public class ClientAllTechnologyStacks
    {
    }

    [Route("/tech/{Slug}")]
    public class ClientTechnology
    {
        public string Slug { get; set; }
    }

    [Route("/users/{UserName}")]
    public class ClientUser
    {
        public string UserName { get; set; }
    }

    [Route("/{PathInfo*}")]
    public class FallbackForClientRoutes
    {
        public string PathInfo { get; set; }
    }

    [Route("/ping")]
    public class Ping
    {
    }

    [Route("/favorites/technology/{TechnologyId}", "PUT")]
    public class AddFavoriteTechnology
        : IReturn<FavoriteTechnologyResponse>
    {
        public int TechnologyId { get; set; }
    }

    [Route("/favorites/techtacks/{TechnologyStackId}", "PUT")]
    public class AddFavoriteTechStack
        : IReturn<FavoriteTechStackResponse>
    {
        public int TechnologyStackId { get; set; }
    }

    [Route("/app-overview")]
    public class AppOverview
        : IReturn<AppOverviewResponse>
    {
        public bool Reload { get; set; }
    }

    public class AppOverviewResponse
    {
        public AppOverviewResponse()
        {
            AllTiers = new List<Option>{};
            TopTechnologies = new List<TechnologyInfo>{};
        }

        public DateTime Created { get; set; }
        public List<Option> AllTiers { get; set; }
        public List<TechnologyInfo> TopTechnologies { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/technology", "POST")]
    public class CreateTechnology
        : IReturn<CreateTechnologyResponse>
    {
        public string Name { get; set; }
        public string VendorName { get; set; }
        public string VendorUrl { get; set; }
        public string ProductUrl { get; set; }
        public string LogoUrl { get; set; }
        public string Description { get; set; }
        public bool IsLocked { get; set; }
        public string Tier { get; set; }
    }

    public class CreateTechnologyResponse
    {
        public Technology Result { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/techstacks", "POST")]
    public class CreateTechnologyStack
        : IReturn<CreateTechnologyStackResponse>
    {
        public CreateTechnologyStack()
        {
            TechnologyIds = new List<long>{};
        }

        public string Name { get; set; }
        public string VendorName { get; set; }
        public string AppUrl { get; set; }
        public string ScreenshotUrl { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public bool IsLocked { get; set; }
        public List<long> TechnologyIds { get; set; }
    }

    public class CreateTechnologyStackResponse
    {
        public TechStackDetails Result { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/technology/{Id}", "DELETE")]
    public class DeleteTechnology
        : IReturn<DeleteTechnologyResponse>
    {
        public long Id { get; set; }
    }

    public class DeleteTechnologyResponse
    {
        public Technology Result { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/techstacks/{Id}", "DELETE")]
    public class DeleteTechnologyStack
        : IReturn<DeleteTechnologyStackResponse>
    {
        public long Id { get; set; }
    }

    public class DeleteTechnologyStackResponse
    {
        public TechStackDetails Result { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class FavoriteTechnologyResponse
    {
        public Technology Result { get; set; }
    }

    public class FavoriteTechStackResponse
    {
        public TechnologyStack Result { get; set; }
    }

    [Route("/technology/search")]
    [AutoQueryViewer(Title="Find Technologies", Description="Explore different Technologies", IconUrl="octicon:database", DefaultSearchField="Tier", DefaultSearchType="=", DefaultSearchText="Data")]
    public class FindTechnologies
        : QueryDb<Technology>, IReturn<QueryResponse<Technology>>
    {
        public string Name { get; set; }
        public string NameContains { get; set; }
    }

    [Route("/admin/technology/search")]
    [AutoQueryViewer(Title="Find Technologies Admin", Description="Explore different Technologies", IconUrl="octicon:database", DefaultSearchField="Tier", DefaultSearchType="=", DefaultSearchText="Data")]
    public class FindTechnologiesAdmin
        : QueryDb<Technology>, IReturn<QueryResponse<Technology>>
    {
        public string Name { get; set; }
    }

    [Route("/techstacks/search")]
    [AutoQueryViewer(Title="Find Technology Stacks", Description="Explore different Technology Stacks", IconUrl="material-icons:cloud", DefaultSearchField="Description", DefaultSearchType="Contains", DefaultSearchText="ServiceStack")]
    public class FindTechStacks
        : QueryDb<TechnologyStack>, IReturn<QueryResponse<TechnologyStack>>
    {
        public string NameContains { get; set; }
    }

    [Route("/technology", "GET")]
    public class GetAllTechnologies
        : IReturn<GetAllTechnologiesResponse>
    {
    }

    public class GetAllTechnologiesResponse
    {
        public GetAllTechnologiesResponse()
        {
            Results = new List<Technology>{};
        }

        public List<Technology> Results { get; set; }
    }

    [Route("/techstacks", "GET")]
    public class GetAllTechnologyStacks
        : IReturn<GetAllTechnologyStacksResponse>
    {
    }

    public class GetAllTechnologyStacksResponse
    {
        public GetAllTechnologyStacksResponse()
        {
            Results = new List<TechnologyStack>{};
        }

        public List<TechnologyStack> Results { get; set; }
    }

    [Route("/config")]
    public class GetConfig
        : IReturn<GetConfigResponse>
    {
    }

    public class GetConfigResponse
    {
        public GetConfigResponse()
        {
            AllTiers = new List<Option>{};
        }

        public List<Option> AllTiers { get; set; }
    }

    [Route("/favorites/technology", "GET")]
    public class GetFavoriteTechnologies
        : IReturn<GetFavoriteTechnologiesResponse>
    {
        public int TechnologyId { get; set; }
    }

    public class GetFavoriteTechnologiesResponse
    {
        public GetFavoriteTechnologiesResponse()
        {
            Results = new List<Technology>{};
        }

        public List<Technology> Results { get; set; }
    }

    [Route("/favorites/techtacks", "GET")]
    public class GetFavoriteTechStack
        : IReturn<GetFavoriteTechStackResponse>
    {
        public int TechnologyStackId { get; set; }
    }

    public class GetFavoriteTechStackResponse
    {
        public GetFavoriteTechStackResponse()
        {
            Results = new List<TechnologyStack>{};
        }

        public List<TechnologyStack> Results { get; set; }
    }

    [Route("/pagestats/{Type}/{Slug}")]
    public class GetPageStats
        : IReturn<GetPageStatsResponse>
    {
        public string Type { get; set; }
        public string Slug { get; set; }
    }

    public class GetPageStatsResponse
    {
        public string Type { get; set; }
        public string Slug { get; set; }
        public long ViewCount { get; set; }
        public long FavCount { get; set; }
    }

    [Route("/technology/{Slug}")]
    public class GetTechnology
        : IReturn<GetTechnologyResponse>
    {
        public string Slug { get; set; }
    }

    [Route("/technology/{Slug}/favorites")]
    public class GetTechnologyFavoriteDetails
        : IReturn<GetTechnologyFavoriteDetailsResponse>
    {
        public string Slug { get; set; }
    }

    public class GetTechnologyFavoriteDetailsResponse
    {
        public GetTechnologyFavoriteDetailsResponse()
        {
            Users = new List<string>{};
        }

        public List<string> Users { get; set; }
        public int FavoriteCount { get; set; }
    }

    [Route("/technology/{Slug}/previous-versions", "GET")]
    public class GetTechnologyPreviousVersions
        : IReturn<GetTechnologyPreviousVersionsResponse>
    {
        public string Slug { get; set; }
    }

    public class GetTechnologyPreviousVersionsResponse
    {
        public GetTechnologyPreviousVersionsResponse()
        {
            Results = new List<TechnologyHistory>{};
        }

        public List<TechnologyHistory> Results { get; set; }
    }

    public class GetTechnologyResponse
    {
        public GetTechnologyResponse()
        {
            TechnologyStacks = new List<TechnologyStack>{};
        }

        public DateTime Created { get; set; }
        public Technology Technology { get; set; }
        public List<TechnologyStack> TechnologyStacks { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/techstacks/{Slug}", "GET")]
    public class GetTechnologyStack
        : IReturn<GetTechnologyStackResponse>
    {
        public string Slug { get; set; }
    }

    [Route("/techstacks/{Slug}/favorites")]
    public class GetTechnologyStackFavoriteDetails
        : IReturn<GetTechnologyStackFavoriteDetailsResponse>
    {
        public string Slug { get; set; }
    }

    public class GetTechnologyStackFavoriteDetailsResponse
    {
        public GetTechnologyStackFavoriteDetailsResponse()
        {
            Users = new List<string>{};
        }

        public List<string> Users { get; set; }
        public int FavoriteCount { get; set; }
    }

    [Route("/techstacks/{Slug}/previous-versions", "GET")]
    public class GetTechnologyStackPreviousVersions
        : IReturn<GetTechnologyStackPreviousVersionsResponse>
    {
        public string Slug { get; set; }
    }

    public class GetTechnologyStackPreviousVersionsResponse
    {
        public GetTechnologyStackPreviousVersionsResponse()
        {
            Results = new List<TechnologyStackHistory>{};
        }

        public List<TechnologyStackHistory> Results { get; set; }
    }

    public class GetTechnologyStackResponse
    {
        public DateTime Created { get; set; }
        public TechStackDetails Result { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/my-feed")]
    public class GetUserFeed
        : IReturn<GetUserFeedResponse>
    {
    }

    public class GetUserFeedResponse
    {
        public GetUserFeedResponse()
        {
            Results = new List<TechStackDetails>{};
        }

        public List<TechStackDetails> Results { get; set; }
    }

    [Route("/userinfo/{UserName}")]
    public class GetUserInfo
        : IReturn<GetUserInfoResponse>
    {
        public string UserName { get; set; }
    }

    public class GetUserInfoResponse
    {
        public GetUserInfoResponse()
        {
            TechStacks = new List<TechnologyStack>{};
            FavoriteTechStacks = new List<TechnologyStack>{};
            FavoriteTechnologies = new List<Technology>{};
        }

        public string UserName { get; set; }
        public DateTime Created { get; set; }
        public string AvatarUrl { get; set; }
        public List<TechnologyStack> TechStacks { get; set; }
        public List<TechnologyStack> FavoriteTechStacks { get; set; }
        public List<Technology> FavoriteTechnologies { get; set; }
    }

    public class LockStackResponse
    {
    }

    [Route("/admin/technology/{TechnologyId}/lock")]
    public class LockTech
        : IReturn<LockStackResponse>
    {
        public long TechnologyId { get; set; }
        public bool IsLocked { get; set; }
    }

    [Route("/admin/techstacks/{TechnologyStackId}/lock")]
    public class LockTechStack
        : IReturn<LockStackResponse>
    {
        public long TechnologyStackId { get; set; }
        public bool IsLocked { get; set; }
    }

    [Route("/admin/technology/{TechnologyId}/logo")]
    public class LogoUrlApproval
        : IReturn<LogoUrlApprovalResponse>
    {
        public long TechnologyId { get; set; }
        public bool Approved { get; set; }
    }

    public class LogoUrlApprovalResponse
    {
        public Technology Result { get; set; }
    }

    [DataContract]
    public class Option
    {
        [DataMember(Name="name")]
        public string Name { get; set; }

        [DataMember(Name="title")]
        public string Title { get; set; }

        [DataMember(Name="value")]
        public TechnologyTier? Value { get; set; }
    }

    [Route("/overview")]
    public class Overview
        : IReturn<OverviewResponse>
    {
        public bool Reload { get; set; }
    }

    public class OverviewResponse
    {
        public OverviewResponse()
        {
            TopUsers = new List<UserInfo>{};
            TopTechnologies = new List<TechnologyInfo>{};
            LatestTechStacks = new List<TechStackDetails>{};
            PopularTechStacks = new List<TechnologyStack>{};
            TopTechnologiesByTier = new Dictionary<TechnologyTier, List<TechnologyInfo>>{};
        }

        public DateTime Created { get; set; }
        public List<UserInfo> TopUsers { get; set; }
        public List<TechnologyInfo> TopTechnologies { get; set; }
        public List<TechStackDetails> LatestTechStacks { get; set; }
        public List<TechnologyStack> PopularTechStacks { get; set; }
        public Dictionary<TechnologyTier, List<TechnologyInfo>> TopTechnologiesByTier { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/favorites/technology/{TechnologyId}", "DELETE")]
    public class RemoveFavoriteTechnology
        : IReturn<FavoriteTechnologyResponse>
    {
        public int TechnologyId { get; set; }
    }

    [Route("/favorites/techtacks/{TechnologyStackId}", "DELETE")]
    public class RemoveFavoriteTechStack
        : IReturn<FavoriteTechStackResponse>
    {
        public int TechnologyStackId { get; set; }
    }

    [Route("/my-session")]
    public class SessionInfo
    {
    }

    public class TechnologyInfo
    {
        public string Tier { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public int StacksCount { get; set; }
    }

    public class TechnologyInStack
        : TechnologyBase
    {
        public long TechnologyId { get; set; }
        public long TechnologyStackId { get; set; }
        public string Justification { get; set; }
    }

    public class TechStackDetails
        : TechnologyStackBase
    {
        public TechStackDetails()
        {
            TechnologyChoices = new List<TechnologyInStack>{};
        }

        public string DetailsHtml { get; set; }
        public List<TechnologyInStack> TechnologyChoices { get; set; }
    }

    [Route("/technology/{Id}", "PUT")]
    public class UpdateTechnology
        : IReturn<UpdateTechnologyResponse>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string VendorName { get; set; }
        public string VendorUrl { get; set; }
        public string ProductUrl { get; set; }
        public string LogoUrl { get; set; }
        public string Description { get; set; }
        public bool IsLocked { get; set; }
        public string Tier { get; set; }
    }

    public class UpdateTechnologyResponse
    {
        public Technology Result { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/techstacks/{Id}", "PUT")]
    public class UpdateTechnologyStack
        : IReturn<UpdateTechnologyStackResponse>
    {
        public UpdateTechnologyStack()
        {
            TechnologyIds = new List<long>{};
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string VendorName { get; set; }
        public string AppUrl { get; set; }
        public string ScreenshotUrl { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public bool IsLocked { get; set; }
        public List<long> TechnologyIds { get; set; }
    }

    public class UpdateTechnologyStackResponse
    {
        public TechStackDetails Result { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class UserInfo
    {
        public string UserName { get; set; }
        public string AvatarUrl { get; set; }
        public int StacksCount { get; set; }
    }

    public class Technology
        : TechnologyBase
    {
    }

    public class TechnologyBase
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string VendorName { get; set; }
        public string VendorUrl { get; set; }
        public string ProductUrl { get; set; }
        public string LogoUrl { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastModified { get; set; }
        public string LastModifiedBy { get; set; }
        public string OwnerId { get; set; }
        public string Slug { get; set; }
        public bool LogoApproved { get; set; }
        public bool IsLocked { get; set; }
        public string Tier { get; set; }
        public DateTime? LastStatusUpdate { get; set; }
    }

    public class TechnologyHistory
        : TechnologyBase
    {
        public long TechnologyId { get; set; }
        public string Operation { get; set; }
    }

    public class TechnologyStack
        : TechnologyStackBase
    {
    }

    public class TechnologyStackBase
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string VendorName { get; set; }
        public string Description { get; set; }
        public string AppUrl { get; set; }
        public string ScreenshotUrl { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastModified { get; set; }
        public string LastModifiedBy { get; set; }
        public bool IsLocked { get; set; }
        public string OwnerId { get; set; }
        public string Slug { get; set; }
        public string Details { get; set; }
        public DateTime? LastStatusUpdate { get; set; }
    }

    public class TechnologyStackHistory
        : TechnologyStackBase
    {
        public TechnologyStackHistory()
        {
            TechnologyIds = new List<long>{};
        }

        public long TechnologyStackId { get; set; }
        public string Operation { get; set; }
        public List<long> TechnologyIds { get; set; }
    }

    public enum TechnologyTier
    {
        ProgrammingLanguage,
        Client,
        Http,
        Server,
        Data,
        SoftwareInfrastructure,
        OperatingSystem,
        HardwareInfrastructure,
        ThirdPartyServices,
    }
}

