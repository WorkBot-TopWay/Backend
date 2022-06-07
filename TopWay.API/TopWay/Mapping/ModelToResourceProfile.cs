using AutoMapper;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Scaler, ScalerResource>();
        CreateMap<ClimbingGyms, ClimbingGymResource>();
        CreateMap<Notification, NotificationResource>();
        CreateMap<Categories, CategoriesResource>();
        CreateMap<CategoryGyms, CategoryGymResource>();
        CreateMap<Images, ImagesResource>();
        CreateMap<CompetitionGyms, CompetitionGymResource>();
        CreateMap<Comments, CommentResource>();
        CreateMap<CompetitionReservationClimber, CompetitionReservationClimberResource>();
        CreateMap<CompetitionGymRankings, CompetitionGymRankingResource>();
        CreateMap<League, LeagueResource>();
        CreateMap<Request, RequestResource>();
        CreateMap<ClimberLeagues, ClimbersLeagueResource>();
        CreateMap<CompetitionLeague, CompetitionLeagueResource>();
        CreateMap<CompetitionLeagueRanking, CompetitionLeagueRankingResource>();
        CreateMap<Favorite, FavoriteResource>();
        CreateMap<Features, FeaturesResource>();
        CreateMap<News,NewsResource>();
    }
}