using BusinessObjects.RequestModels.VnPay;
using BusinessObjects.ResponseModels.VnPay;
using Microsoft.AspNetCore.Http;

namespace Service.Services.VNPayServices
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model);

        VnPaymentResponseModel PaymentResponse(IQueryCollection colletions);
    }
}
