using BusinessObjects.RequestModels.VnPay;
using BusinessObjects.ResponseModels.VnPay;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.VNPayServices
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model);

        VnPaymentResponseModel PaymentResponse(IQueryCollection colletions);
    }
}
