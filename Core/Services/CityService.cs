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
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<City> Add(City entity)
        {
            var city = await _unitOfWork.CityRepository.Add(entity);
            await _unitOfWork.SaveAsync();
            return city;
        }

        public async Task<int> Delete(int id)
        {
            _unitOfWork.CityRepository.Delete(id);
            return await _unitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAll()
        {
            _unitOfWork.CityRepository.DeleteAll();
            return await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<City>> GetAll()
        {
            return await _unitOfWork.CityRepository.GetAll();
        }

        public async Task<IEnumerable<City>> GetAll(Expression<Func<City, bool>> predicate)
        {
            return await _unitOfWork.CityRepository.GetAll(predicate);
        }

        public async Task<City> GetById(int id)
        {
            return await _unitOfWork.CityRepository.GetById(id);
        }

        public async Task<IEnumerable<City>> GetRange(int offset, int count)
        {
            return await _unitOfWork.CityRepository.GetRange(offset, count);
        }

        public async Task<City> Update(City entity)
        {
            var city = _unitOfWork.CityRepository.Update(entity);
            await _unitOfWork.SaveAsync();
            return city;
        }
    }
}
