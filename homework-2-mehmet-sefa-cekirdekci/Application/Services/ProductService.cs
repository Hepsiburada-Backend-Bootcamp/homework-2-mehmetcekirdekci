using Application.Models.ProductModels;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ServiceMessages;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public IResponse Add(ProductCreateDTO productCreateDTO)
        {
            var product = _mapper.Map<Product>(productCreateDTO);
            _productRepository.Add(product);

            return new SuccessResponse(Messages.ProductAdded);
        }

        public IResponse Delete(int productId)
        {
            var product = _productRepository.Get(p => p.ProductId == productId);
            _productRepository.Delete(product);

            return new SuccessResponse(Messages.ProductDeleted);
        }

        public IDataResponse<List<ProductDTO>> GetAll()
        {
            var products = _productRepository.GetList();
            var dtos = _mapper.Map<List<ProductDTO>>(products);
            return new SuccessDataResponse<List<ProductDTO>>(dtos);
        }

        public IDataResponse<ProductDTO> GetById(int productId)
        {
            var product = _productRepository.Get(p => p.ProductId == productId);
            var mappedProduct = _mapper.Map<ProductDTO>(product);

            return new SuccessDataResponse<ProductDTO>(mappedProduct);
        }

        public IResponse Update(ProductUpdateDTO productUpdateDTO, int id)
        {
            var mappedProduct = _mapper.Map<Product>(productUpdateDTO);
            var updatedProduct = _productRepository.Get(p => p.ProductId == id);

            updatedProduct.CategoryId = mappedProduct.CategoryId;
            updatedProduct.ProductName = mappedProduct.ProductName;
            updatedProduct.SupplierId = mappedProduct.SupplierId;
            _productRepository.Update(updatedProduct);

            return new SuccessResponse(Messages.ProductUpdated);
        }
    }
}
