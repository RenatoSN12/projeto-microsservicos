using Emitix.BillingService.Common;
using Emitix.BillingService.Common.Extensions;
using Emitix.BillingService.Data.UnitOfWork;
using Emitix.BillingService.DTOs.Requests.Invoice;
using Emitix.BillingService.DTOs.Response;
using Emitix.BillingService.Exceptions;
using Emitix.BillingService.Mappers;
using Emitix.BillingService.Reports;
using Emitix.BillingService.Repositories;
using FluentValidation;
using QuestPDF.Fluent;

namespace Emitix.BillingService.Services;

public class BillingService(
    IBillingRepository repository,
    IUnitOfWork unitOfWork,
    IServiceProvider serviceProvider)
    : IBillingService
{
    public async Task<Response<InvoiceDto>> CreateInvoice(CreateInvoiceDto request)
    {
        try
        {
            var validator = serviceProvider.GetRequiredService<IValidator<CreateInvoiceDto>>();
            var validateResult = await validator.ValidateAsync(request);
            if (!validateResult.IsValid)
                return Response<InvoiceDto>.Error(null, validateResult.Errors.ToMessageString(), 400);

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

    public async Task<Response<InvoiceDto>> PrintInvoice(PrintInvoiceDto request)
    {
        try
        {
            var invoiceResult = await GetByNumberAndSeries(
                new GetInvoiceDto(request.InvoiceNumber, request.InvoiceSeries));

            if (!invoiceResult.IsSuccess)
                return invoiceResult;

            var report = new InvoiceDocument(invoiceResult.Data!);
            report.GeneratePdfAndShow();
            return Response<InvoiceDto>.Success(invoiceResult.Data!);
        }
        catch (Exception e)
        {
            return Response<InvoiceDto>.Error(null, e.Message, 500);
        }
    }

    public async Task<Response<InvoiceDto>> GetByNumberAndSeries(GetInvoiceDto request)
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

    public async Task<Response<InvoiceDto>> AlterInvoiceStatus(AlterInvoiceStatusDto request)
    {
        try
        {
            var invoice = await repository.GetInvoiceWithProductsByNumberAndSeriesAsync(new GetInvoiceDto(request.Number, request.Series));
            if (invoice == null)
                return Response<InvoiceDto>.Error(null,
                    "Não foi encontrada nenhuma nota fiscal com o número e série informados", 404);
            
            invoice.AlterStatus(request.Status);
            repository.Update(invoice);
            await unitOfWork.CommitAsync();
            return Response<InvoiceDto>.Success(invoice.ToDto());
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
}