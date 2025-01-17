<h1 align="center">
ApiShop - a REST API written in .NET for an online shop.
</br>
It is still in development
<br>
</h1>

## Technologies used:
* .NET 8.0
* EntityFrameworkCore 9.0
* JwtBearer 8.0


## There are 4 controllers where you can access:
* [CartController](#CartController)
* [OrderController](#OrderController)
* [ProductController](#ProductController)
* [UserController](#UserController)

## UserController
## Endpoint at  [/users](#users) | Method: POST
* it register user to database

- expect the following format data:
  
![image](https://github.com/user-attachments/assets/2d34117c-0f2f-4f24-83d6-c2eabe09eb4d)

If any error occurs, it will return only a message to you. The possible status is:
* 200 Ok
* 400 Bad request 
* 401 Unauthorized
</br>
The format of the returned data is as follows:

![image](https://github.com/user-attachments/assets/ec7506b3-fd05-4712-bf17-f7c917a63b99)



## Endpoint at  [/users/in](#users/in) | Method: POST
* it login user

- expect the following format data:
  
![image](https://github.com/user-attachments/assets/0d21150f-4892-4bc9-8d25-890bfef43f3e)


If any error occurs, it will return only a message to you. The possible status is:
* 200 Ok
* 400 Bad request 
* 401 Unauthorized
</br>
The format of the returned data is as follows:

![image](https://github.com/user-attachments/assets/2c4ccbcd-5a87-420c-b314-f3b2c854f07f)

