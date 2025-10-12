using AutoMapper;
using CurrencyServices.CurrencyFetcher.Domain.Entities;
using CurrencyServices.CurrencyFetcher.Infrastructure.Dtos;

namespace CurrencyServices.CurrencyFetcher.Infrastructure.Mappings;

public class CurrencyProfile : Profile
{
    public CurrencyProfile()
    {
        CreateMap<Valute, Currency>()
            .ForMember(c => c.Name, opt => opt.MapFrom(cur => cur.Name))
            .ForMember(c => c.Rate, opt => opt.MapFrom(cur => decimal.Parse(cur.Nominal)));

        CreateMap<CbrResponse, IEnumerable<Currency>>()
            .ConvertUsing((src, dest, context) =>
                context.Mapper.Map<IEnumerable<Currency>>(src.Valutes));
    }
}
