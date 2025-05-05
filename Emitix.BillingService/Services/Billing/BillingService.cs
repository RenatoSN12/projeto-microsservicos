using Emitix.BillingService.Common;
using Emitix.BillingService.Common.Extensions;
using Emitix.BillingService.Data.UnitOfWork;
using Emitix.BillingService.DTOs.Requests;
using Emitix.BillingService.DTOs.Response;
using Emitix.BillingService.Exceptions;
using Emitix.BillingService.Mappers;
using Emitix.BillingService.Reports;
using Emitix.BillingService.Repositories;
using Emitix.BillingService.Services.External;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using QuestPDF.Fluent;

namespace Emitix.BillingService.Services.Billing;

public class BillingService(
    IBillingRepository repository,
    IProductServiceClient productServiceClient,
    IUnitOfWork unitOfWork,
    IServiceProvider serviceProvider)
    : IBillingService
{
    public async Task<Response<InvoiceDto>> CreateInvoiceAsync(CreateInvoiceDto request)
    {
        try
        {
            var validator = serviceProvider.GetRequiredService<IValidator<CreateInvoiceDto>>();
            var validateResult = await validator.ValidateAsync(request);
            if (!validateResult.IsValid)
                return Response<InvoiceDto>.Error(null, validateResult.Errors.ToMessageString(), 400);

            var productCodes = request.Products.Select(x => x.ProductCode).ToArray();
            var invalidCodes = await GetInvalidProductCodes(productCodes);

            if (invalidCodes.Count > 0)
                return Response<InvoiceDto>.Error(null,
                    $"Não foram encontrados itens cadastrados com o(s) código(s): {string.Join(", ", invalidCodes)}", 400);

            var invoiceEntity = request.ToEntity();
            await repository.CreateInvoiceAsync(invoiceEntity);
            await unitOfWork.CommitAsync();
            return Response<InvoiceDto>.Success(invoiceEntity.ToDto());
        }
        catch (DomainException ex)
        {
            return Response<InvoiceDto>.Error(null, ex.Message, 400);
        }
        catch (Exception ex)
        {
            return Response<InvoiceDto>.Error(null, ex.Message, 500);
        }
    }

    public async Task<Response<InvoiceDto>> PrintInvoiceAsync(PrintInvoiceDto request)
    {
        var invoiceResult = await GetByNumberAndSeriesAsync(new GetInvoiceDto(request.InvoiceNumber, request.InvoiceSeries));

        if (!invoiceResult.IsSuccess)
            return invoiceResult;

        var a = new InvoiceDocument(invoiceResult.Data!);
        a.GeneratePdfAndShow();
        return Response<InvoiceDto>.Success(invoiceResult.Data!);
    }

    public async Task<Response<InvoiceDto>> GetByNumberAndSeriesAsync(GetInvoiceDto request)
    {
        try
        {
            var result = await repository.GetInvoiceWithProductsByNumberAndSeriesAsync(request);
            if (result == null)
                return Response<InvoiceDto>.Error(null,
                    "Não foi encontrada nenhuma nota fiscal com o número e série informados", 404);
            
            return Response<InvoiceDto>.Success(result.ToDto());
        }
        catch (Exception e)
        {
            return Response<InvoiceDto>.Error(null, e.Message, 500);
        }
    }

    public async Task<Response<List<InvoiceDto>>> GetAllInvoices()
    {
        try
        {
            var invoices = await repository.GetInvoicesWithProductsAsync();
            return invoices.Count == 0
                ? Response<List<InvoiceDto>>.Error(null, "Não foi encontrada nenhuma nota fiscal emitida.", 404) 
                : Response<List<InvoiceDto>>.Success(invoices.Select(x => x.ToDto()).ToList());
        }
        catch (Exception e)
        {
            return Response<List<InvoiceDto>>.Error(null, e.Message, 500);
        }
    }

    private async Task<List<string>> GetInvalidProductCodes(string[] productCodes)
    {
        var result = await productServiceClient.VerifyExistingCodes(productCodes);
        return (result.Message.IsNullOrEmpty() ? [] : result.Data)!;
    }
}