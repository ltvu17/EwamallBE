using AutoMapper;
using Ewamall.Business.IRepository;
using Ewamall.DataAccess.Repository;
using Ewamall.Domain.Entities;
using Ewamall.Domain.Shared;
using Ewamall.WebAPI.DTOs;

namespace Ewamall.WebAPI.Services.Implements
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepo _voucherRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public VoucherService(IVoucherRepo voucherRepo, IUnitOfWork unitOfWork, IMapper mapper )
        {
            _mapper = mapper;
            _voucherRepo = voucherRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Voucher>> CreateVoucher(int staffId, CreateVoucherCommand request)
        {
            var result = Voucher.Create(
                request.VoucherCode,
                request.Type,
                request.Name,
                request.Description,
                request.Discount,
                request.StartDate,
                request.EndDate,
                request.MinOrder,
                request.MaxDiscount,
                staffId
                );
            if (result.IsFailure)
            {
                return Result.Failure<Voucher>(new Error("Create voucher", "Fail to create voucher"));
            }
            var voucher = result.Value;
            await _voucherRepo.AddAsync( voucher );
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<Result<Voucher>> DeleteVoucher(int id)
        {
            var voucher = await _voucherRepo.GetByIdAsync(id);
            if (voucher is null)
            {
                return Result.Failure<Voucher>(new Error("DeleteVoucher.GetById", "Voucher not found"));
            }
            await _voucherRepo.RemoveAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return voucher;
        }

        public async Task<Result<IEnumerable<Voucher>>> GetAllVoucher()
        {
            Result<IEnumerable<Voucher>> result = (await _voucherRepo.GetAllAsync()).ToList();
            if (result.IsFailure)
            {
                return Result.Failure<IEnumerable<Voucher>>(new Error("IEnumerable<Voucher>.GetAll()", "Fail to load voucher"));
            }
            return result;
        }

        public async Task<Result<Voucher>> UpdateVoucher(int id, CreateVoucherCommand request)
        {
            var voucher = await _voucherRepo.GetByIdAsync( id );
            var updateVoucher = _mapper.Map(request, voucher);
            if (voucher is null)
            {
                return Result.Failure<Voucher>(new Error("UpdateVoucher.GetById", "Voucher not found"));
            }

            await _voucherRepo.UpdateAsync(updateVoucher);
            await _unitOfWork.SaveChangesAsync();
            return updateVoucher;
        }
    }
}
