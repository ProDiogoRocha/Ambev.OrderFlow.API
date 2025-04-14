using Ambev.OrderFlow.Application.DTOs;
using Ambev.OrderFlow.Domain.Aggregates;
using Ambev.OrderFlow.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.OrderFlow.Application.Profiles
{
    public class RevendaProfile : Profile
    {
        public RevendaProfile()
        {
            CreateMap<Revenda, RevendaDTO>();
            CreateMap<Telefone, TelefoneDTO>();
            CreateMap<Contato, ContatoDTO>();
            CreateMap<Endereco, EnderecoDTO>();

            CreateMap<RevendaDTO, Revenda>();
            CreateMap<TelefoneDTO, Telefone>();
            CreateMap<ContatoDTO, Contato>();
            CreateMap<EnderecoDTO, Endereco>();
        }
    }
}
