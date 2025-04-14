using Ambev.OrderFlow.Application.DTOs;
using Ambev.OrderFlow.Domain.Entities;
using AutoMapper;

namespace Ambev.OrderFlow.Application.Profiles
{
    public class PedidoProfile : Profile
    {
        public PedidoProfile()
        {
            CreateMap<Pedido, PedidoDTO>();
            CreateMap<ItemPedido, ItemPedidoDTO>();

            CreateMap<PedidoDTO, Pedido>();
            CreateMap<ItemPedidoDTO, ItemPedido>();
        }
    }
}
