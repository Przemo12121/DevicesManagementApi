using System.Collections;

namespace DevicesManagement.DataTransferObjects.Responses;

public record PaginationResponseDto<T>(int totalCount, ICollection<T> Results);
