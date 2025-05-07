using Emitix.BillingService.Common.Enums;

namespace Emitix.BillingService.DTOs.Requests.Invoice;

public sealed record AlterInvoiceStatusDto(int Number, string Series, EInvoiceStatus Status);