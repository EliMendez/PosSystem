using AutoMapper;
using PosSystem.Dto.Dto;
using PosSystem.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Service.Mapping
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            #region Role
            // Mapping Role to RoleDto
            CreateMap<Role, RoleDto>()
                .ForMember(
                    destination => destination.RoleId,
                    opt => opt.MapFrom(origin => origin.RoleId)
                )
                .ForMember(
                    destination => destination.Description,
                    opt => opt.MapFrom(origin => origin.Description)
                )
                .ForMember(
                    destination => destination.Status,
                    opt => opt.MapFrom(origin => origin.Status)
                );

            // Mapping RoleDto to Role
            CreateMap<RoleDto, Role>()
                .ForMember(
                    destination => destination.RoleId,
                    opt => opt.MapFrom(origin => origin.RoleId)
                )
                .ForMember(
                    destination => destination.Description,
                    opt => opt.MapFrom(origin => origin.Description)
                )
                .ForMember(
                    destination => destination.Status,
                    opt => opt.MapFrom(origin => origin.Status)
                );
            #endregion

            #region User
            // Mapping User to UserDto
            CreateMap<User, UserDto>()
                .ForMember(
                    destination => destination.UserId,
                    opt => opt.MapFrom(origin => origin.UserId)
                )
                .ForMember(
                    destination => destination.Name,
                    opt => opt.MapFrom(origin => origin.Name)
                )
                .ForMember(
                    destination => destination.Surname,
                    opt => opt.MapFrom(origin => origin.Surname)
                )
                .ForMember(
                    destination => destination.RoleDescription,
                    opt => opt.MapFrom(origin => origin.Role != null ? origin.Role.Description : string.Empty)
                )
                .ForMember(
                    destination => destination.Phone,
                    opt => opt.MapFrom(origin => origin.Phone)
                )
                .ForMember(
                    destination => destination.Email,
                    opt => opt.MapFrom(origin => origin.Email)
                )
                .ForMember(
                    destination => destination.Password,
                    opt => opt.Ignore() // It will be ignored because the key cannot be exposed on the frontend.
                )
                .ForMember(
                    destination => destination.Status,
                    opt => opt.MapFrom(origin => origin.Status)
                );

            // Mapping UserDto to User
            CreateMap<UserDto, User>()
                .ForMember(
                    destination => destination.UserId,
                    opt => opt.MapFrom(origin => origin.UserId)
                )
                .ForMember(
                    destination => destination.Name,
                    opt => opt.MapFrom(origin => origin.Name)
                )
                .ForMember(
                    destination => destination.Surname,
                    opt => opt.MapFrom(origin => origin.Surname)
                )
                .ForMember(
                    destination => destination.RoleId,
                    opt => opt.MapFrom(origin => origin.RoleId)
                )
                .ForMember(
                    destination => destination.Phone,
                    opt => opt.MapFrom(origin => origin.Phone)
                )
                .ForMember(
                    destination => destination.Email,
                    opt => opt.MapFrom(origin => origin.Email)
                )
                .ForMember(
                    destination => destination.Status,
                    opt => opt.MapFrom(origin => origin.Status)
                );
            #endregion

            #region Business
            // Mapping Business to BusinessDto
            CreateMap<Business, BusinessDto>()
                .ForMember(
                    destination => destination.BusinessId,
                    opt => opt.MapFrom(origin => origin.BusinessId)
                )
                .ForMember(
                    destination => destination.Ruc,
                    opt => opt.MapFrom(origin => origin.Ruc)
                )
                .ForMember(
                    destination => destination.CompanyName,
                    opt => opt.MapFrom(origin => origin.CompanyName)
                )
                .ForMember(
                    destination => destination.Email,
                    opt => opt.MapFrom(origin => origin.Email)
                )
                .ForMember(
                    destination => destination.Phone,
                    opt => opt.MapFrom(origin => origin.Phone)
                )
                .ForMember(
                    destination => destination.Address,
                    opt => opt.MapFrom(origin => origin.Address)
                )
                .ForMember(
                    destination => destination.Owner,
                    opt => opt.MapFrom(origin => origin.Owner)
                )
                .ForMember(
                    destination => destination.Discount,
                    opt => opt.MapFrom(origin => origin.Discount)
                );

            // Mapping BusinessDto to Business
            CreateMap<BusinessDto, Business>()
                .ForMember(
                    destination => destination.BusinessId,
                    opt => opt.MapFrom(origin => origin.BusinessId)
                )
                .ForMember(
                    destination => destination.Ruc,
                    opt => opt.MapFrom(origin => origin.Ruc)
                )
                .ForMember(
                    destination => destination.CompanyName,
                    opt => opt.MapFrom(origin => origin.CompanyName)
                )
                .ForMember(
                    destination => destination.Email,
                    opt => opt.MapFrom(origin => origin.Email)
                )
                .ForMember(
                    destination => destination.Phone,
                    opt => opt.MapFrom(origin => origin.Phone)
                )
                .ForMember(
                    destination => destination.Address,
                    opt => opt.MapFrom(origin => origin.Address)
                )
                .ForMember(
                    destination => destination.Owner,
                    opt => opt.MapFrom(origin => origin.Owner)
                )
                .ForMember(
                    destination => destination.Discount,
                    opt => opt.MapFrom(origin => origin.Discount)
                );
            #endregion

            #region DocumentNumber
            // Mapping DocumentNumber to DocumentNumberDto
            CreateMap<DocumentNumber, DocumentNumberDto>()
                .ForMember(
                    destination => destination.DocumentNumberId,
                    opt => opt.MapFrom(origin => origin.DocumentNumberId)
                )
                .ForMember(
                    destination => destination.Document,
                    opt => opt.MapFrom(origin => origin.Document)
                );

            // Mapping DocumentNumberDto to DocumentNumber
            CreateMap<DocumentNumberDto, DocumentNumber>()
                .ForMember(
                    destination => destination.DocumentNumberId,
                    opt => opt.MapFrom(origin => origin.DocumentNumberId)
                )
                .ForMember(
                    destination => destination.Document,
                    opt => opt.MapFrom(origin => origin.Document)
                );
            #endregion

            #region Category
            // Mapping Category to CategoryDto
            CreateMap<Category, CategoryDto>()
                .ForMember(
                    destination => destination.CategoryId,
                    opt => opt.MapFrom(origin => origin.CategoryId)
                )
                .ForMember(
                    destination => destination.Description,
                    opt => opt.MapFrom(origin => origin.Description)
                )
                .ForMember(
                    destination => destination.Status,
                    opt => opt.MapFrom(origin => origin.Status)
                );

            // Mapping CategoryDto to Category
            CreateMap<CategoryDto, Category>()
                .ForMember(
                    destination => destination.CategoryId,
                    opt => opt.MapFrom(origin => origin.CategoryId)
                )
                .ForMember(
                    destination => destination.Description,
                    opt => opt.MapFrom(origin => origin.Description)
                )
                .ForMember(
                    destination => destination.Status,
                    opt => opt.MapFrom(origin => origin.Status)
                );
            #endregion

            #region Product
            // Mapping Product to ProductDto
            CreateMap<Product, ProductDto>()
                .ForMember(
                    destination => destination.ProductId,
                    opt => opt.MapFrom(origin => origin.ProductId)
                )
                .ForMember(
                    destination => destination.Description,
                    opt => opt.MapFrom(origin => origin.Description)
                )
                .ForMember(
                    destination => destination.Barcode,
                    opt => opt.MapFrom(origin => origin.Barcode)
                )
                .ForMember(
                    destination => destination.CategoryDescription,
                    opt => opt.MapFrom(origin => origin.Category != null ? origin.Category.Description : string.Empty)
                )
                .ForMember(
                    destination => destination.SalePrice,
                    opt => opt.MapFrom(origin => origin.SalePrice)
                )
                .ForMember(
                    destination => destination.Stock,
                    opt => opt.MapFrom(origin => origin.Stock)
                )
                .ForMember(
                    destination => destination.MinimumStock,
                    opt => opt.MapFrom(origin => origin.MinimumStock)
                )
                .ForMember(
                    destination => destination.Status,
                    opt => opt.MapFrom(origin => origin.Status)
                );

            // Mapping ProductDto to Product
            CreateMap<ProductDto, Product>()
                .ForMember(
                    destination => destination.ProductId,
                    opt => opt.MapFrom(origin => origin.ProductId)
                )
                .ForMember(
                    destination => destination.Description,
                    opt => opt.MapFrom(origin => origin.Description)
                )
                .ForMember(
                    destination => destination.Barcode,
                    opt => opt.MapFrom(origin => origin.Barcode)
                )
                .ForMember(
                    destination => destination.CategoryId,
                    opt => opt.MapFrom(origin => origin.CategoryId)
                )
                .ForMember(
                    destination => destination.SalePrice,
                    opt => opt.MapFrom(origin => origin.SalePrice)
                )
                .ForMember(
                    destination => destination.Stock,
                    opt => opt.MapFrom(origin => origin.Stock)
                )
                .ForMember(
                    destination => destination.MinimumStock,
                    opt => opt.MapFrom(origin => origin.MinimumStock)
                )
                .ForMember(
                    destination => destination.Status,
                    opt => opt.MapFrom(origin => origin.Status)
                );
            #endregion

            #region Sale
            // Mapping Sale to SaleDto
            CreateMap<Sale, SaleDto>()
                .ForMember(
                    destination => destination.SaleDate,
                    opt => opt.MapFrom(origin => origin.SaleDate)
                 )
                .ForMember(
                    destination => destination.Dni,
                    opt => opt.MapFrom(origin => origin.Dni)
                )
                .ForMember(
                    destination => destination.Customer,
                    opt => opt.MapFrom(origin => origin.Customer)
                )
                .ForMember(
                    destination => destination.Discount,
                    opt => opt.MapFrom(origin => origin.Discount)
                )
                .ForMember(
                    destination => destination.Total,
                    opt => opt.MapFrom(origin => origin.Total)
                )
                .ForMember(
                    destination => destination.UserId,
                    opt => opt.MapFrom(origin => origin.UserId)
                )
                .ForMember(
                    destination => destination.Status,
                    opt => opt.MapFrom(origin => origin.Status)
                )
                .ForMember(
                    destination => destination.Reason,
                    opt => opt.MapFrom(origin => origin.Reason)
                )
                .ForMember(
                    destination => destination.UserCancel,
                    opt => opt.MapFrom(origin => origin.UserCancel)
                );

            // Mapping SaleDto to Sale
            CreateMap<SaleDto, Sale>()
                .ForMember(
                    destination => destination.SaleId,
                    opt => opt.Ignore()
                )
                .ForMember(
                    destination => destination.SaleDate,
                    //opt => opt.MapFrom(origin => DateOnly.FromDateTime(origin.SaleDate))
                    opt => opt.MapFrom(origin => origin.SaleDate)
                )
                .ForMember(
                    destination => destination.SaleId,
                    opt => opt.MapFrom(origin => origin.SaleId)
                )
                .ForMember(
                    destination => destination.Dni,
                    opt => opt.MapFrom(origin => origin.Dni)
                )
                .ForMember(
                    destination => destination.Customer,
                    opt => opt.MapFrom(origin => origin.Customer)
                )
                .ForMember(
                    destination => destination.Discount,
                    opt => opt.MapFrom(origin => origin.Discount)
                )
                .ForMember(
                    destination => destination.Total,
                    opt => opt.MapFrom(origin => origin.Total)
                )
                .ForMember(
                    destination => destination.UserId,
                    opt => opt.MapFrom(origin => origin.UserId)
                )
                .ForMember(
                    destination => destination.Status,
                    opt => opt.MapFrom(origin => origin.Status)
                )
                .ForMember(
                    destination => destination.Reason,
                    opt => opt.MapFrom(origin => origin.Reason)
                )
                .ForMember(
                    destination => destination.AnnulledDate,
                    opt => opt.MapFrom(origin => origin.AnnulledDate!.Value)
                )
                .ForMember(
                    destination => destination.UserCancel,
                    opt => opt.MapFrom(origin => origin.UserCancel)
                );
            #endregion

            #region SaleDetail
            // Mapping SaleDetail to SaleDetailDto
            CreateMap<SaleDetail, SaleDetailDto>()
                .ForMember(
                    destination => destination.SaleId,
                    opt => opt.MapFrom(origin => origin.SaleId)
                )
                .ForMember(
                    destination => destination.ProductId,
                    opt => opt.MapFrom(origin => origin.ProductId)
                )
                .ForMember(
                    destination => destination.ProductName,
                    opt => opt.MapFrom(origin => origin.ProductName)
                )
                .ForMember(
                    destination => destination.Price,
                    opt => opt.MapFrom(origin => origin.Price)
                )
                .ForMember(
                    destination => destination.Quantity,
                    opt => opt.MapFrom(origin => origin.Quantity)
                )
                .ForMember(
                    destination => destination.Discount,
                    opt => opt.MapFrom(origin => origin.Discount)
                )
                .ForMember(
                    destination => destination.Total,
                    opt => opt.MapFrom(origin => origin.Total)
                );

            // Mapping SaleDetailDto to SaleDetail
            CreateMap<SaleDetailDto, SaleDetail>()
                .ForMember(
                    destination => destination.SaleDetailId,
                    opt => opt.Ignore()
                )
                .ForMember(
                    destination => destination.SaleId,
                    opt => opt.MapFrom(origin => origin.SaleId)
                )
                .ForMember(
                    destination => destination.ProductId,
                    opt => opt.MapFrom(origin => origin.ProductId)
                )
                .ForMember(
                    destination => destination.ProductName,
                    opt => opt.MapFrom(origin => origin.ProductName)
                )
                .ForMember(
                    destination => destination.Price,
                    opt => opt.MapFrom(origin => origin.Price)
                )
                .ForMember(
                    destination => destination.Quantity,
                    opt => opt.MapFrom(origin => origin.Quantity)
                )
                .ForMember(
                    destination => destination.Discount,
                    opt => opt.MapFrom(origin => origin.Discount)
                )
                .ForMember(
                    destination => destination.Total,
                    opt => opt.MapFrom(origin => origin.Total)
                );
            #endregion
        }
    }
}
