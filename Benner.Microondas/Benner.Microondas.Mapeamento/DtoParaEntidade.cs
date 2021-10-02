using AutoMapper;
using Benner.Microondas.Model.Dto;
using Benner.Microondas.Model.Entidades;

namespace Benner.Microondas.Mapeamento
{
    public class DtoParaEntidade : Profile
    {
        public DtoParaEntidade()
        {
            CreateMap<ProgramaDto, ProgramaEntidade>()
                .ReverseMap();
        }
    }
}
