namespace SimpleWallet.Application.Mappings;

using AutoMapper;
using SimpleWallet.Application.DTOs;
using SimpleWallet.Domain.Entities;
using SimpleWallet.Domain.Enums;

public class GeneralProfile : Profile
{
    public GeneralProfile()
    {
        #region Wallet
        CreateMap<Wallet, WalletDto>()
            .ForMember(dest => dest.DocumentId, opt => opt.MapFrom(src => src.DocumentId))
            .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentType))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Balance));

        CreateMap<WalletCreateDto, Wallet>()
            .ForMember(dest => dest.DocumentId, opt => opt.MapFrom(src => src.DocumentId))
            .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentType))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Balance));

        CreateMap<WalletUpdateDto, Wallet>()
            .ForMember(dest => dest.DocumentId, opt => opt.MapFrom(src => src.DocumentId))
            .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentType))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        #endregion

        #region Movement
        CreateMap<Movement, MovementDto>()
            .ForMember(dest => dest.WalletId, opt => opt.MapFrom(src => src.WalletId))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));

        CreateMap<MovementCreateDto, Movement>()
            .ForMember(dest => dest.WalletId, opt => opt.MapFrom(src => src.WalletId))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));

        CreateMap<MovementUpdateDto, Movement>()
            .ForMember(dest => dest.WalletId, opt => opt.MapFrom(src => src.WalletId))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));
        #endregion
    }
}