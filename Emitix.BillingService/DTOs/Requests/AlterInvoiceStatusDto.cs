using Emitix.BillingService.Common.Enums;

namespace Emitix.BillingService.DTOs.Requests;

public sealed record AlterInvoiceStatusDto(int Number, string Series, EInvoiceStatus Status);