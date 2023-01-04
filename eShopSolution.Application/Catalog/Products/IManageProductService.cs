using eShopSolution.ViewModels.Catalog.ProductImage;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Products
{
    public interface IManageProductService
    {
        Task<int> Create(ProductCreateRequest request);

        Task<int> Update(ProductUpdateRequest request);

        Task<int> Delete(int productId);

        Task<ProductViewModel> GetById(int ProductId , string languageId);

        Task<bool> UpdatePrice(int productId, decimal newPrice);

        Task<bool> UpdateStock(int productId, int addedQuantity);

        Task AddViewcount(int productId);

        Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);


        //ProductImage
        Task<int> AddImages(int productId, ProductImageCreateRequest request);

        Task<int> RemoveImage(int imageId);

        Task<int> UpdateImage(ProductImageUpdateRequest request);

        Task<List<ProductImageViewModel>> GetListImages(int productId);

        Task<ProductImageViewModel> GetImageById(int imageId);
    }
}
