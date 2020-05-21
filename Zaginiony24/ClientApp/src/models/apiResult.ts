export interface ApiResult<T> {
    Result: T;
    IsSuccess: boolean;
    ErrorCodes: string[];
  }