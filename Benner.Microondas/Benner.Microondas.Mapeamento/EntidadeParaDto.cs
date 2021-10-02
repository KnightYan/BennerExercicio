using AutoMapper;
using Benner.Microondas.Model.Dto;
using Benner.Microondas.Model.Entidades;

namespace Benner.Microondas.Mapeamento
{
    public class EntidadeParaDto : Profile
    {
        public EntidadeParaDto()
        {
            CreateMap<ProgramaEntidade, ProgramaDto>()
                .ReverseMap();
        }
    }
}
