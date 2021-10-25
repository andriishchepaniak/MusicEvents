using AutoMapper;
using Core.DTO;
using Core.Interfaces;
using DAL.UnitOfWorkService;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CitySubscriptionService : ICitySubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CitySubscriptionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CitySubscriptionDTO> Add(CitySubscriptionDTO entity)
        {
            await _unitOfWork.CitySubscription.Add(_mapper.Map<CitySubscription>(entity));
            await _unitOfWork.SaveAsync();
            return entity;
        }

        public async void Delete(CitySubscriptionDTO entity)
        {
            _unitOfWork.CitySubscription.Delete(_mapper.Map<CitySubscription>(entity));
            await _unitOfWork.SaveAsync();
        }

        public async Task<int> Delete(int id)
        {
            _unitOfWork.CitySubscription.Delete(id);
            return await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<CitySubscriptionDTO>> GetAll()
        {
            return _mapper
                .Map<IEnumerable<CitySubscriptionDTO>>
                (await _unitOfWork.CitySubscription.GetAll());
        }

        public Task<IEnumerable<CitySubscriptionDTO>> GetAll(Expression<Func<CitySubscriptionDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<CitySubscriptionDTO> GetById(int id)
        {
            return _mapper
                .Map<CitySubscriptionDTO>(await _unitOfWork.CitySubscription.GetById(id));
        }

        public async Task<IEnumerable<CitySubscriptionDTO>> GetRange(int offset, int count)
        {
            return _mapper
                .Map<IEnumerable<CitySubscriptionDTO>>
                (await _unitOfWork.CitySubscription.GetRange(offset, count));
        }

        public async Task<CitySubscriptionDTO> Update(CitySubscriptionDTO entity)
        {
            var sub = _mapper.Map<CitySubscription>(entity);
            _unitOfWork.CitySubscription.Update(sub);
            await _unitOfWork.SaveAsync();
            return entity;
        }
    }
}
