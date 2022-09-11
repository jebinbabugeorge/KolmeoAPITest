using AutoMapper;
using KolmeoAPI.DALayer;
using KolmeoAPI.Data.Models;
using KolmeoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KolmeoAPI.BLayer
{
    public class BusinessLayer : IBusinessLayer
    {
        private readonly IMapper _mapper;
        private readonly IDataAccessLayer _dataAccesslayer;

        public BusinessLayer(IMapper mapper, IDataAccessLayer dataAccesslayer)
        {
            _mapper = mapper;
            _dataAccesslayer = dataAccesslayer;
        }

        public async Task<int> CreateProductAsync(ProductDTO productDto)
        {
            ValidateProduct(productDto);

            var product = _mapper.Map<Product>(productDto);
            return await _dataAccesslayer.CreateProductAsync(product);
        }

        private static void ValidateProduct(ProductDTO productDto)
        {
            var exceptions = new List<Exception>();

            if (string.IsNullOrWhiteSpace(productDto.Name))
                exceptions.Add(new ArgumentException("Name cannot be empty"));

            if(string.IsNullOrWhiteSpace(productDto.Description))
                exceptions.Add(new ArgumentException("Description cannot be empty"));

            if (productDto.Price < 0.01M)
                exceptions.Add(new ArgumentException("Price cannot be less than $0.01"));

            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
        }

        public async Task<ProductDTO> GetProductAsync(int Id)
        {
            var product = await _dataAccesslayer.FindProductAsync(Id);

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            var products = await _dataAccesslayer.GetProductsAsync();

            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }
    }
}
