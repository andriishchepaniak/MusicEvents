using AutoMapper;
using Core.DTO;
using Core.Interfaces;
using DAL.UnitOfWorkService;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ArtistAndCitySubscriptionService : IArtistAndCitySubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ArtistAndCitySubscriptionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ArtistAndCitySubscriptionDTO> Add(ArtistAndCitySubscriptionDTO entity)
        {
            await _unitOfWork.ArtistAndCitySubscription
                .Add(_mapper.Map<ArtistAndCitySubscription>(entity));
            await _unitOfWork.SaveAsync();
            return entity;
        }

        public async Task Delete(int id)
        {
            _unitOfWork.ArtistAndCitySubscription.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<ArtistAndCitySubscriptionDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<ArtistAndCitySubscriptionDTO>>
                (await _unitOfWork.ArtistAndCitySubscription.GetAll());
        }

        public async Task<IEnumerable<ArtistAndCitySubscriptionDTO>> GetAll
            (Expression<Func<ArtistAndCitySubscriptionDTO, bool>> predicate)
        {
            var expression = _mapper.Map<Expression<Func<ArtistAndCitySubscription, bool>>>
                (predicate);

            return _mapper.Map<IEnumerable<ArtistAndCitySubscriptionDTO>>
                (await _unitOfWork.ArtistAndCitySubscription.GetAll(expression));
        }

        public async Task<ArtistAndCitySubscriptionDTO> GetById(int id)
        {
            return _mapper.Map<ArtistAndCitySubscriptionDTO>
                (await _unitOfWork.ArtistAndCitySubscription.GetById(id));
        }

        public async Task<IEnumerable<ArtistAndCitySubscriptionDTO>> GetRange
            (int offset, int count)
        {
            return _mapper.Map<IEnumerable<ArtistAndCitySubscriptionDTO>>
                (await _unitOfWork.ArtistAndCitySubscription.GetRange(offset, count));
        }

        public async Task<ArtistAndCitySubscriptionDTO> Update(ArtistAndCitySubscriptionDTO entity)
        {
            _unitOfWork.ArtistAndCitySubscription.Update
                (_mapper.Map<ArtistAndCitySubscription>(entity));

            await _unitOfWork.SaveAsync();
            return entity;
        }
    }
}
