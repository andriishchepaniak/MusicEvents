using AutoMapper;
using Core.DTO;
using Core.Interfaces;
using DAL.UnitOfWork;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ArtistSubscriptionService : IArtistSubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ArtistSubscriptionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ArtistSubscriptionDTO> Add(ArtistSubscriptionDTO entity)
        {
            await _unitOfWork.ArtistSubscription.Add(_mapper.Map<ArtistSubscription>(entity));
            await _unitOfWork.SaveAsync();
            return entity;
        }

        public async void Delete(ArtistSubscriptionDTO entity)
        {
            _unitOfWork.ArtistSubscription.Delete(_mapper.Map<ArtistSubscription>(entity));
            await _unitOfWork.SaveAsync();
        }

        public async Task Delete(int id)
        {
            _unitOfWork.ArtistSubscription.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<ArtistSubscriptionDTO>> GetAll()
        {
            return _mapper
                .Map<IEnumerable<ArtistSubscriptionDTO>>
                (await _unitOfWork.ArtistSubscription.GetAll());
        }

        public Task<IEnumerable<ArtistSubscriptionDTO>> GetAll(Expression<Func<ArtistSubscriptionDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<ArtistSubscriptionDTO> GetById(int id)
        {
            return _mapper
                .Map<ArtistSubscriptionDTO>(await _unitOfWork.ArtistSubscription.GetById(id));
        }

        public async Task<IEnumerable<ArtistSubscriptionDTO>> GetRange(int offset, int count)
        {
            return _mapper
                .Map<IEnumerable<ArtistSubscriptionDTO>>
                (await _unitOfWork.ArtistSubscription.GetRange(offset, count));
        }

        public async Task<ArtistSubscriptionDTO> Update(ArtistSubscriptionDTO entity)
        {
            var sub = _mapper.Map<ArtistSubscription>(entity);
            _unitOfWork.ArtistSubscription.Update(sub);
            await _unitOfWork.SaveAsync();
            return entity;
        }
    }
}
