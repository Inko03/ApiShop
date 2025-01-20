using Newtonsoft.Json.Serialization;

public class MessageServices {

public object Message(string errorStatus, string errorMessage){
    var dane = new{
        error = errorStatus,
        message = errorMessage
    };
    return dane;
}
public object DataSender(string errorStatus,List<ProductModel> product){
    var dane = new{
        error = errorStatus,
        data = product
    };
    return dane;
}

}