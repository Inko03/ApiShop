public class Result<T>{
    public bool IsSuccess{get; private set;}
    public T Data {get;private set;}
    public string ErrorMessage{get; private set;}
    private Result(bool isSuccess,T data,string errorMessage){
        IsSuccess = isSuccess;
        Data = data;
        ErrorMessage = errorMessage;
    }
    public static Result<T> Success(T data){
        return new Result<T>(true,data,null);
    }
    public static Result<T>Failure(string errorMessage){
        return new Result<T>(false,default,errorMessage);
    }
}