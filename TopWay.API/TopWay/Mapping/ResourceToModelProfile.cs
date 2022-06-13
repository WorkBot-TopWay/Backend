using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveScalerResource, Scaler>();
        CreateMap<SaveClimbingGymResource, ClimbingGyms>();
        CreateMap<SaveNotificationResource, Notification>();
        CreateMap<SaveCategoryResource, Categories>();
        CreateMap<SaveCategoryGymResource, CategoryGyms>();
        CreateMap<SaveImagesResource, Images>();
        CreateMap<SaveCompetitionGymResource, CompetitionGyms>();
        CreateMap<SaveCommentResource, Comments>();
        CreateMap<SaveCompetitionReservationClimberResource, CompetitionReservationClimber>();
        CreateMap<SaveCompetitionGymRankingResource, CompetitionGymRankings>();
        CreateMap<SaveLeagueResource, League>();
        CreateMap<SaveRequestResource, Request>();
        CreateMap<SaveClimbersLeagueResource, ClimberLeagues>();
        CreateMap<SaveCompetitionLeagueResource, CompetitionLeague>();
        CreateMap<SaveCompetitionLeagueRankingResource, CompetitionLeagueRanking>();
        CreateMap<SaveFavoriteResource, Favorite>();
        CreateMap<SaveFeaturesResource, Features>();
        CreateMap<SaveNewsResource, News>();
    }
}