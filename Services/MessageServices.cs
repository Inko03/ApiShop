using Newtonsoft.Json.Serialization;

public class MessageServices:IMessageServices {

public object Message(string errorStatus, string errorMessage){
    var dane = new{
        status = errorStatus,
        message = errorMessage
    };
    return dane;
}
public object DataSender(string errorStatus,List<ProductModel> product){
    var dane = new{
        status = errorStatus,
        data = product
    };
    return dane;
}

}