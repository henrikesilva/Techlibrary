using AutoMapper;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;

namespace TechLibrary.Application.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestUserJson, Domain.Entities.User>();
    }

    private void EntityToResponse()
    {
        CreateMap<Domain.Entities.User, ResponseRegisteredUserJson>();
        
        CreateMap<Domain.Entities.Book, ResponseBookJson>();
        CreateMap<Domain.Entities.Pagination, ResponsePaginationJson>();
    }
}
