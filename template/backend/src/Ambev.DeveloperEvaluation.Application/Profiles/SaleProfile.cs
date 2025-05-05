using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Profiles
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            // Map de entrada (Request → Domain)
            CreateMap<CreateSaleItemRequest, SaleItem>();
            CreateMap<CreateSaleRequest, Sale>();

            // Map de saída (Domain → Response)
            CreateMap<SaleItem, SaleItemResponse>();
            CreateMap<Sale, SaleResponse>();
        }
    }
}
