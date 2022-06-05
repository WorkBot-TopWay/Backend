﻿using AutoMapper;
using TopWay.API.TopWay.Domain.Models;
using TopWay.API.TopWay.Resources;

namespace TopWay.API.TopWay.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveScalerResource, Scaler>();
        CreateMap<SaveClimbingGymResource, ClimbingGym>();
        CreateMap<SaveNotificationResource, Notification>();
        CreateMap<SaveCategoryResource, Category>();
        CreateMap<SaveCategoryGymResource, CategoryGym>();
        CreateMap<SaveImagesResource, Images>();
        CreateMap<SaveCompetitionGymResource, CompetitionGym>();
        CreateMap<SaveCommentResource, Comment>();
    }
}